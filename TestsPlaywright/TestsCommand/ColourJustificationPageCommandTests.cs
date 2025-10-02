namespace TestsPlaywright.TestsCommand;

public class ColourJustificationPageCommandTests : BaseTestClass
{
    [Fact]
    public async Task CanCreateColourJustification()
    {
        var colourJustification = new ColourJustificationModel();
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            var initialCount = _context.ColourJustifications.Count();

            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/create", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Create Colour Justification'");

            var colourJustificationJustification = $"Colour Justification {Guid.NewGuid()}";
            await page.GetByLabel("Justification").FillAsync(colourJustificationJustification);

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Create Colour Justification page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            Assert.Equal(initialCount + 1, _context.ColourJustifications.Count());

            colourJustification = await _context.ColourJustifications.FirstOrDefaultAsync(c => c.Justification == colourJustificationJustification);
            Assert.NotNull(colourJustification);
            Assert.Equal(colourJustificationJustification, colourJustification.Justification);
        }
        finally
        {
            if (colourJustification != null)
            {
                ColourJustificationHelper.RemoveColourJustification(colourJustification.ColourJustificationId, _context);
            }
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CanEditColourJustification()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/edit/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Colour Justification'");

            var updatedColourJustificationJustification = $"Updated Colour Justification {Guid.NewGuid()}";
            await page.GetByLabel("Justification").FillAsync(updatedColourJustificationJustification);

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Edit Colour Justification page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var updatedColourJustification = await WaitForColourJustificationUpdate(colourJustificationId, updatedColourJustificationJustification);
            Assert.NotNull(updatedColourJustification);
            Assert.Equal(updatedColourJustificationJustification, updatedColourJustification.Justification);
        }
        finally
        {
            ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CanDeleteColourJustification()
    {
        var colourJustificationId = ColourJustificationHelper.AddColourJustification(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();
        var shouldReattemptDelete = false;

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/colourjustification/delete/{colourJustificationId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Colour Justification'");

            var deleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
            if (await deleteButton.CountAsync() == 0)
            {
                deleteButton = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteButton.CountAsync() > 0, "Delete button not found on Delete Colour Justification page.");
            }
            await deleteButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Colour Justifications'");

            var deletedColourJustification = await _context.ColourJustifications
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ColourJustificationId == colourJustificationId);
            Assert.Null(deletedColourJustification);

            if (deletedColourJustification != null)
            {
                shouldReattemptDelete = true;
            }
        }
        finally
        {
            if (shouldReattemptDelete)
            {
                ColourJustificationHelper.RemoveColourJustification(colourJustificationId, _context);
            }
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    private async Task<ColourJustificationModel?> WaitForColourJustificationUpdate(int colourJustificationId, string expectedJustification)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < GlobalValues.GlobalTimeOut)
        {
            var colourJustification = await _context.ColourJustifications
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ColourJustificationId == colourJustificationId);
            if (colourJustification != null && colourJustification.Justification == expectedJustification)
            {
                return colourJustification;
            }
            await Task.Delay(100);
        }
        return await _context.ColourJustifications.FindAsync(colourJustificationId);
    }
}
