namespace TestsUnit.Helpers;

public static class ErrorObjectFactory
{
    public static List<Exception> GetTestExceptions()
    {
        return
        [
            new("Exception1", new Exception("InnerException1")),
            new("Exception2", new Exception("InnerException2")),
        ];
    }

    public static List<ErrorModel> GetTestErrors()
    {
        return
        [
            new ErrorModel
            {
                ErrorDate = DateTime.Now,
                ErrorMessage = "Error1",
                Exception = "Exception1",
                InnerException = "InnerException1",
                StackTrace = "This is the stack trace for Error1",
                Resolved = false,
                ResolvedDate = null,
            },
            new ErrorModel
            {
                ErrorDate = DateTime.Now,
                ErrorMessage = "Error2",
                Exception = "Exception2",
                InnerException = "InnerException2",
                StackTrace = "This is the stack trace for Error2",
                Resolved = false,
                ResolvedDate = null,
            }
        ];
    }
}
