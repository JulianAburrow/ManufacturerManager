using Microsoft.IdentityModel.Tokens;

namespace TestsUnit.TestsCommand;

public class ManufacturerCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IManufacturerCommandHandler _manufacturerCommandHandler;
    private readonly IManufacturerQueryHandler _manufacturerQueryHandler;
    private readonly List<ManufacturerModel> _testManufacturers = ManufacturerObjectFactory.GetTestManufacturers();

    public ManufacturerCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _manufacturerCommandHandler = new ManufacturerCommandHandler(_factory);
        _manufacturerQueryHandler = new ManufacturerQueryHandler(_factory);
    }

    [Fact]
    public async Task CreateManufacturer_CreatesManufacturer()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.Manufacturers.Count();

        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[0]);
        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[1]);
        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[2]);
        await _manufacturerCommandHandler.CreateManufacturerAsync(_testManufacturers[3]);

        _manufacturerManagerContext.Manufacturers.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteManufacturer_DeletesManufacturer()
    {
        await using (var _manufacturerManagerContext = await _factory.CreateDbContextAsync())
        {
            _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[0]);
            await _manufacturerManagerContext.SaveChangesAsync();
        }

        var manufacturerId = _testManufacturers[0].ManufacturerId;
        await _manufacturerCommandHandler.DeleteManufacturerAsync(manufacturerId);

        var returnedManufacturer = await _manufacturerQueryHandler.GetManufacturerAsync(manufacturerId);

        returnedManufacturer.Should().NotBeNull();
        returnedManufacturer.ManufacturerId.Should().Be(0);
        returnedManufacturer.Name.Should().BeNullOrEmpty();
        returnedManufacturer.Widgets.Should().BeNull();
    }

    [Fact]
    public async Task SetManufacturerInactive_SetsWidgetsForManufacturerInactive()
    {
        // Arrange: seed manufacturer and widget using a fresh context
        await using (var seedContext = await _factory.CreateDbContextAsync())
        {
            seedContext.Manufacturers.Add(_testManufacturers[0]);
            await seedContext.SaveChangesAsync();

            var widget1 = new WidgetModel
            {
                Name = "Widget1",
                ManufacturerId = _testManufacturers[0].ManufacturerId,
                ColourId = 1,
                StatusId = (int)PublicEnums.WidgetStatusEnum.Active
            };

            seedContext.Widgets.Add(widget1);
            await seedContext.SaveChangesAsync();
        }

        // Act: update manufacturer (handler uses its own fresh context)
        _testManufacturers[0].StatusId = (int)PublicEnums.ManufacturerStatusEnum.Inactive;
        await _manufacturerCommandHandler.UpdateManufacturerAsync(_testManufacturers[0]);

        // Assert: read using a NEW fresh context
        await using var assertContext = await _factory.CreateDbContextAsync();
        var updatedWidget = assertContext.Widgets
            .Single(w => w.ManufacturerId == _testManufacturers[0].ManufacturerId);

        updatedWidget.StatusId.Should().Be((int)PublicEnums.WidgetStatusEnum.Inactive);
    }

    [Fact]
    public async Task UpdateManufacturer_UpdatesManufacturer()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var newManufacturer = "AceWidgets";

        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[2]);
        _manufacturerManagerContext.SaveChanges();

        var manufacturerToUpdate = _manufacturerManagerContext.Manufacturers.First(m => m.ManufacturerId == _testManufacturers[2].ManufacturerId);
        manufacturerToUpdate.Name = newManufacturer;
        await _manufacturerCommandHandler.UpdateManufacturerAsync(manufacturerToUpdate);

        var updatedManufacturer = _manufacturerManagerContext.Manufacturers.First(m => m.ManufacturerId == _testManufacturers[2].ManufacturerId);
        updatedManufacturer.Name.Should().Be(newManufacturer);
    }
}
