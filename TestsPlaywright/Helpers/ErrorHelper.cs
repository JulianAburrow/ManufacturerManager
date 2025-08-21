using Microsoft.EntityFrameworkCore;

namespace TestsPlaywright.Helpers;

public static class ErrorHelper
{
    public static ErrorModel AddError(ManufacturerManagerContext context)
    {
        var newError = new ErrorModel
        {
            ErrorDate = DateTime.Now,
            ErrorMessage = $"Error {Guid.NewGuid()}",
            Exception = "Test exception",
            InnerException = "Test inner exception",
            StackTrace = "Test stack trace",
            Resolved = false
        };
        context.Errors.Add(newError);
        context.SaveChanges();
        return newError;
    }

    public static void RemoveError(int errorId, ManufacturerManagerContext context)
    {
        var error = context.Errors.Find(errorId);
        if (error != null)
        {
            context.Errors.Remove(error);
            context.SaveChanges();
        }
    }
}
