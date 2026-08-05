namespace TestsUnit.TestsQuery;

public class ManufacturerStatusQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IManufacturerStatusQueryHandler _manufacturerStatusHandler;
    private readonly List<ManufacturerStatusModel> _testManufacturerStatuses = ManufacturerStatusObjectFactory.GetTestManufacturerStatuses();

    public ManufacturerStatusQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _manufacturerStatusHandler = new ManufacturerStatusQueryHandler(_factory);
    }

    [Fact]
    public async Task GetManufacturerStatuses_GetsManufacturerStatuses()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync(TestContext.Current.CancellationToken);
        var initialCount = _manufacturerManagerContext.ManufacturerStatuses.Count();

        _manufacturerManagerContext.ManufacturerStatuses.Add(_testManufacturerStatuses[0]);
        _manufacturerManagerContext.ManufacturerStatuses.Add(_testManufacturerStatuses[1]);
        await _manufacturerManagerContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var manufacturerStatusesReturned = await _manufacturerStatusHandler.GetManufacturerStatusesAsync();

        manufacturerStatusesReturned.Count.Should().Be(initialCount + 2);
    }
}
