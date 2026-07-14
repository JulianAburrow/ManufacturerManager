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
    public async Task GetColourJustificationAsync_ReturnsColourJustification_WhenFound()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.ColourJustifications.Add(_testColourJustifications[0]);
            await context.SaveChangesAsync();
        }

        var id = _testColourJustifications[0].ColourJustificationId;

        // Act
        var returnedColourJustification = await _colourJustificationQueryHandler.GetColourJustificationAsync(id);

        // Assert
        returnedColourJustification.Should().NotBeNull();
        returnedColourJustification.ColourJustificationId.Should().Be(id);
        returnedColourJustification.Justification.Should().Be(_testColourJustifications[0].Justification);
    }

    [Fact]
    public async Task GetColourJustificationAsync_ReturnsEmptyModel_WhenNotFound()
    {
        // Act
        var returnedColourJustification = await _colourJustificationQueryHandler.GetColourJustificationAsync(-1);

        // Assert
        returnedColourJustification.Should().NotBeNull();
        returnedColourJustification.Should().BeOfType<ColourJustificationModel>();
        returnedColourJustification.ColourJustificationId.Should().Be(0);
        returnedColourJustification.Justification.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetColourJustificationsAsync_ReturnsAllColourJustifications()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.ColourJustifications.AddRange(_testColourJustifications);
            await context.SaveChangesAsync();
        }

        // Act
        var colourJustifications = await _colourJustificationQueryHandler.GetColourJustificationsAsync();

        // Assert
        colourJustifications.Should().NotBeNull();
        colourJustifications.Should().HaveCount(_testColourJustifications.Count);
    }
}
