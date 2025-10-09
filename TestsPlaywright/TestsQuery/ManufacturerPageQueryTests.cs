namespace TestsPlaywright.TestsQuery;

public class ManufacturerPageQueryTests : BaseTestClass
{
    [Fact]
    public async Task ManufacturerHomePageLoads()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/", GlobalValues.GetPageOptions());
        var homeTitle = await page.TitleAsync();
        Assert.Equal("Manufacturer Manager", homeTitle);

        var manufacturersLink = page.GetByRole(AriaRole.Link, new() { Name = "Manufacturers" });
        if (await manufacturersLink.CountAsync() == 0)
        {
            manufacturersLink = page.GetByText("Manufacturers", new() { Exact = false });
            Assert.True(await manufacturersLink.CountAsync() > 0, "Manufacturers link not found in navmenu.");
        }
        await manufacturersLink.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CreateLinkOnIndexPageNavigatesToCreateManufacturerPage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturers/index", GlobalValues.GetPageOptions());
        var manufacturersTitle = await page.TitleAsync();
        Assert.Equal("Manufacturers", manufacturersTitle);

        var createLink = page.GetByRole(AriaRole.Link, new() { Name = "Create Manufacturer" });
        if (await createLink.CountAsync() == 0)
        {
            createLink = page.GetByText("Create", new() { Exact = false });
            Assert.True(await createLink.CountAsync() > 0, "Create Manufacturer link not found on Manufacturers page.");
        }
        await createLink.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Manufacturer'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewLinkOnIndexPageNavigatesToViewManufacturerPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturers/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

            var viewLink = page.GetByRole(AriaRole.Link, new() { Name = "View" });
            if (await viewLink.CountAsync() == 0)
            {
                viewLink = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewLink.CountAsync() > 0, "View link not found on Manufacturers index page.");
            }
            await viewLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'View Manufacturer'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditLinkOnIndexPageNavigatesToEditManufacturerPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturers/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

            var editLink = page.GetByRole(AriaRole.Link, new() { Name = "Edit" });
            if (await editLink.CountAsync() == 0)
            {
                editLink = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editLink.CountAsync() > 0, "Edit link not found on Manufacturers index page.");
            }
            await editLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Manufacturer'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Manufacturer'");

        var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
        if (await cancelLink.CountAsync() == 0)
        {
            cancelLink = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Create page.");
        }
        await cancelLink.First.ClickAsync();
        await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelLinkOnEditPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/edit/{manufacturerId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Manufacturer'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Edit page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelLinkOnViewPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/view/{manufacturerId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Manufacturer'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Back to list" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on View page.");
            }
            await cancelLink.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

}  