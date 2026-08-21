namespace TestsUnit.Helpers;

public static class AdhocQueryObjectFactory
{
    public static List<AdhocQueryModel> GetTestAdhocQueries()
    {
        var now = DateTime.UtcNow;
        return
        [
            new AdhocQueryModel
            {
                NaturalLanguageQuery = "Test Adhoc Query 1",
                Message = "This is a test adhoc query.",
                SqlReturned = "SELECT * FROM TestTable WHERE Id = 1",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-3),
            },
            new AdhocQueryModel
            {
                NaturalLanguageQuery = "Test Adhoc Query 2",
                Message = "This is another test adhoc query.",
                SqlReturned = "SELECT * FROM TestTable WHERE Id = 2",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-2),
            },
            new AdhocQueryModel
            {
                NaturalLanguageQuery = "Test Adhoc Query 3",
                Message = "This is yet another test adhoc query.",
                SqlReturned = "SELECT * FROM TestTable WHERE Id = 3",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-1),
            },
            new AdhocQueryModel
            {
                NaturalLanguageQuery = "Test Adhoc Query 2",
                Message = "This is a test adhoc query.",
                SqlReturned = "SELECT * FROM TestTable WHERE Id = 2",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-0.5)
            },
        ];
    }
}