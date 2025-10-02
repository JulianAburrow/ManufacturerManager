namespace TestsPlaywright.TestsCommand;

public class WidgetPageCommandTests : BaseTestClass
{
    [Fact]
    public async Task CanCreateWidget()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widget = new WidgetModel();
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            // database will have been seeded with colours and colour justifications.
            var colourName = "Red";
            var colourJustificationName = "Customer request";
            var statusName = PublicEnums.WidgetStatusEnum.Active.ToString();
            var costPrice = 10m;
            var retailPrice = 20m;
            var stockLevel = 5;
            var initialCount = _context.Widgets.Count();
            var manufacturer = _context.Manufacturers.First(m => m.ManufacturerId == manufacturerId);

            await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/create", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Create Widget'");
            var widgetName = $"Test Widget {Guid.NewGuid()}";
            await page.GetByLabel("Name").FillAsync(widgetName);
            await SelectDropdownOption(page, "manufacturer-select", manufacturer.Name);
            await SelectDropdownOption(page, "colour-select", colourName);
            await SelectDropdownOption(page, "colour-justification-select", colourJustificationName);
            await SelectDropdownOption(page, "status-select", statusName);
            await page.GetByLabel("Cost Price").FillAsync(costPrice.ToString());
            await page.GetByLabel("Retail Price").FillAsync(retailPrice.ToString());
            await page.GetByLabel("Stock Level").FillAsync(stockLevel.ToString());

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Create Widget page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Widgets'");

            Assert.Equal(initialCount + 1, _context.Widgets.Count());

            widget = await _context.Widgets
                .Include(w => w.Manufacturer)
                .Include(w => w.Colour)
                .Include(w => w.ColourJustification)
                .FirstOrDefaultAsync(w => w.Name == widgetName);
            Assert.NotNull(widget);
            Assert.Equal(widgetName, widget.Name);
            Assert.Equal(manufacturer.Name, widget.Manufacturer.Name);
            Assert.Equal(colourName, widget.Colour.Name);
            Assert.Equal(colourJustificationName, widget.ColourJustification.Justification);
            Assert.Equal((int)PublicEnums.WidgetStatusEnum.Active, widget.StatusId);
            Assert.Equal(costPrice, widget.CostPrice);
            Assert.Equal(retailPrice, widget.RetailPrice);
            Assert.Equal(stockLevel, widget.StockLevel);
        }
        finally
        {
            if (widget != null)
            {
                WidgetHelper.RemoveWidget(widget.WidgetId, _context);
            }
            _context.ChangeTracker.Clear();
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CanEditWidget()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var newManufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var widgetId = WidgetHelper.AddWidget(manufacturerId, _context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            // database will have been seeded with colours and colour justifications.
            var colourName = "Red";
            var colourJustificationName = "Customer request";
            var statusName = "Inactive";
            var costPrice = 10m;
            var retailPrice = 20m;
            var stockLevel = 5;
            var newManufacturer = _context.Manufacturers.First(m => m.ManufacturerId == newManufacturerId);

            await page.GotoAsync($"{GlobalValues.BaseUrl}/widget/edit/{widgetId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Widget'");

            var updatedWidgetName = $"Updated Widget {Guid.NewGuid()}";
            await page.GetByLabel("Name").FillAsync(updatedWidgetName);
            await SelectDropdownOption(page, "manufacturer-select", newManufacturer.Name);
            await SelectDropdownOption(page, "colour-select", colourName);
            await SelectDropdownOption(page, "colour-justification-select", colourJustificationName);
            await SelectDropdownOption(page, "status-select", statusName);
            await page.GetByLabel("Cost Price").FillAsync(costPrice.ToString());
            await page.GetByLabel("Retail Price").FillAsync(retailPrice.ToString());
            await page.GetByLabel("Stock Level").FillAsync(stockLevel.ToString());

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Edit Widget page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Widgets'");

            var updatedWidget = await WaitForWidgetUpdate(widgetId, updatedWidgetName);
            Assert.NotNull(updatedWidget);
            Assert.Equal(updatedWidgetName, updatedWidget.Name);
            Assert.Equal(newManufacturer.Name, updatedWidget.Manufacturer.Name);
            Assert.Equal(colourName, updatedWidget.Colour.Name);
            Assert.Equal(colourJustificationName, updatedWidget.ColourJustification.Justification);
            Assert.Equal((int)PublicEnums.WidgetStatusEnum.Inactive, updatedWidget.StatusId);
            Assert.Equal(costPrice, updatedWidget.CostPrice);
            Assert.Equal(retailPrice, updatedWidget.RetailPrice);
            Assert.Equal(stockLevel, updatedWidget.StockLevel);
        }
        finally
        {
            WidgetHelper.RemoveWidget(widgetId, _context);
            _context.ChangeTracker.Clear();
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            ManufacturerHelper.RemoveManufacturer(newManufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    private async Task<WidgetModel?> WaitForWidgetUpdate(int widgetId, string expectedName)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < GlobalValues.GlobalTimeOut)
        {
            var widget = await _context.Widgets
                .Include(w => w.Manufacturer)
                .Include(w => w.Colour)
                .Include(w => w.ColourJustification)
                .Include(w => w.Status)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.WidgetId == widgetId);
            if (widget != null && widget.Name == expectedName)
                return widget;
            await Task.Delay(100);
        }
        return await _context.Widgets.FindAsync(widgetId);
    }
    private static async Task SelectDropdownOption(IPage page, string dropdownTestId, string itemText)
    {
        await page.GetByTestId(dropdownTestId).Locator("..").ClickAsync();
        await page.ClickAsync($"div.mud-popover div.mud-list-item:has-text('{itemText}')");
    }

}
