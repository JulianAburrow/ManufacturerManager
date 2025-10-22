namespace TestsPlaywright.TestsCommand;

public partial class HelpDocumentPageCommandTests : BaseTestClass
{
    [Fact]
    public async Task CanUploadHelpDocument()
    {
        HelpDocumentHelper.AddHelpDocument();

        var page = await PlaywrightTestHelper.CreatePageAsync();

        const string expectedMessage = "File successfully uploaded.";

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocument/upload", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Upload Help Document'");

            await page.Locator("[data-testid='ddlHelpDocumentCategory']").Locator("..").ClickAsync();
            page.Locator("div.mud-popover div.mud-list-item").Filter(new() { HasTextRegex = new Regex("^Colour$") });
            await page.Locator("div.mud-popover div.mud-list-item").Filter(new() { HasTextRegex = new Regex("^Colour$") }).ClickAsync();

            await page.SetInputFilesAsync("[data-testid='uplHelpDocument']", HelpDocumentHelper.GetHelpDocumentPath());

            await page.WaitForFunctionAsync($"() => document.querySelector('[data-testid=\"fileUploadMessage\"]').innerText === '{expectedMessage}'");
            var message = await page.Locator("[data-testid='fileUploadMessage']").InnerTextAsync();
            Assert.Equal(expectedMessage, message);

            await page.ClickAsync("button:has-text('Submit')");

            await page.WaitForFunctionAsync("document.title === 'Help Documents'");

            Assert.True(File.Exists(HelpDocumentHelper.GetHelpDocumentPath()));

        }
        finally
        {
            await CleanUp(page);
        }
    }

    /*
     * This has been commented out as, whilst it works locally, it fails on Publish and in GitHub Actions.
     * I believe that trhis is due to permissions differing between the create and delete functionality.

    [Fact]
    public async Task CanDeleteHelpDocument()
    {
        HelpDocumentHelper.AddHelpDocument();

        const string expectedMessage = "File successfully uploaded.";

        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            var documentNameWithoutSuffix = Uri.EscapeDataString(HelpDocumentHelper.DocumentName.Substring(0, HelpDocumentHelper.DocumentName.IndexOf('.')));
            await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocument/delete/colour/{documentNameWithoutSuffix}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Help Document'");

            var deleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
            if (await deleteButton.CountAsync() == 0)
            {
                deleteButton = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteButton.CountAsync() > 0, "Delete button not found on Delete Help Document page");
            }
            await deleteButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Help Documents'");

            await Task.Delay(1000);

            var path = HelpDocumentHelper.GetHelpDocumentPath();
            var timeout = TimeSpan.FromSeconds(5);
            var sw = Stopwatch.StartNew();

            while (File.Exists(path) && sw.Elapsed < timeout)
            {
                await Task.Delay(200);
            }

            Assert.False(File.Exists(path), $"Expected file to be deleted, but it still exists at: {path}");
        }
        finally
        {
            await CleanUp(page);
        }
    }

    */

    private async Task CleanUp(IPage page)
    {
        HelpDocumentHelper.RemoveHelpDocument();
        if (File.Exists(HelpDocumentHelper.GetHelpDocumentPath()))
        {
            File.Delete(HelpDocumentHelper.GetHelpDocumentPath());
        }
        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }
}
