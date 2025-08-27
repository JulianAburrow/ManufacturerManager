namespace MMUserInterface.Interfaces;

public interface ICrudWithErrorHandlingHelper
{
    Task<bool> ExecuteWithErrorHandling(
        Func<Task> action,
        string successMessage,
        string errorMessage);
}
