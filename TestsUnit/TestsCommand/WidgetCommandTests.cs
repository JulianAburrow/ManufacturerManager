namespace TestsUnit.TestsCommand;

public class WidgetCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IWidgetCommandHandler _widgetCommandHandler;
    private readonly IWidgetQueryHandler _widgetQueryHandler;
    private readonly List<WidgetModel> _testWidgets = WidgetObjectFactory.GetTestWidgets();

    public WidgetCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _widgetCommandHandler = new WidgetCommandHandler(_manufacturerManagerContext);
        _widgetQueryHandler = new WidgetQueryHandler(_manufacturerManagerContext);
    }    

    [Fact]
    public async Task CreateWidgetCreatesWidget()
    {
        var initialCount = _manufacturerManagerContext.Widgets.Count();

        await _widgetCommandHandler.CreateWidgetAsync(_testWidgets[0], false);
        await _widgetCommandHandler.CreateWidgetAsync(_testWidgets[1], true);

        _manufacturerManagerContext.Widgets.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task DeleteWidgetDeletesWidget()
    {
        int widgetId;

        _manufacturerManagerContext.Widgets.Add(_testWidgets[0]);
        _manufacturerManagerContext.SaveChanges();
        widgetId = _testWidgets[0].WidgetId;

        await _widgetCommandHandler.DeleteWidgetAsync(widgetId, true);
        Func<Task> act = async () => await _widgetQueryHandler.GetWidgetAsync(widgetId);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateWidgetUpdatesWidget()
    {
        var newWidget = "AcmeWidget";

        _manufacturerManagerContext.Widgets.Add(_testWidgets[0]);
        _manufacturerManagerContext.SaveChanges();

        var widgetToUpdate = _manufacturerManagerContext.Widgets.First(w => w.WidgetId == _testWidgets[0].WidgetId);
        widgetToUpdate.Name = newWidget;
        await _widgetCommandHandler.UpdateWidgetAsync(widgetToUpdate, true);

        var updatedWidget = _manufacturerManagerContext.Widgets.First(w => w.WidgetId == _testWidgets[0].WidgetId);
        updatedWidget.Name.Should().Be(newWidget);
    }
}
