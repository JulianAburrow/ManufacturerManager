using Microsoft.EntityFrameworkCore;

namespace TestsPlaywright.TestsCommand;

public class ErrorPageCommandTests : BaseTestClass
{
    [Fact]
    public async Task CanEditError()
    {
        var newError = ErrorHelper.AddError(_context);
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/edit/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Edit Error'");

            var errorResolvedCheckBox = page.Locator("[data-testid=\"error-resolved-checkbox\"]").First;
            await errorResolvedCheckBox.ClickAsync();

            var resolvedField = page.Locator("[data-testid=\"resolved-date-label\"]").First;
            string fieldText = await resolvedField.InnerTextAsync();
            Assert.False(string.IsNullOrWhiteSpace(fieldText));

            var submitButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
            if (await submitButton.CountAsync() == 0)
            {
                submitButton = page.GetByText("Submit", new() { Exact = false });
                Assert.True(await submitButton.CountAsync() > 0, "Submit button not found on Edit Error page.");
            }
            await submitButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");

            var updatedError = await WaitForErrorUpdate(newError.ErrorId, newError.ErrorMessage);
            Assert.NotNull(updatedError);
            Assert.True(updatedError.Resolved);
            Assert.NotNull(updatedError.ResolvedDate);
        }
        finally
        {
            ErrorHelper.RemoveError(newError.ErrorId, _context);
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    [Fact]
    public async Task CanDeleteError()
    {
        var newError = ErrorHelper.AddError(_context);
        var shouldReattemptDelete = false;
        var page = await PlaywrightTestHelper.CreatePageAsync();

        try
        {
            await page.GotoAsync($"{GlobalValues.BaseUrl}/error/delete/{newError.ErrorId}", GlobalValues.GetPageOptions());
            await page.WaitForFunctionAsync("document.title === 'Delete Error'");

            var deleteButton = page.GetByRole(AriaRole.Button, new() { Name = "Delete" });
            if (await deleteButton.CountAsync() == 0)
            {
                deleteButton = page.GetByText("Delete", new() { Exact = false });
                Assert.True(await deleteButton.CountAsync() > 0, "Delete button not found on Delete Error page.");
            }
            await deleteButton.First.ClickAsync();

            await page.WaitForFunctionAsync("document.title === 'Errors'");

            var deletedError = await _context.Errors
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ErrorId == newError.ErrorId);
            Assert.Null(deletedError);

            if (deletedError != null)
            {
                shouldReattemptDelete = true;
            }
        }
        finally
        {
            if (shouldReattemptDelete)
            {
                ErrorHelper.RemoveError(newError.ErrorId, _context);
            }
            await PlaywrightTestHelper.DisposeBrowserAndContext(page);
        }
    }

    private async Task<ErrorModel?> WaitForErrorUpdate(int errorId, string expectedMessage)
    {
        var sw = new Stopwatch();
        while (sw.ElapsedMilliseconds < GlobalValues.GlobalTimeOut)
        {
            var error = await _context.Errors
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ErrorId == errorId);
            if (error != null && error.ErrorMessage == expectedMessage)
            {
                return error;
            }
            await Task.Delay(100);
        }
        return await _context.Errors.FindAsync(errorId);
    }
}