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
    public async Task CreateLinkOnIndexPageNavigatesToCreateColourJustificationPage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        Assert.Equal("Colour Justifications", await page.TitleAsync());

        var createLink = page.GetByRole(AriaRole.Link, new() { Name = "Create Colour Justification" });
        if (await createLink.CountAsync() == 0)
        {
            createLink = page.GetByText("Create Colour Justification", new() { Exact = false });
            Assert.True(await createLink.CountAsync() > 0, "Create Colour Justification link not found on Colour Justifications index page.");
        }
        await createLink.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Colour Justification'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewLinkOnIndexPageNavigatesToViewColourJustificationPage()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var viewLink = page.GetByRole(AriaRole.Link, new() { Name = "View" });
            if (await viewLink.CountAsync() == 0)
            {
                viewLink = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewLink.CountAsync() > 0, "View link not found on Colour Justifications index page.");
            }
            await viewLink.First.ClickAsync();
            await page.WaitForFunctionAsync("document.title === 'View Colour Justification'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditLinkOnIndexPageNavigatesToEditColourJustificationPage()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var editLink = page.GetByRole(AriaRole.Link, new() { Name = "Edit" });
            if (await editLink.CountAsync() == 0)
            {
                editLink = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editLink.CountAsync() > 0, "Edit link not found on Colour Justifications index page.");
            }
            await editLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Colour Justification'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task DeleteLinkOnIndexPageNavigatesToDeleteColourJustificationPage()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustifications/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var deleteLink = page.GetByRole(AriaRole.Link, new() { Name = "Delete" });
            if (await deleteLink.CountAsync() == 0)
            {
                deleteLink = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteLink.CountAsync() > 0, "Delete link not found on Colour Justifications index page.");
            }
            await deleteLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Delete Colour Justification'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    

    [Fact]
    public async Task CancelLinkOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Colour Justification'");

        var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
        if (await cancelLink.CountAsync() == 0)
        {
            cancelLink = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Create Colour Justification page.");
        }
        await cancelLink.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelLinkOnEditPageNavigatesToIndex()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/edit/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Colour Justification'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Edit Colour Justification page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnViewPageNavigatesToIndex()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/view/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Colour Justification'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on View Colour Justification page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnDeletePageNavigatesToIndex()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {

            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/delete/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Colour Justification'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel button not found on Delete Colour Justification page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }
}
