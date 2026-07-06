namespace TestsUnit.Helpers;

public static class TestsUnitHelper
{
    public static IDbContextFactory<ManufacturerManagerContext> GetInMemoryFactory()
    {
        var options = new DbContextOptionsBuilder<ManufacturerManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PooledDbContextFactory<ManufacturerManagerContext>(options);
    }
}
