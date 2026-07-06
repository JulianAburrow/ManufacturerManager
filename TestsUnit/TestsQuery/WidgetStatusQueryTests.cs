namespace TestsUnit.TestsQuery;

public class WidgetStatusQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IWidgetStatusQueryHandler _widgetStatusHandler;
    private readonly List<WidgetStatusModel> _testWidgetStatues = WidgetStatusObjectFactory.GetTestWidgetStatuses();

    public WidgetStatusQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _widgetStatusHandler = new WidgetStatusQueryHandler(_factory);
    }

    [Fact]
    public async Task GetWidgetStatuses_GetsWidgetStatuses()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.WidgetStatuses.Count();

        _manufacturerManagerContext.WidgetStatuses.Add(_testWidgetStatues[0]);
        _manufacturerManagerContext.WidgetStatuses.Add(_testWidgetStatues[1]);
        _manufacturerManagerContext.SaveChanges();

        var widgetStatusesReturned = await _widgetStatusHandler.GetWidgetStatusesAsync();

        widgetStatusesReturned.Count().Should().Be(initialCount + 2);
    }
}
