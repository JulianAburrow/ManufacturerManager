namespace TestsPlaywright.TestsCommand;

public class ColourPageCommandTestsClass : BaseTestClass
{
    [Fact]
    public async Task CanCreateColour()
    {
        var colour = new ColourModel();
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            var initialCount = _context.Colours.Count();

            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/create", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Create Colour'");

            var colourName = "Test Colour 123456";
            await page.GetByLabel("Name").FillAsync(colourName);

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Create Colour page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");

            Assert.Equal(initialCount + 1, _context.Colours.Count());

            colour = await _context.Colours.FirstOrDefaultAsync(c => c.Name == colourName);
            Assert.NotNull(colour);
            Assert.Equal(colourName, colour.Name);
        }
        finally
        {
            if (colour != null)
            {
                ColourHelper.RemoveColour(colour.ColourId, _context);
            }
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CanEditColour()
    {
        var colourId = ColourHelper.AddColour(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/edit/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Colour'");

            var updatedColourName = "Updated Colour 654321";
            await page.GetByLabel("Name").FillAsync(updatedColourName);

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Edit Colour page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var updatedColour = await WaitForColourUpdate(colourId, updatedColourName);
            Assert.NotNull(updatedColour);
            Assert.Equal(updatedColourName, updatedColour.Name);
        }
        finally
        {
            ColourHelper.RemoveColour(colourId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CanDeleteColour()
    {
        var colourId = ColourHelper.AddColour(_context);
        var shouldReattemptDelete = false;
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colour/delete/{colourId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Colour'");

            var deleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
            if (await deleteButton.CountAsync() == 0)
            {
                deleteButton = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteButton.CountAsync() > 0, "Delete button not found on Delete Colour page.");
            }
            await deleteButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colours'");

            var deletedColour = await _context.Colours
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ColourId == colourId);
            Assert.Null(deletedColour);

            if (deletedColour != null)
            {
                shouldReattemptDelete = true;
            }
        }
        finally
        {
            if (shouldReattemptDelete)
            {
                ColourHelper.RemoveColour(colourId, _context);
            }
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    private async Task<ColourModel?> WaitForColourUpdate(int colourId, string expectedName)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < GlobalValues.GlobalTimeOut)
        {
            var colour = await _context.Colours
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ColourId == colourId);
            if (colour != null && colour.Name == expectedName)
            {
                return colour;
            }
            await Task.Delay(100);
        }
        return await _context.Colours.FindAsync(colourId);
    }
}
