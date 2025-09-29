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
    public async Task ViewButtonOnIndexPageNavigatesToViewErrorPage()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/errors/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Errors'");

            var viewButton = page.GetByRole(AriaRole.Button, new() { Name = "View" });
            if (await viewButton.CountAsync() == 0)
            {
                viewButton = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewButton.CountAsync() > 0, "View button not found on Errors index page.");
            }
            await viewButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'View Error'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditButtonOnIndexPageNavigatesToEditErrorPage()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/errors/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Errors'");

            var editButton = page.GetByRole(AriaRole.Button, new() { Name = "Edit" });
            if (await editButton.CountAsync() == 0)
            {
                editButton = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editButton.CountAsync() > 0, "Edit button not found on Errors index page.");
            }
            await editButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Error'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task DeleteButtonOnIndexPageNavigatesToDeleteErrorPage()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/errors/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Errors'");
            
            var deleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
            if (await deleteButton.CountAsync() == 0)
            {
                deleteButton = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteButton.CountAsync() > 0, "Delete button not found on Errors index page.");
            }
            await deleteButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Delete Error'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }    

    [Fact]
    public async Task CancelButtonOnEditPageNavigatesToIndex()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/edit/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Error'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Edit Error page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnViewPageNavigatesToIndex()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/view/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Error'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on View Error page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnDeletePageNavigatesToIndex()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/delete/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Error'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Delete Error page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    
}
