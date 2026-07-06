namespace TestsUnit.TestsQuery;

public class WidgetQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IWidgetQueryHandler _widgetHandler;
    private readonly List<WidgetModel> _testWidgets = WidgetObjectFactory.GetTestWidgets();

    public WidgetQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _widgetHandler = new WidgetQueryHandler(_factory);
    }

    [Fact]
    public async Task GetWidget_GetsWidget()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        _manufacturerManagerContext.Widgets.Add(_testWidgets[1]);
        _manufacturerManagerContext.SaveChanges();

        var returnedWidget = await _widgetHandler.GetWidgetAsync(_testWidgets[1].WidgetId);
        returnedWidget.Should().NotBeNull();
        Assert.Equal(_testWidgets[1].Name, returnedWidget.Name);
        Assert.Equal(_testWidgets[1].Manufacturer.Name, returnedWidget.Manufacturer.Name);
    }

    [Fact]
    public async Task GetWidgets_GetsWidgets()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();

        var initialCount = _manufacturerManagerContext.Widgets.Count();

        _manufacturerManagerContext.Widgets.Add(_testWidgets[0]);
        _manufacturerManagerContext.Widgets.Add(_testWidgets[1]);
        _manufacturerManagerContext.SaveChanges();

        var widgetsReturned = await _widgetHandler.GetWidgetsAsync();

        widgetsReturned.Count.Should().Be(initialCount + 2);
    }
}