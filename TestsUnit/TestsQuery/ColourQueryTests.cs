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
    public async Task GetColourAsync_ReturnsColour_WhenFound()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.Colours.Add(_testColours[3]);
            await context.SaveChangesAsync();
        }

        var id = _testColours[3].ColourId;

        // Act
        var returnedColour = await _colourQueryHandler.GetColourAsync(id);

        // Assert
        returnedColour.Should().NotBeNull();
        returnedColour.ColourId.Should().Be(id);
        returnedColour.Name.Should().Be(_testColours[3].Name);
    }

    [Fact]
    public async Task GetColourAsync_ReturnsEmptyModel_WhenNotFound()
    {
        // Act
        var returnedColour = await _colourQueryHandler.GetColourAsync(-1);

        // Assert
        returnedColour.Should().NotBeNull();
        returnedColour.Should().BeOfType<ColourModel>();
        returnedColour.ColourId.Should().Be(0);
        returnedColour.Name.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetColoursAsync_ReturnsAllColours()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.Colours.AddRange(_testColours);
            await context.SaveChangesAsync();
        }

        // Act
        var colours = await _colourQueryHandler.GetColoursAsync();

        // Assert
        colours.Should().NotBeNull();
        colours.Should().HaveCount(_testColours.Count);
    }
}
