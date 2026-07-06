namespace TestsUnit.TestsQuery;

public class ColourQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IColourQueryHandler _colourQueryHandler;
    private readonly List<ColourModel> _testColours = ColourObjectFactory.GetTestColours();

    public ColourQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _colourQueryHandler = new ColourQueryHandler(_factory);
    }

    [Fact]
    public async Task GetColour_GetsColour()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        _manufacturerManagerContext.Colours.Add(_testColours[3]);
        _manufacturerManagerContext.SaveChanges();

        var returnedColour = await _colourQueryHandler.GetColourAsync(_testColours[3].ColourId);
        returnedColour.Should().NotBeNull();
        returnedColour.Name.Should().Be(_testColours[3].Name);
    }

    [Fact]
    public async Task GetColours_GetsColours()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.Colours.Count();

        _manufacturerManagerContext.Colours.Add(_testColours[0]);
        _manufacturerManagerContext.Colours.Add(_testColours[1]);
        _manufacturerManagerContext.Colours.Add(_testColours[2]);
        _manufacturerManagerContext.Colours.Add(_testColours[3]);
        _manufacturerManagerContext.SaveChanges();

        var colours = await _colourQueryHandler.GetColoursAsync();

        colours.Count.Should().Be(initialCount + 4);
    }
}
