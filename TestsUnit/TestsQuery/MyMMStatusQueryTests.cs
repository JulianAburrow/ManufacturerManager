namespace TestsUnit.TestsQuery;

public class MyMMStatusQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IMyMMStatusQueryHandler _myMMStatusHandler;
    private readonly List<MyMMStatusModel> _testMyMMStatuses = MyMMStatusObjectFactory.GetTestMyMMStatuses();
    
    public MyMMStatusQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _myMMStatusHandler = new MyMMStatusQueryHandler(_factory);
    }

    [Fact]
    public async Task GetMyMMStatuses_GetsMyMMStatuses()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.MyMMStatuses.Count();

        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[1]);
        _manufacturerManagerContext.SaveChanges();

        var myMMStatusesReturned = await _myMMStatusHandler.GetMyMMStatusesAsync();

        myMMStatusesReturned.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task GetMyMMStatus_GetsMyMMStatus()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();

        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
        _manufacturerManagerContext.SaveChanges();
        var myMMStatusId = _testMyMMStatuses[0].StatusId;

        var myMMStatusReturned = await _myMMStatusHandler.GetMyMMStatusAsync(myMMStatusId);

        myMMStatusReturned.Should().NotBeNull();
        myMMStatusReturned.StatusId.Should().Be(myMMStatusId);
    }

    [Fact]
    public async Task GetMyMMStatusAsync_ReturnsEmptyModel_WhenNotFound()
    {
        // Act
        var returnedStatus = await _myMMStatusHandler.GetMyMMStatusAsync(-1);

        // Assert
        returnedStatus.Should().NotBeNull();
        returnedStatus.Should().BeOfType<MyMMStatusModel>();
        returnedStatus.StatusId.Should().Be(0);
        returnedStatus.StatusName.Should().BeNullOrEmpty();
    }

}
