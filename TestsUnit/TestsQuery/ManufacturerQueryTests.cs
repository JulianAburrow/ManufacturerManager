using TestsUnit.Helpers;

namespace TestsUnit.TestsQuery;

public class ManufacturerQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IManufacturerQueryHandler _manufacturerHandler;
    private readonly List<ManufacturerModel> _testManufacturers = ManufacturerObjectFactory.GetTestManufacturers();

    public ManufacturerQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _manufacturerHandler = new ManufacturerQueryHandler(_factory);
    }

    [Fact]
    public async Task GetManufacturer_GetsManufacturer()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[3]);
        _manufacturerManagerContext.SaveChanges();

        var returnedManufacturer = await _manufacturerHandler.GetManufacturerAsync(_testManufacturers[3].ManufacturerId);
        returnedManufacturer.Should().NotBeNull();
        Assert.Equal(_testManufacturers[3].Name, returnedManufacturer.Name);
    }

    [Fact]
    public async Task GetManufacturers_GetsManufacturers()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.Manufacturers.Count();

        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[0]);
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[1]);
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[2]);
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[3]);
        _manufacturerManagerContext.SaveChanges();

        var manufacturersReturned = await _manufacturerHandler.GetManufacturersAsync();

        manufacturersReturned.Count.Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task GetManufacturer_ReturnsEmptyModel_WhenNotFound()
    {
        var returnedManufacturer = await _manufacturerHandler.GetManufacturerAsync(-1);
        returnedManufacturer.Should().NotBeNull();
        returnedManufacturer.Should().BeOfType<ManufacturerModel>();
        returnedManufacturer.ManufacturerId.Should().Be(0);
        returnedManufacturer.Name.Should().BeNullOrEmpty();
    }
}