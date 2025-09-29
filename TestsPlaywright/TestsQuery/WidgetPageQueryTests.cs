namespace TestsPlaywright.TestsQuery;

public class WidgetPageQueryTests : BaseTestClass
{
    [Fact]
    public async Task WidgetHomePageLoads()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/", GlobalValues.GetPageOptions());
        var homeTitle = await page.TitleAsync();
        Assert.Equal("Manufacturer Manager", homeTitle);

        var widgetsLink = page.GetByRole(AriaRole.Link, new() { Name = "Widgets" });
        if (await widgetsLink.CountAsync() == 0)
        {
            widgetsLink = page.GetByText("Widgets", new() { Exact = false });
            Assert.True(await widgetsLink.CountAsync() > 0, "Widgets link not found in navmenu");
        }
        await widgetsLink.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Widgets'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CreateButtonOnIndexPageNavigatesToCreatePage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/widgets/index", GlobalValues.GetPageOptions());
        var widgetsTitle = await page.TitleAsync();
        Assert.Equal("Widgets", widgetsTitle);

        var createButton = page.GetByRole(AriaRole.Button, new() { Name = "Create Widget" });
        if (await createButton.CountAsync() == 0)
        {
            createButton = page.GetByText("Create Widget", new() { Exact = false });
            Assert.True(await createButton.CountAsync() > 0, "Create Widget button not found on Widgets page");
        }
        await createButton.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Widget'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewButtonOnIndexPageNavigatesToViewWidgetPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widgets/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Widgets'");

            var viewButton = page.GetByRole(AriaRole.Link, new() { Name = "View" });
            if (await viewButton.CountAsync() == 0)
            {
                viewButton = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewButton.CountAsync() > 0, "View button not found on Widgets index page.");
            }
            await viewButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'View Widget'");
        }
        finally
        {
            WidgetHelper.RemoveWidget(widgetId, _context);
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task EditButtonOnIndexPageNavigatesToEditWidgetPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widgets/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Widgets'");

            var editButton = page.GetByRole(AriaRole.Link, new() { Name = "Edit" });
            if (await editButton.CountAsync() == 0)
            {
                editButton = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editButton.CountAsync() > 0, "Edit button not found on Widgets index page.");
            }
            await editButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Edit Widget'");
        }
        finally
        {
            WidgetHelper.RemoveWidget(widgetId, _context);
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }    

    [Fact]
    public async Task CancelButtonOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Widget'");

        var cancelButton = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
        if (await cancelButton.CountAsync() == 0)
        {
            cancelButton = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Create page.");
        }
        await cancelButton.First.ClickAsync();
        await page.WaitForFunctionAsync("document.title === 'Widgets'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelButtonOnEditPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/edit/{widgetId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Widget'");

            var cancelButton = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelButton.CountAsync() == 0)
            {
                cancelButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelButton.CountAsync() > 0, "Cancel button not found on Edit page.");
            }
            await cancelButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Widgets'");
        }
        finally
        {
            WidgetHelper.RemoveWidget(widgetId, _context);
            _context.ChangeTracker.Clear();
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CancelButtonOnViewPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/view/{widgetId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Widget'");

            var backToListButton = page.GetByRole(AriaRole.Link, new() { Name = "Back to list" });
            if (await backToListButton.CountAsync() == 0)
            {
                backToListButton = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await backToListButton.CountAsync() > 0, "Back to list button not found on View page.");
            }
            await backToListButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Widgets'");
        }
        finally
        {
            WidgetHelper.RemoveWidget(widgetId, _context);
            _context.ChangeTracker.Clear();
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }
}
