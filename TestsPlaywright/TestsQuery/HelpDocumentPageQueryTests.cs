namespace TestsPlaywright.TestsQuery;

public class HelpDocumentPageQueryTests : BaseTestClass
{
    [Fact]
    public async Task HelpDocumentHomePageLoads()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocuments/index");
        var helpDocumentTitle = await page.TitleAsync();
        Assert.Equal("Help Documents", helpDocumentTitle);
    }

    [Fact]
    public async Task UploadLinkInIndexPageNavigatesToUploadDocumentPage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocuments/index");

        var uploadLink = page.GetByRole(AriaRole.Link, new() { Name = "Upload Help Document" });
        if (await uploadLink.CountAsync() == 0)
        {
            uploadLink = page.GetByText("Upload Help Document", new() { Exact = false });
            Assert.True(await uploadLink.CountAsync() > 0, "Upoad Help Document link not found on Help Documents index page.");
        }
        await uploadLink.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Upload Help Document'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task OpenLinkOnIndexPageOpensDocument()
    {
        HelpDocumentHelper.AddHelpDocument();

        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            var downloadTask = page.WaitForDownloadAsync();
            var popupTask = page.WaitForPopupAsync();

            await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocuments/index");
            var helpDocumentsTitle = await page.TitleAsync();
            Assert.Equal("Help Documents", helpDocumentsTitle);

            var openLink = page.GetByRole(AriaRole.Link, new() { Name = "Open" });
            if (await openLink.CountAsync() == 0)
            {
                openLink = page.GetByText("Open", new() { Exact = false });
                Assert.True(await openLink.CountAsync() > 0, "Open link not found on Colours index page.");
            }
            await openLink.First.ClickAsync();

            var completed = await Task.WhenAny(downloadTask, popupTask);

            if (completed == downloadTask)
            {
                var download = await downloadTask;
                Assert.EndsWith(".pdf", download.SuggestedFilename);
            }
            else
            {
                var popupPage = await popupTask;
                Assert.Contains(".pdf", popupPage.Url);
            }                
        }
        finally
        {
            await CleanUp(page);
        }
    }

    [Fact]
    public async Task DeleteLinkOnIndexPageOpensDeletePage()
    {
        HelpDocumentHelper.AddHelpDocument();

        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {

            await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocuments/index");
            var helpDocumentsTitle = await page.TitleAsync();
            Assert.Equal("Help Documents", helpDocumentsTitle);

            var deleteLink = page.GetByRole(AriaRole.Link, new() { Name = "Delete" });
            if (await deleteLink.CountAsync() == 0)
            {
                deleteLink = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteLink.CountAsync() > 0, "Delete link not found on Colours index page.");
            }
            await deleteLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Delete Help Document'");
        }
        finally
        {
            await CleanUp(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnUploadPageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocument/upload");
        await page.WaitForFunctionAsync("document.title === 'Upload Help Document'");

        var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
        if (await cancelLink.CountAsync() == 0)
        {
            cancelLink = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Upload Help Document page.");
        }
        await cancelLink.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Help Documents'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelLinkOnDeletePageNavigatesToIndex()
    {
        HelpDocumentHelper.AddHelpDocument();
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            var documentName = HelpDocumentHelper.DocumentName[..HelpDocumentHelper.DocumentName.IndexOf('.')];

            await page.GotoAsync($"{GlobalValues.BaseUrl}/helpdocument/delete/colour/{documentName}");
            await page.WaitForFunctionAsync("document.title === 'Delete Help Document'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Delete Help Document page.");
            }
            await cancelLink.ClickAsync();
            await page.WaitForFunctionAsync("document.title === 'Help Documents'");
        }
        finally
        {
            await CleanUp(page);
        }
    }

    private async Task CleanUp(IPage page)
    {
        HelpDocumentHelper.RemoveHelpDocument();
        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }
}
