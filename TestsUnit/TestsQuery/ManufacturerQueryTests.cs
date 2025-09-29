using TestsUnit.Helpers;

namespace TestsUnit.TestsQuery;

public class ManufacturerQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IManufacturerQueryHandler _manufacturerHandler;
    private readonly List<ManufacturerModel> _testManufacturers = ManufacturerObjectFactory.GetTestManufacturers();

    public ManufacturerQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _manufacturerHandler = new ManufacturerQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetManufacturerGetsManufacturer()
    {
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[3]);
        _manufacturerManagerContext.SaveChanges();

        var returnedManufacturer = await _manufacturerHandler.GetManufacturerAsync(_testManufacturers[3].ManufacturerId);
        returnedManufacturer.Should().NotBeNull();
        Assert.Equal(_testManufacturers[3].Name, returnedManufacturer.Name);
    }

    [Fact]
    public async Task GetManufacturersGetsManufacturers()
    {
        var initialCount = _manufacturerManagerContext.Manufacturers.Count();

        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[0]);
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[1]);
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[2]);
        _manufacturerManagerContext.Manufacturers.Add(_testManufacturers[3]);
        _manufacturerManagerContext.SaveChanges();

        var manufacturersReturned = await _manufacturerHandler.GetManufacturersAsync();

        manufacturersReturned.Count.Should().Be(initialCount + 4);
    }
}