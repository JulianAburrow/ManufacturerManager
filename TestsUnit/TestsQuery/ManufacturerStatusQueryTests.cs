namespace TestsUnit.TestsQuery;

public class ManufacturerStatusQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IManufacturerStatusQueryHandler _manufacturerStatusHandler;
    private readonly List<ManufacturerStatusModel> _testManufacturerStatuses = ManufacturerStatusObjectFactory.GetTestManufacturerStatuses();

    public ManufacturerStatusQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _manufacturerStatusHandler = new ManufacturerStatusQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetManufacturerStatusesGetsManufacturerStatuses()
    {
        var initialCount = _manufacturerManagerContext.ManufacturerStatuses.Count();

        _manufacturerManagerContext.ManufacturerStatuses.Add(_testManufacturerStatuses[0]);
        _manufacturerManagerContext.ManufacturerStatuses.Add(_testManufacturerStatuses[1]);
        _manufacturerManagerContext.SaveChanges();

        var manufacturerStatusesReturned = await _manufacturerStatusHandler.GetManufacturerStatusesAsync();

        manufacturerStatusesReturned.Count.Should().Be(initialCount + 2);
    }
}
