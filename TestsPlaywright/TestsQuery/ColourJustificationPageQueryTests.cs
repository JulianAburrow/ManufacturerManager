namespace TestsPlaywright.TestsQuery;

public class ColourJustificationPageQueryTests : BaseTestClass
{
    [Fact]
    public async Task ColourJustificationHomePageLoads()
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

        // This should have revealed the 'Colour Justifications' link in the admin menu.
        var colourJustificationsLink = page.GetByRole(AriaRole.Link, new() { Name = "Colour Justifications" });
        if (await colourJustificationsLink.CountAsync() == 0)
        {
            colourJustificationsLink = page.GetByText("Colour Justifications", new() { Exact = false });
            Assert.True(await colourJustificationsLink.CountAsync() > 0, "Colour Justifications link not found in admin menu.");
        }
        await colourJustificationsLink.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CreateButtonOnIndexPageNavigatesToCreateColourJustificationPage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        Assert.Equal("Colour Justifications", await page.TitleAsync());

        var createButton = page.GetByRole(AriaRole.Button, new() { Name = "Create" });
        if (await createButton.CountAsync() == 0)
        {
            createButton = page.GetByText("Create", new() { Exact = false });
            Assert.True(await createButton.CountAsync() > 0, "Create button not found on Colour Justifications index page.");
        }
        await createButton.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Colour Justification'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewButtonOnIndexPageNavigatesToViewColourJustificationPage()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var viewButton = page.GetByRole(AriaRole.Button, new() { Name = "View" });
            if (await viewButton.CountAsync() == 0)
            {
                viewButton = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewButton.CountAsync() > 0, "View button not found on Colour Justifications index page.");
            }
            await viewButton.First.ClickAsync();
            await page.WaitForFunctionAsync("document.title === 'View Colour Justification'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditButtonOnIndexPageNavigatesToEditColourJustificationPage()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var editButton = page.GetByRole(AriaRole.Button, new() { Name = "Edit" });
            if (await editButton.CountAsync() == 0)
            {
                editButton = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editButton.CountAsync() > 0, "Edit button not found on Colour Justifications index page.");
            }
            await editButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Colour Justification'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task DeleteButtonOnIndexPageNavigatesToDeleteColourJustificationPage()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var deleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
            if (await deleteButton.CountAsync() == 0)
            {
                deleteButton = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteButton.CountAsync() > 0, "Delete button not found on Colour Justifications index page.");
            }
            await deleteButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Delete Colour Justification'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    

    [Fact]
    public async Task CancelButtonOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Colour Justification'");

        var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
        if (await cancelButton.CountAsync() == 0)
        {
            cancelButton = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Create Colour Justification page.");
        }
        await cancelButton.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelButtonOnEditPageNavigatesToIndex()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/edit/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Colour Justification'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Edit Colour Justification page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnViewPageNavigatesToIndex()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/view/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Colour Justification'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on View Colour Justification page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnDeletePageNavigatesToIndex()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {

            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/delete/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Colour Justification'");

            var cancelButton = page.GetByRole(AriaRole.Button, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Delete Colour Justification page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }
}
