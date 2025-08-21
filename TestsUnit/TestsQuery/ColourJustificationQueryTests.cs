using TestsUnit.Helpers;

namespace TestsUnit.TestsQuery;

public class ColourJustificationQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IColourJustificationQueryHandler _colourJustificationQueryHandler;
    private readonly List<ColourJustificationModel> _testColourJustifications = ColourJustificationObjectFactory.GetTestColourJustifications();

    public ColourJustificationQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _colourJustificationQueryHandler = new ColourJustificationQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetColourJustificationGetsColourJustification()
    {
        _manufacturerManagerContext.ColourJustifications.Add(_testColourJustifications[0]);
        _manufacturerManagerContext.SaveChanges();

        var returnedColourJustification = await _colourJustificationQueryHandler.GetColourJustificationAsync(_testColourJustifications[0].ColourJustificationId);
        returnedColourJustification.Should().NotBeNull();
        returnedColourJustification.Justification.Should().Be(_testColourJustifications[0].Justification);
    }

    [Fact]
    public async Task GetColourJustificationsGetsColourJustifications()
    {
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
