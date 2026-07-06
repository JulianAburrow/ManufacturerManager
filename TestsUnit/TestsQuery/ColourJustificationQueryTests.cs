using TestsUnit.Helpers;

namespace TestsUnit.TestsQuery;

public class ColourJustificationQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IColourJustificationQueryHandler _colourJustificationQueryHandler;
    private readonly List<ColourJustificationModel> _testColourJustifications = ColourJustificationObjectFactory.GetTestColourJustifications();

    public ColourJustificationQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _colourJustificationQueryHandler = new ColourJustificationQueryHandler(_factory);
    }

    [Fact]
    public async Task GetColourJustification_GetsColourJustification()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[0]);
        _manufacturerManagerContext.SaveChanges();

        var returnedColourJustification = await _colourJustificationQueryHandler.GetColourJustificationAsync(_testColourJustifications[0].ColourJustificationId);
        returnedColourJustification.Should().NotBeNull();
        returnedColourJustification.Justification.Should().Be(_testColourJustifications[0].Justification);
    }

    [Fact]
    public async Task GetColourJustifications_GetsColourJustifications()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.ColourJustifications.Count();

        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[0]);
        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[1]);
        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[2]);
        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[3]);
        _manufacturerManagerContext.SaveChanges();

        var colourJustifications = await _colourJustificationQueryHandler.GetColourJustificationsAsync();

        colourJustifications.Count.Should().Be(initialCount + 4);
    }
}
