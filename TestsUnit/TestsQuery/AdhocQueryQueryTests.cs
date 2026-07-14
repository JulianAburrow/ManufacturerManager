namespace TestsUnit.TestsQuery;

public class AdhocQueryQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IAdhocQueryQueryHandler _adhocQueryQueryHandler;
    private readonly List<AdhocQueryModel> _testAdhocQueries = AdhocQueryObjectFactory.GetTestAdhocQueries();

    public AdhocQueryQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _adhocQueryQueryHandler = new AdhocQueryQueryHandler(_factory);
    }

    [Fact]
    public async Task GetAdhocQueries_GetsAdhocQueries()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.AdhocQueries.Count();

        _manufacturerManagerContext.AdhocQueries.Add(_testAdhocQueries[0]);
        _manufacturerManagerContext.AdhocQueries.Add(_testAdhocQueries[1]);
        _manufacturerManagerContext.AdhocQueries.Add(_testAdhocQueries[2]);
        _manufacturerManagerContext.SaveChanges();

        var adhocQueries = await _adhocQueryQueryHandler.GetAdhocQueriesAsync();

        adhocQueries.Count.Should().Be(initialCount + 3);
    }

    [Fact]
    public async Task GetAdhocQuery_GetsAdhocQuery()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();

        _manufacturerManagerContext.AdhocQueries.Add(_testAdhocQueries[0]);
        _manufacturerManagerContext.SaveChanges();

        var returnedAdhocQuery = await _adhocQueryQueryHandler.GetAdhocQueryAsync(_testAdhocQueries[0].AdhocQueryId);
        returnedAdhocQuery.Should().NotBeNull();
        returnedAdhocQuery.NaturalLanguageQuery.Should().Be(_testAdhocQueries[0].NaturalLanguageQuery);
    }

    [Fact]
    public async Task GetLastXSuccessfulAdhocQueries_ReturnsLastXDistinctQueries_InDescendingOrder()
    {
        // Arrange
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.AdhocQueries.Count();

        // Add the test data (with unique timestamps + a duplicate)
        foreach (var q in _testAdhocQueries)
            _manufacturerManagerContext.AdhocQueries.Add(q);

        _manufacturerManagerContext.SaveChanges();

        // Act
        var adhocQueries = await _adhocQueryQueryHandler.GetLastXSuccessfulAdhocQueries(2);

        // Assert
        adhocQueries.Count.Should().Be(2);

        // Because DISTINCT keeps the *first* occurrence in the ordered list,
        // and the newest "Test Adhoc Query 2" is the most recent overall,
        // the expected order is:
        //
        // 1. "Test Adhoc Query 2"  (the newest one, WhenRun = now - 0.5s)
        // 2. "Test Adhoc Query 3"  (WhenRun = now - 1s)

        adhocQueries[0].NaturalLanguageQuery.Should().Be("Test Adhoc Query 2");
        adhocQueries[1].NaturalLanguageQuery.Should().Be("Test Adhoc Query 3");
    }

    [Fact]
    public async Task GetLastXSuccessfulAdhocQueries_IgnoresUnsuccessfulQueries()
    {
        // Arrange
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var now = DateTime.UtcNow;

        var successful = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Success",
            IsSuccessful = true,
            WhenRun = now.AddSeconds(-1)
        };

        var unsuccessful = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Failure",
            IsSuccessful = false,
            WhenRun = now
        };

        _manufacturerManagerContext.AdhocQueries.Add(successful);
        _manufacturerManagerContext.AdhocQueries.Add(unsuccessful);
        _manufacturerManagerContext.SaveChanges();

        // Act
        var result = await _adhocQueryQueryHandler.GetLastXSuccessfulAdhocQueries(5);

        // Assert
        result.Count.Should().Be(1);
        result[0].NaturalLanguageQuery.Should().Be("Success");
    }

    [Fact]
    public async Task GetLastXSuccessfulAdhocQueries_ReturnsAll_WhenFewerThanRequested()
    {
        // Arrange
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        _manufacturerManagerContext.AdhocQueries.Add(_testAdhocQueries[0]);
        _manufacturerManagerContext.SaveChanges();

        // Act
        var result = await _adhocQueryQueryHandler.GetLastXSuccessfulAdhocQueries(5);

        // Assert
        result.Count.Should().Be(1);
        result[0].NaturalLanguageQuery.Should().Be(_testAdhocQueries[0].NaturalLanguageQuery);
    }

    [Fact]
    public async Task GetLastXSuccessfulAdhocQueries_ReturnsSingleItem_WhenAllQueriesAreDuplicates()
    {
        // Arrange
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var now = DateTime.UtcNow;

        var q1 = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Duplicate",
            IsSuccessful = true,
            WhenRun = now.AddSeconds(-3)
        };

        var q2 = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Duplicate",
            IsSuccessful = true,
            WhenRun = now.AddSeconds(-2)
        };

        var q3 = new AdhocQueryModel
        {
            NaturalLanguageQuery = "Duplicate",
            IsSuccessful = true,
            WhenRun = now.AddSeconds(-1)
        };

        _manufacturerManagerContext.AdhocQueries.AddRange(q1, q2, q3);
        _manufacturerManagerContext.SaveChanges();

        // Act
        var result = await _adhocQueryQueryHandler.GetLastXSuccessfulAdhocQueries(5);

        // Assert
        result.Count.Should().Be(1);
        result[0].NaturalLanguageQuery.Should().Be("Duplicate");
    }

    [Fact]
    public async Task GetAdhocQuery_ReturnsEmptyModel_WhenNotFound()
    {
        var returnedAdhocQuery = await _adhocQueryQueryHandler.GetAdhocQueryAsync(-1);
        returnedAdhocQuery.Should().NotBeNull();
        returnedAdhocQuery.Should().BeOfType<AdhocQueryModel>();
        returnedAdhocQuery.AdhocQueryId.Should().Be(0);
        returnedAdhocQuery.NaturalLanguageQuery.Should().BeNullOrEmpty();
    }
}
