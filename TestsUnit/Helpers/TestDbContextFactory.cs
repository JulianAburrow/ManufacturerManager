namespace TestsUnit.Helpers;

public class TestDbContextFactory : IDbContextFactory<ManufacturerManagerContext>
{
    private readonly DbContextOptions<ManufacturerManagerContext> _options;

    public TestDbContextFactory(DbContextOptions<ManufacturerManagerContext> options)
    {
        _options = options;
    }

    public ManufacturerManagerContext CreateDbContext()
    {
        return new ManufacturerManagerContext(_options);
    }
}
