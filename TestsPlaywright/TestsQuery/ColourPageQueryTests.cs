namespace TestsPlaywright.TestsQuery;

public class ColourPageQueryTests : BaseTestClass
{
    [Fact]
    public async Task ColourHomePageLoads()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        // In this test only we will load the application home page and then click the
        // Admin menu item to expand the dropdownlist, then click 'Colours' to navigate
        // to the Colours page (which will always contain something as there are colours
        // created when the database is built).

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

        // This should have revealed the 'Colours' link in the Admin menu.
        var coloursLink = page.GetByRole(AriaRole.Link, new() { Name = "Colours" });
        if (await coloursLink.CountAsync() == 0)
        {
            coloursLink = page.GetByText("Colours", new() { Exact = false });
            Assert.True(await coloursLink.CountAsync() > 0, "Colours link not found in Admin menu.");
        }
        await coloursLink.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Colours'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CreateLinkOnIndexPageNavigatesToCreateColourPage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
        var coloursTitle = await page.TitleAsync();
        Assert.Equal("Colours", coloursTitle);

        var createLink = page.GetByRole(AriaRole.Link, new() { Name = "Create Colour" });
        if (await createLink.CountAsync() == 0)
        {
            createLink = page.GetByText("Create Colour", new() { Exact = false });
            Assert.True(await createLink.CountAsync() > 0, "Create Colour link not found on Colours index page.");
        }
        await createLink.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Colour'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewLinkOnIndexPageNavigatesToViewColourPage()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var viewLink = page.GetByRole(AriaRole.Link, new() { Name = "View" });
            if (await viewLink.CountAsync() == 0)
            {
                viewLink = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewLink.CountAsync() > 0, "View link not found on Colours index page.");
            }
            await viewLink.First.ClickAsync();
            await page.WaitForFunctionAsync("document.title === 'View Colour'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditLinkOnIndexPageNavigatesToEditColourPage()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var editLink = page.GetByRole(AriaRole.Button, new() { Name = "Edit" });
            if (await editLink.CountAsync() == 0)
            {
                editLink = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editLink.CountAsync() > 0, "Edit link not found on Colours index page.");
            }
            await editLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Colour'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task DeleteLinkOnIndexPageNavigatesToDeleteColourPage()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var deleteLink = page.GetByRole(AriaRole.Link, new() { Name = "Delete" });
            if (await deleteLink.CountAsync() == 0)
            {
                deleteLink = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteLink.CountAsync() > 0, "Delete link not found on Colours index page.");
            }
            await deleteLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Delete Colour'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Colour'");
        
        var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
        if (await cancelLink.CountAsync() == 0)
        {
            cancelLink = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Create Colour page.");
        }
        await cancelLink.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Colours'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelLinkOnEditPageNavigatesToIndex()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/edit/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Colour'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Edit Colour page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnViewPageNavigatesToIndex()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/view/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Colour'");

            var cancelLink = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on View Colour page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnDeletePageNavigatesToIndex()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/delete/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Colour'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Delete Colour page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }
}
