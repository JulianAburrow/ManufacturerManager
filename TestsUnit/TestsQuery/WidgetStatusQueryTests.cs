namespace TestsUnit.TestsQuery;

public class WidgetStatusQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IWidgetStatusQueryHandler _widgetStatusHandler;
    private readonly List<WidgetStatusModel> _testWidgetStatues = WidgetStatusObjectFactory.GetTestWidgetStatuses();

    public WidgetStatusQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _widgetStatusHandler = new WidgetStatusQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetWidgetStatusesGetsWidgetStatuses()
    {
        var initialCount = _manufacturerManagerContext.WidgetStatuses.Count();

        _manufacturerManagerContext.WidgetStatuses.Add(_testWidgetStatues[0]);
        _manufacturerManagerContext.WidgetStatuses.Add(_testWidgetStatues[1]);
        _manufacturerManagerContext.SaveChanges();

        var widgetStatusesReturned = await _widgetStatusHandler.GetWidgetStatusesAsync();

        widgetStatusesReturned.Count().Should().Be(initialCount + 2);
    }
}
