namespace TestsUnit.TestsCommand;

public class ColourJustificationCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IColourJustificationCommandHandler _colourJustificationCommandHandler;
    private readonly List<ColourJustificationModel> _testColourJustifications = ColourJustificationObjectFactory.GetTestColourJustifications();

    public ColourJustificationCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _colourJustificationCommandHandler = new ColourJustificationCommandHandler(_factory);
    }

    

    [Fact]
    public async Task CreateColourJustification_CreatesColourJustification()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.ColourJustifications.Count();

        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[0]);
        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[1]);
        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[2]);
        await _colourJustificationCommandHandler.CreateColourJustificationAsync(_testColourJustifications[3]);

        _manufacturerManagerContext.ColourJustifications.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteColourJustification_DeletesColourJustification()
    {
        int colourJustificationId;
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();

        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[2]);
        _manufacturerManagerContext.SaveChanges();
        colourJustificationId = _testColourJustifications[2].ColourJustificationId;

        await _colourJustificationCommandHandler.DeleteColourJustificationAsync(colourJustificationId);

        var deletedColourJustification = _manufacturerManagerContext.ColourJustifications.FirstOrDefault(c => c.ColourJustificationId == colourJustificationId);

        deletedColourJustification.Should().BeNull();
    }

    [Fact]
    public async Task UpdateColourJustification_UpdatesColourJustification()
    {
        var newJustification = "newJustification";
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();

        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[3]);
        _manufacturerManagerContext.SaveChanges();

        var colourJustificationToUpdate = _manufacturerManagerContext.ColourJustifications.First(c => c.ColourJustificationId == _testColourJustifications[3].ColourJustificationId);
        colourJustificationToUpdate.Justification = newJustification;
        await _colourJustificationCommandHandler.UpdateColourJustificationAsync(_testColourJustifications[3]);

        var updatedColourJustification = _manufacturerManagerContext.ColourJustifications.First(c => c.ColourJustificationId == _testColourJustifications[3].ColourJustificationId);
        updatedColourJustification.Justification.Should().Be(newJustification);
    }
}
