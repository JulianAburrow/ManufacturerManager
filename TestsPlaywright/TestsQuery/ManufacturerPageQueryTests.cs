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
    public async Task CreateButtonOnIndexPageNavigatesToCreateManufacturerPage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturers/index", GlobalValues.GetPageOptions());
        var manufacturersTitle = await page.TitleAsync();
        Assert.Equal("Manufacturers", manufacturersTitle);

        var createButton = page.GetByRole(AriaRole.Button, new() { Name = "Create Manufacturer" });
        if (await createButton.CountAsync() == 0)
        {
            createButton = page.GetByText("Create", new() { Exact = false });
            Assert.True(await createButton.CountAsync() > 0, "Create Manufacturer button not found on Manufacturers page.");
        }
        await createButton.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Manufacturer'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewButtonOnIndexPageNavigatesToViewManufacturerPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturers/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

            var viewButton = page.GetByRole(AriaRole.Button, new() { Name = "View" });
            if (await viewButton.CountAsync() == 0)
            {
                viewButton = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewButton.CountAsync() > 0, "View button not found on Manufacturers index page.");
            }
            await viewButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'View Manufacturer'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditButtonOnIndexPageNavigatesToEditManufacturerPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturers/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

            var editButton = page.GetByRole(AriaRole.Button, new() { Name = "Edit" });
            if (await editButton.CountAsync() == 0)
            {
                editButton = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editButton.CountAsync() > 0, "Edit button not found on Manufacturers index page.");
            }
            await editButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Manufacturer'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Manufacturer'");

        var cancelButton = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
        if (await cancelButton.CountAsync() == 0)
        {
            cancelButton = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Create page.");
        }
        await cancelButton.First.ClickAsync();
        await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelButtonOnEditPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/edit/{manufacturerId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Manufacturer'");

            var cancelButton = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Edit page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnViewPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/view/{manufacturerId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Manufacturer'");

            var backToListButton = page.GetByRole(AriaRole.Link, new() { Name = "Back to list" });
            if (await backToListButton.CountAsync() == 0)
            {
                backToListButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await backToListButton.CountAsync() > 0, "Back to list button not found on View page.");
            }
            await backToListButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

}  