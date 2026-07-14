namespace TestsUnit.TestsCommand;

public class WidgetCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IWidgetCommandHandler _widgetCommandHandler;
    private readonly IWidgetQueryHandler _widgetQueryHandler;
    private readonly List<WidgetModel> _testWidgets = WidgetObjectFactory.GetTestWidgets();

    public WidgetCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _widgetCommandHandler = new WidgetCommandHandler(_factory);
        _widgetQueryHandler = new WidgetQueryHandler(_factory);
    }    

    [Fact]
    public async Task CreateWidget_CreatesWidget()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.Widgets.Count();

        await _widgetCommandHandler.CreateWidgetAsync(_testWidgets[0]);
        await _widgetCommandHandler.CreateWidgetAsync(_testWidgets[1]);

        _manufacturerManagerContext.Widgets.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task DeleteWidget_DeletesWidget()
    {
        await using (var _manufacturerManagerContext = await _factory.CreateDbContextAsync())
        {
            _manufacturerManagerContext.Widgets.Add(_testWidgets[0]);
            _manufacturerManagerContext.SaveChanges();
        }
        
        var widgetId = _testWidgets[0].WidgetId;

        await _widgetCommandHandler.DeleteWidgetAsync(widgetId);

        var returnedWidget = await _widgetQueryHandler.GetWidgetAsync(widgetId);
        
        returnedWidget.Should().NotBeNull();
        returnedWidget.WidgetId.Should().Be(0);
        returnedWidget.Name.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task UpdateWidget_UpdatesWidget()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var newWidget = "AcmeWidget";

        _manufacturerManagerContext.Widgets.Add(_testWidgets[0]);
        _manufacturerManagerContext.SaveChanges();

        var widgetToUpdate = _manufacturerManagerContext.Widgets.First(w => w.WidgetId == _testWidgets[0].WidgetId);
        widgetToUpdate.Name = newWidget;
        await _widgetCommandHandler.UpdateWidgetAsync(widgetToUpdate);

        var updatedWidget = _manufacturerManagerContext.Widgets.First(w => w.WidgetId == _testWidgets[0].WidgetId);
        updatedWidget.Name.Should().Be(newWidget);
    }
}
