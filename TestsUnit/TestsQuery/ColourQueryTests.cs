namespace TestsUnit.TestsQuery;

public class ColourQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IColourQueryHandler _colourQueryHandler;
    private readonly List<ColourModel> _testColours = ColourObjectFactory.GetTestColours();

    public ColourQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _colourQueryHandler = new ColourQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetColourGetsColour()
    {
        _manufacturerManagerContext.Colours.Add(_testColours[3]);
        _manufacturerManagerContext.SaveChanges();

        var returnedColour = await _colourQueryHandler.GetColourAsync(_testColours[3].ColourId);
        returnedColour.Should().NotBeNull();
        returnedColour.Name.Should().Be(_testColours[3].Name);
    }

    [Fact]
    public async Task GetColoursGetsColours()
    {
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
