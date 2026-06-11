namespace TestsUnit.TestsCommand;

public class AdhocQueryCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IAdhocQueryCommandHandler _adhocQueryCommandHandler;
    private readonly List<AdhocQueryModel> _testAdhocQueries = AdhocQueryObjectFactory.GetTestAdhocQueries();

    public AdhocQueryCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _adhocQueryCommandHandler = new AdhocQueryCommandHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task CreateAdhocQuery_CreatesAdhocQuery()
    {
        var initialCount = _manufacturerManagerContext.AdhocQueries.Count();

        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[0], false);
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[1], false);
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[2], false);
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[3], true);

        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteAdhocQuery_DeletesAdhocQuery()
    {
        int adhocQueryId;
        
        _manufacturerManagerContext.AdhocQueries.Add(_testAdhocQueries[1]);
        _manufacturerManagerContext.SaveChanges();
        adhocQueryId = _testAdhocQueries[1].AdhocQueryId;

        await _adhocQueryCommandHandler.DeleteAdhocQueryAsync(adhocQueryId, true);

        var deletedAdhocQuery = _manufacturerManagerContext.AdhocQueries.FirstOrDefault(a => a.AdhocQueryId == adhocQueryId);

        deletedAdhocQuery.Should().BeNull();
    }

    [Fact]
    public async Task CreateAdhocQueryAsync_AddsEntity_AndSavesWhenRequested()
    {
        var model = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Test",
            IsSuccessful = true,
            WhenRun = DateTime.UtcNow
        };

        // Act
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(model, true);

        // Assert
        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(1);
    }

    [Fact]
    public async Task CreateAdhocQueryAsync_AddsEntity_WithoutSavingWhenRequested()
    {
        var model = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Test",
            IsSuccessful = true,
            WhenRun = DateTime.UtcNow
        };

        // Act
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(model, false);

        // Assert
        _manufacturerManagerContext.AdhocQueries.Local.Count.Should().Be(1);
        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteAdhocQueryAsync_DeletesEntity_AndSavesWhenRequested()
    {
        // Arrange
        var handler = new AdhocQueryCommandHandler(_manufacturerManagerContext);
        var model = _testAdhocQueries[0];
        _manufacturerManagerContext.AdhocQueries.Add(model);
        _manufacturerManagerContext.SaveChanges();

        // Act
        await handler.DeleteAdhocQueryAsync(model.AdhocQueryId, true);

        // Assert
        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteAdhocQueryAsync_DeletesEntity_WithoutSavingWhenRequested()
    {
        // Arrange
        var handler = new AdhocQueryCommandHandler(_manufacturerManagerContext);
        var model = _testAdhocQueries[0];
        _manufacturerManagerContext.AdhocQueries.Add(model);
        _manufacturerManagerContext.SaveChanges();

        // Act
        await handler.DeleteAdhocQueryAsync(model.AdhocQueryId, false);

        // Assert
        _manufacturerManagerContext.AdhocQueries.Local.Count.Should().Be(0); // removed from tracking
        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(1);     // still in DB
    }

    [Fact]
    public async Task DeleteAdhocQueryAsync_DoesNothing_WhenEntityDoesNotExist()
    {
        // Arrange
        var handler = new AdhocQueryCommandHandler(_manufacturerManagerContext);

        // Act
        await handler.DeleteAdhocQueryAsync(9999, true);

        // Assert
        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(0);
    }
}
