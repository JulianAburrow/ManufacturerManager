using Microsoft.EntityFrameworkCore;

namespace TestsPlaywright.TestsCommand;

public class ManufacturerPageCommandTests : BaseTestClass
{
    [Fact]
    public async Task CanCreateManufacturer()
    {
        var manufacturer = new ManufacturerModel();
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            var initialCount = _context.Manufacturers.Count();
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/create", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Create Manufacturer'");

            var manufacturerName = $"Test Manufacturer {Guid.NewGuid()}";
            await page.GetByLabel("Name").FillAsync($"{manufacturerName}");
            await page.ClickAsync("div[class*='mud-select'] div[class*='mud-input-control-input-container']");
            await page.ClickAsync("div.mud-popover div.mud-list-item:has-text('Active')");

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Create Manufacturer page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

            Assert.Equal(initialCount + 1, _context.Manufacturers.Count());

            manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name == manufacturerName);
            Assert.NotNull(manufacturer);
            Assert.Equal(manufacturerName, manufacturer.Name);
            Assert.Equal((int)Enums.StatusEnum.Active, manufacturer.StatusId);
        }
        finally
        {
            if (manufacturer != null)
            {
                ManufacturerHelper.RemoveManufacturer(manufacturer.ManufacturerId, _context);
            }
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CanEditManufacturer()
    {
        var manufacturerId = ManufacturerHelper.AddManufacturer(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/manufacturer/edit/{manufacturerId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Manufacturer'");

            var updatedManufacturerName = $"Updated Manufacturer {Guid.NewGuid()}";
            await page.GetByLabel("Name").FillAsync(updatedManufacturerName);

            await page.ClickAsync("div[class*='mud-select'] div[class*='mud-input-control-input-container']");
            await page.ClickAsync("div.mud-popover div.mud-list-item:has-text('Inactive')");

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Edit Manufacturer page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Manufacturers'");

            var updatedManufacturer = await WaitForManufacturerUpdate(manufacturerId, updatedManufacturerName, (int)Enums.StatusEnum.Inactive, 2_000);
            Assert.NotNull(updatedManufacturer);
            Assert.Equal(updatedManufacturerName, updatedManufacturer.Name);
            Assert.Equal((int)Enums.StatusEnum.Inactive, updatedManufacturer.StatusId);
        }
        finally
        {
            ManufacturerHelper.RemoveManufacturer(manufacturerId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    private async Task<ManufacturerModel?> WaitForManufacturerUpdate(int manufacturerId, string expectedName, int expectedStatusId, int timeoutMs)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            var manufacturer = await _context.Manufacturers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ManufacturerId == manufacturerId);
            if (manufacturer != null && manufacturer.Name == expectedName && manufacturer.StatusId == expectedStatusId)
                return manufacturer;
            await Task.Delay(100);
        }
        return await _context.Manufacturers.FindAsync(manufacturerId);
    }
}
