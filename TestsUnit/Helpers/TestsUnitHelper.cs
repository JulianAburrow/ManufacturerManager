namespace TestsUnit.Helpers;

public static class TestsUnitHelper
{
    public static IDbContextFactory<ManufacturerManagerContext> GetInMemoryFactory()
    {
        var options = BuildOptions();
        return new TestDbContextFactory(options);
    }

    private static DbContextOptions<ManufacturerManagerContext> BuildOptions() =>
        new DbContextOptionsBuilder<ManufacturerManagerContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB per test
            .Options;
}
