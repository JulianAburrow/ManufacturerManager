namespace MMUserInterface.Helpers;

public class CrudWithErrorHandlingHelper(ISnackbar snackbar, IErrorCommandHandler errorCommandHandler) : ICrudWithErrorHandlingHelper
{
    private readonly ISnackbar _snackbar = snackbar;
    private readonly IErrorCommandHandler _errorCommandHandler = errorCommandHandler;

    public async Task<bool> ExecuteWithErrorHandling(
        Func<Task> action,
        string successMessage,
        string errorMessage)
    {
        var success = false;

        try
        {
            await action();
            _snackbar.Add(successMessage, Severity.Success);
            success = true;
        }
        catch (Exception ex)
        {
            await _errorCommandHandler.CreateErrorAsync(ex, true);
            _snackbar.Add(errorMessage, Severity.Error);
            success = false;
        }

        return success;
    }
}
