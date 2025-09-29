namespace TestsUnit.TestsCommand;

public class ColourJustificationCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IColourJustificationCommandHandler _colourJustificationCommandHandler;
    private readonly List<ColourJustificationModel> _testColourJustifications = ColourJustificationObjectFactory.GetTestColourJustifications();

    public ColourJustificationCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _colourJustificationCommandHandler = new ColourJustificationCommandHandler(_manufacturerManagerContext);
    }

    

    [Fact]
    public async Task CreateColourJustificationCreatesColourJustification()
    {
        var initialCount = _manufacturerManagerContext.ColourJustifications.Count();

        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[0], false);
        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[1], false);
        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[2], false);
        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[3], true);

        _manufacturerManagerContext.ColourJustifications.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteColourJustificationDeletesColourJustification()
    {
        int colourJustificationId;

        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[2]);
        _manufacturerManagerContext.SaveChanges();
        colourJustificationId = _testColourJustifications[2].ColourJustificationId;

        await _colourJustificationCommandHandler.DeleteColourJustificationAsync(colourJustificationId, true);

        var deletedColourJustification = _manufacturerManagerContext.ColourJustifications.FirstOrDefault(c => c.ColourJustificationId == colourJustificationId);

        deletedColourJustification.Should().BeNull();
    }

    [Fact]
    public async Task UpdateColourJustificationUpdatesColourJustification()
    {
        var newJustification = "newJustification";

        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[3]);
        _manufacturerManagerContext.SaveChanges();

        var colourJustificationToUpdate = _manufacturerManagerContext.ColourJustifications.First(c => c.ColourJustificationId == _testColourJustifications[3].ColourJustificationId);
        colourJustificationToUpdate.Justification = newJustification;
        await _colourJustificationCommandHandler.UpdateColourJustificationAsync(_testColourJustifications[3], true);

        var updatedColourJustification = _manufacturerManagerContext.ColourJustifications.First(c => c.ColourJustificationId == _testColourJustifications[3].ColourJustificationId);
        updatedColourJustification.Justification.Should().Be(newJustification);
    }
}
