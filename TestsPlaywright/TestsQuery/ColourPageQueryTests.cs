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
    public async Task CreateButtonOnIndexPageNavigatesToCreateColourPage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
        var coloursTitle = await page.TitleAsync();
        Assert.Equal("Colours", coloursTitle);

        var createButton = page.GetByRole(AriaRole.Button, new() { Name = "Create" });
        if (await createButton.CountAsync() == 0)
        {
            createButton = page.GetByText("Create", new() { Exact = false });
            Assert.True(await createButton.CountAsync() > 0, "Create button not found on Colours index page.");
        }
        await createButton.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Colour'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewButtonOnIndexPageNavigatesToViewColourPage()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var viewButton = page.GetByRole(AriaRole.Button, new() { Name = "View" });
            if (await viewButton.CountAsync() == 0)
            {
                viewButton = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewButton.CountAsync() > 0, "View button not found on Colours index page.");
            }
            await viewButton.First.ClickAsync();
            await page.WaitForFunctionAsync("document.title === 'View Colour'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditButtonOnIndexPageNavigatesToEditColourPage()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var editButton = page.GetByRole(AriaRole.Button, new() { Name = "Edit" });
            if (await editButton.CountAsync() == 0)
            {
                editButton = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editButton.CountAsync() > 0, "Edit button not found on Colours index page.");
            }
            await editButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Colour'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task DeleteButtonOnIndexPageNavigatesToDeleteColourPage()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colours/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var deleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
            if (await deleteButton.CountAsync() == 0)
            {
                deleteButton = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteButton.CountAsync() > 0, "Delete button not found on Colours index page.");
            }
            await deleteButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Delete Colour'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Colour'");
        
        var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
        if (await cancelButton.CountAsync() == 0)
        {
            cancelButton = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Create Colour page.");
        }
        await cancelButton.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Colours'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelButtonOnEditPageNavigatesToIndex()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/edit/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Colour'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Edit Colour page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnViewPageNavigatesToIndex()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/view/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Colour'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on View Colour page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnDeletePageNavigatesToIndex()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/delete/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Colour'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Delete Colour page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }
}
