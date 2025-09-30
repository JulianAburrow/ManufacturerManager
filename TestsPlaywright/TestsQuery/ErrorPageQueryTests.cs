namespace TestsPlaywright.TestsQuery;

public class ErrorPageQueryTests : BaseTestClass
{
    [Fact]
    public async Task ErrorHomePageLoads()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        // In this test only we will load the application home page and then click the
        // Admin menu item to expand the dropdownlist, then click 'Errors' to navigate
        // to the Errors page

        await page.GotoAsync($"{GlobalValues.BaseUrl}/", GlobalValues.GetPageOptions());
        var homeTitle = await page.TitleAsync();
        Assert.Equal("Manufacturer Manager", homeTitle);

        var adminLink = page.GetByRole(AriaRole.Link, new() { Name = "Admin" });
        if (await adminLink.CountAsync() == 0)
        {
            adminLink = page.GetByText("Admin", new() { Exact = false });
            Assert.True(await adminLink.CountAsync() > 0, "Admin link not found on home page.");
        }
        await adminLink.ClickAsync();

        // This should have revealed the 'Errors' link in the Admin menu.
        var errorsLink = page.GetByRole(AriaRole.Link, new() { Name = "Errors" });
        if (await errorsLink.CountAsync() == 0)
        {
            errorsLink = page.GetByText("Errors", new() { Exact = false });
            Assert.True(await errorsLink.CountAsync() > 0, "Errors link not found in Admin menu.");
        }
        await errorsLink.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Errors'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewLinkOnIndexPageNavigatesToViewErrorPage()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/errors/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Errors'");

            var viewLink = page.GetByRole(AriaRole.Link, new() { Name = "View" });
            if (await viewLink.CountAsync() == 0)
            {
                viewLink = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewLink.CountAsync() > 0, "View link not found on Errors index page.");
            }
            await viewLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'View Error'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditLinkOnIndexPageNavigatesToEditErrorPage()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/errors/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Errors'");

            var editLink = page.GetByRole(AriaRole.Link, new() { Name = "Edit" });
            if (await editLink.CountAsync() == 0)
            {
                editLink = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editLink.CountAsync() > 0, "Edit link not found on Errors index page.");
            }
            await editLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Error'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task DeleteLinkOnIndexPageNavigatesToDeleteErrorPage()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/errors/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Errors'");
            
            var deleteLink = page.GetByRole(AriaRole.Link, new() { Name = "Delete" });
            if (await deleteLink.CountAsync() == 0)
            {
                deleteLink = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteLink.CountAsync() > 0, "Delete link not found on Errors index page.");
            }
            await deleteLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Delete Error'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }    

    [Fact]
    public async Task CancelLinkOnEditPageNavigatesToIndex()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/edit/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Error'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Edit Error page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnViewPageNavigatesToIndex()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/view/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Error'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on View Error page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnDeletePageNavigatesToIndex()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/delete/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Error'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Delete Error page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    
}
