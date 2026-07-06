namespace TestsUnit.TestsCommand;

public class ColourCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IColourCommandHandler _colourCommandHandler;
    private readonly List<ColourModel> _testColours = ColourObjectFactory.GetTestColours();

    public ColourCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _colourCommandHandler = new ColourCommandHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task CreateColour_CreatesColour()
    {
        var initialCount = _manufacturerManagerContext.Colours.Count();

        await _colourCommandHandler.CreateColourAsync(_testColours[0]);
        await _colourCommandHandler.CreateColourAsync(_testColours[1]);
        await _colourCommandHandler.CreateColourAsync(_testColours[2]);
        await _colourCommandHandler.CreateColourAsync(_testColours[3]);

        _manufacturerManagerContext.Colours.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteColour_DeletesColour()
    {
        int colourId;

        _manufacturerManagerContext.Colours.Add(_testColours[1]);
        _manufacturerManagerContext.SaveChanges();
        colourId = _testColours[1].ColourId;

        await _colourCommandHandler.DeleteColourAsync(colourId);

        var deletedColour = _manufacturerManagerContext.Colours.FirstOrDefault(c => c.ColourId == colourId);

        deletedColour.Should().BeNull();
    }

    [Fact]
    public async Task UpdateColour_UpdatesColour()
    {
        var newColourName = "NewColour";

        _manufacturerManagerContext.Colours.Add(_testColours[0]);
        _manufacturerManagerContext.SaveChanges();

        var colourToUpdate = _manufacturerManagerContext.Colours.First(c => c.ColourId == _testColours[0].ColourId);
        colourToUpdate.Name = newColourName;
        await _colourCommandHandler.UpdateColourAsync(colourToUpdate);

        var updatedColour = _manufacturerManagerContext.Colours.First(c => c.ColourId == _testColours[0].ColourId);
        updatedColour.Name.Should().Be(newColourName);
    }
}
