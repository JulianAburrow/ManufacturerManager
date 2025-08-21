namespace TestsUnit.TestsCommand;

public class ManufacturerCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IManufacturerCommandHandler _manufacturerCommandHandler;
    private readonly IManufacturerQueryHandler _manufacturerQueryHandler;
    private readonly List<ManufacturerModel> _testManufacturers = ManufacturerObjectFactory.GetTestManufacturers();

    public ManufacturerCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _manufacturerCommandHandler = new ManufacturerCommandHandler(_manufacturerManagerContext);
        _manufacturerQueryHandler = new ManufacturerQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task CreateManufacturerCreatesManufacturer()
    {
        var initialCount = _manufacturerManagerContext.Manufacturers.Count();

        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[0], false);
        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[1], false);
        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[2], false);
        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[3], true);

        _manufacturerManagerContext.Manufacturers.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteManufacturerDeletesManufacturer()
    {
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[0]);
        _manufacturerManagerContext.SaveChanges();
        var manufacturerId = _testManufacturers[0].ManufacturerId;
        await _manufacturerCommandHandler.DeleteManufacturerAsync(manufacturerId, true);

        Func<Task> act = async () => await _manufacturerQueryHandler.GetManufacturerAsync(manufacturerId);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SetManufacturerInactiveSetsWidgetsForManufacturerInactive()
    {
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[0]);
        _manufacturerManagerContext.SaveChanges();
        var widget1 = new WidgetModel
        {
            Name = "Widget1",
            ManufacturerId = _testManufacturers[0].ManufacturerId,
            ColourId = 1,
            StatusId = (int)PublicEnums.WidgetStatusEnum.Active
        };
        _manufacturerManagerContext.Widgets.Add(widget1);
        _manufacturerManagerContext.SaveChanges();
        _testManufacturers[0].StatusId = (int)PublicEnums.ManufacturerStatusEnum.Inactive;
        await _manufacturerCommandHandler.UpdateManufacturerAsync(_testManufacturers[0], true);
        var updatedWidgets = _manufacturerManagerContext.Widgets.Where(w => w.WidgetId == widget1.WidgetId);
        foreach (var updatedWidget in updatedWidgets)
        {
            updatedWidget.StatusId.Should().Be((int)PublicEnums.WidgetStatusEnum.Inactive);
        }
    }

    [Fact]
    public async Task UpdateManufacturerUpdatesManufacturer()
    {
        var newManufacturer = "AceWidgets";

        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[2]);
        _manufacturerManagerContext.SaveChanges();

        var manufacturerToUpdate = _manufacturerManagerContext.Manufacturers.First(m => m.ManufacturerId == _testManufacturers[2].ManufacturerId);
        manufacturerToUpdate.Name = newManufacturer;
        await _manufacturerCommandHandler.UpdateManufacturerAsync(manufacturerToUpdate, true);

        var updatedManufacturer = _manufacturerManagerContext.Manufacturers.First(m => m.ManufacturerId == _testManufacturers[2].ManufacturerId);
        updatedManufacturer.Name.Should().Be(newManufacturer);
    }
}
