using System.Diagnostics.CodeAnalysis;

namespace TestsPlaywright.TestsQuery;

public class HelpPageQueryTests : BaseTestClass
{
    public static IEnumerable<object[]> HelpQueries =>
    [
        ["Manufacturer", "What is a Manufacturer?"],
        ["Manufacturer", "How do I create a Manufacturer?"],
        ["Widget", "What is a Widget?"],
        ["Widget", "How do I create a Widget?"],
        ["Colour", "What is a Colour?"],
        ["Colour", "How do I create a colour?"],
        ["ColourJustification", "What is a ColourJustification?"],
        ["ColourJustification", "How do I create a ColourJustification?"],
    ];

    [Fact]
    public async Task HelpPageLoads()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/Help");
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var helpTitle = await page.TitleAsync();
        Assert.Equal("Help", helpTitle);

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Theory]
    [MemberData(nameof(HelpQueries))]
    public async Task HelpPageRespondsToKnownQueries(string category, string question) =>
        await RunHelpQueryTestAsync(category, question);

    private async Task RunHelpQueryTestAsync(string category, string question)
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/Help", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Help'");

        await page.GetByLabel("Type your question here").FillAsync(question);

        await page.Locator("[data-testid='model-select']").Locator("..").ClickAsync();
        var modelItems = page.Locator("div.mud-popover div.mud-list-item");
        await modelItems.First.WaitForAsync();
        await modelItems.First.ClickAsync();

        // Select this last as in the UI run up by the tests if you select it first and then select the model
        // the category will be reset. This does not happen in the application when used normally.
        await page.Locator("[data-testid='category-select']").Locator("..").ClickAsync();
        var categoryItems = page.Locator("div.mud-popover div.mud-list-item");
        await categoryItems.First.WaitForAsync();
        await page.ClickAsync($"div.mud-popover div.mud-list-item:has-text('{category}')");

        var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
        if (await submitButton.CountAsync() == 0)
        {
            submitButton = page.GetByText("Submit", new() { Exact = false });
            Assert.True(await submitButton.CountAsync() > 0, "Submit button not found.");
        }

        await submitButton.First.ClickAsync();

        var thinkingLocator = page.Locator("text=🤔 Thinking about it...");
        await thinkingLocator.WaitForAsync();

        var modelResponse = page.GetByTestId("model-response");
        await modelResponse.First.WaitForAsync();
        var replyText = await WaitForStableResponseAsync(page, "[data-testid='model-response']");
        Assert.False(string.IsNullOrWhiteSpace(replyText), "Response block is empty.");
        Assert.False(replyText.StartsWith("❌"), $"Model failed: {replyText}");
        Assert.True(replyText.Length > 30, "Response block is unexpectedly short.");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    private static async Task<string> WaitForStableResponseAsync(IPage page, string selector)
    {
        var locator = page.Locator(selector);
        var start = DateTime.UtcNow;
        string lastText = string.Empty;
        int stableCount = 0;

        while ((DateTime.UtcNow - start).TotalMilliseconds < GlobalValues.GlobalTimeOut)
        {
            var currentText = await locator.InnerTextAsync();
            if (currentText == lastText)
            {
                stableCount++;
                if (stableCount >= 3) break; // 3 consecutive matches = stable
            }
            else
            {
                stableCount = 0;
                lastText = currentText;
            }

            await Task.Delay(300); // Poll every 300ms
        }

        if (stableCount < 3)
            throw new TimeoutException($"Response did not stabilize within {GlobalValues.GlobalTimeOut}ms");

        return lastText;
    }

}
