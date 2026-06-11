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
                MessageOrSqlReturned = "This is a test adhoc query.",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-3),
            },
            new AdhocQueryModel
            {
                NaturalLanguageQuery = "Test Adhoc Query 2",
                MessageOrSqlReturned = "This is another test adhoc query.",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-2),
            },
            new AdhocQueryModel
            {
                NaturalLanguageQuery = "Test Adhoc Query 3",
                MessageOrSqlReturned = "This is yet another test adhoc query.",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-1),
            },
            new AdhocQueryModel
            {
                NaturalLanguageQuery = "Test Adhoc Query 2",
                IsSuccessful = true,
                WhenRun = now.AddSeconds(-0.5)
            },
        ];
    }
}
