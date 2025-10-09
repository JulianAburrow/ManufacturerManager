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
    public async Task CreateLinkOnIndexPageNavigatesToCreatePage()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/widgets/index", GlobalValues.GetPageOptions());
        var widgetsTitle = await page.TitleAsync();
        Assert.Equal("Widgets", widgetsTitle);

        var createLink = page.GetByRole(AriaRole.Link, new() { Name = "Create Widget" });
        if (await createLink.CountAsync() == 0)
        {
            createLink = page.GetByText("Create Widget", new() { Exact = false });
            Assert.True(await createLink.CountAsync() > 0, "Create Widget link not found on Widgets page");
        }
        await createLink.First.ClickAsync();

        await page.WaitForFunctionAsync("document.title === 'Create Widget'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task ViewLinkOnIndexPageNavigatesToViewWidgetPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widgets/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Widgets'");

            var viewLink = page.GetByRole(AriaRole.Link, new() { Name = "View" });
            if (await viewLink.CountAsync() == 0)
            {
                viewLink = page.GetByText("View", new() { Exact = false });
                Assert.True(await viewLink.CountAsync() > 0, "View link not found on Widgets index page.");
            }
            await viewLink.First.ClickAsync();

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
    public async Task EditLinkOnIndexPageNavigatesToEditWidgetPage()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widgets/index", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Widgets'");

            var editLink = page.GetByRole(AriaRole.Link, new() { Name = "Edit" });
            if (await editLink.CountAsync() == 0)
            {
                editLink = page.GetByText("Edit", new() { Exact = false });
                Assert.True(await editLink.CountAsync() > 0, "Edit link not found on Widgets index page.");
            }
            await editLink.First.ClickAsync();

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
    public async Task CancelLinkOnCreatePageNavigatesToIndex()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/create", GlobalValues.GetPageOptions());
        await page.WaitForFunctionAsync("document.title === 'Create Widget'");

        var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
        if (await cancelLink.CountAsync() == 0)
        {
            cancelLink = page.GetByText("Cancel", new() { Exact = false });
            Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Create page.");
        }
        await cancelLink.First.ClickAsync();
        await page.WaitForFunctionAsync("document.title === 'Widgets'");

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }

    [Fact]
    public async Task CancelLinkOnEditPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/edit/{widgetId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Widget'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Cancel" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on Edit page.");
            }
            await cancelLink.First.ClickAsync();

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
    public async Task CancelLinkOnViewPageNavigatesToIndex()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/view/{widgetId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'View Widget'");

            var cancelLink = page.GetByRole(AriaRole.Link, new() { Name = "Back to list" });
            if (await cancelLink.CountAsync() == 0)
            {
                cancelLink = page.GetByText("Cancel", new() { Exact = false });
                Assert.True(await cancelLink.CountAsync() > 0, "Cancel link not found on View page.");
            }
            await cancelLink.First.ClickAsync();

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
