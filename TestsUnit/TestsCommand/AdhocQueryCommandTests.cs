namespace TestsUnit.TestsCommand;

public class AdhocQueryCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IAdhocQueryCommandHandler _adhocQueryCommandHandler;
    private readonly List<AdhocQueryModel> _testAdhocQueries = AdhocQueryObjectFactory.GetTestAdhocQueries();

    public AdhocQueryCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _adhocQueryCommandHandler = new AdhocQueryCommandHandler(_factory);
    }

    [Fact]
    public async Task CreateAdhocQuery_CreatesAdhocQuery()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.AdhocQueries.Count();

        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[0]);
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[1]);
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[2]);
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(_testAdhocQueries[3]);

        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteAdhocQuery_DeletesAdhocQuery()
    {
        int adhocQueryId;

        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        _manufacturerManagerContext.AdhocQueries.Add(_testAdhocQueries[1]);
        _manufacturerManagerContext.SaveChanges();
        adhocQueryId = _testAdhocQueries[1].AdhocQueryId;

        await _adhocQueryCommandHandler.DeleteAdhocQueryAsync(adhocQueryId);

        var deletedAdhocQuery = _manufacturerManagerContext.AdhocQueries.FirstOrDefault(a => a.AdhocQueryId == adhocQueryId);

        deletedAdhocQuery.Should().BeNull();
    }

    [Fact]
    public async Task CreateAdhocQueryAsync_AddsEntity_AndSavesWhenRequested()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();

        var model = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Test",
            IsSuccessful = true,
            WhenRun = DateTime.UtcNow
        };

        // Act
        await _adhocQueryCommandHandler.CreateAdhocQueryAsync(model);

        // Assert
        _manufacturerManagerContext.AdhocQueries.Count().Should().Be(1);
    }

    [Fact]
    public async Task DeleteAdhocQueryAsync_DeletesEntity_AndSaves()
    {
        // Arrange
        var handler = new AdhocQueryCommandHandler(_factory);

        // Seed the database
        await using (var seedContext = await _factory.CreateDbContextAsync())
        {
            var model = _testAdhocQueries[0];
            seedContext.AdhocQueries.Add(model);
            await seedContext.SaveChangesAsync();
        }

        // Act
        await handler.DeleteAdhocQueryAsync(_testAdhocQueries[0].AdhocQueryId);

        // Assert
        await using (var assertContext = await _factory.CreateDbContextAsync())
        {
            assertContext.AdhocQueries.Count().Should().Be(0);
        }
    }

    [Fact]
    public async Task DeleteAdhocQueryAsync_DoesNothing_WhenEntityDoesNotExist()
    {
        // Arrange
        var handler = new AdhocQueryCommandHandler(_factory);

        // Act
        await handler.DeleteAdhocQueryAsync(9999);

        // Assert
        await using (var assertContext = await _factory.CreateDbContextAsync())
        {
            assertContext.AdhocQueries.Count().Should().Be(0);
        }
    }
}
