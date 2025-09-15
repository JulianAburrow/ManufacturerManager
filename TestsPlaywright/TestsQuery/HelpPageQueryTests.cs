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

        await SelectDropDownOption(page, "category-select", category);
        await page.GetByLabel("Type your question here").FillAsync(question);

        await page.GetByTestId("model-select").Locator("..").ClickAsync();
        var modelItems = page.Locator("div.mud-popover div.mud-list-item");
        await modelItems.First.WaitForAsync(new() { Timeout = 10000 });
        var firstModelText = await modelItems.First.InnerTextAsync();
        await page.ClickAsync($"div.mud-popover div.mud-list-item:has-text('{firstModelText}')");

        var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
        if (await submitButton.CountAsync() == 0)
        {
            submitButton = page.GetByText("Submit", new() { Exact = false });
            Assert.True(await submitButton.CountAsync() > 0, "Submit button not found.");
        }
        await submitButton.First.ClickAsync();

        var thinkingLocator = page.Locator("text=🤔 Thinking about it...");
        await thinkingLocator.WaitForAsync(new() { Timeout = 5000 });

        var modelResponse = page.GetByTestId("model-response");
        await modelResponse.First.WaitForAsync(new() { Timeout = 20000 });
        var replyText = await modelResponse.InnerTextAsync();
        Assert.False(string.IsNullOrWhiteSpace(replyText), "Response block is empty.");
        Assert.True(replyText.Length > 30, "Response block is unexpectedly short.");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    private static async Task SelectDropDownOption(IPage page, string dropdownTestId, string itemText)
    {
        await page.GetByTestId(dropdownTestId).Locator("..").ClickAsync();
        await page.ClickAsync($"div.mud-popover div.mud-list-item:has-text('{itemText}')");
    }
}
