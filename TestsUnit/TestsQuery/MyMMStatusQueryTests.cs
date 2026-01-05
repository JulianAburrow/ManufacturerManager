namespace TestsUnit.TestsQuery;

public class MyMMStatusQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IMyMMStatusQueryHandler _myMMStatusHandler;
    private readonly List<MyMMStatusModel> _testMyMMStatuses = MyMMStatusObjectFactory.GetTestMyMMStatuses();
    
    public MyMMStatusQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _myMMStatusHandler = new MyMMStatusQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetMyMMStatusesGetsMyMMStatuses()
    {
        var initialCount = _manufacturerManagerContext.MyMMStatuses.Count();

        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[1]);
        _manufacturerManagerContext.SaveChanges();

        var myMMStatusesReturned = await _myMMStatusHandler.GetMMStatusesAsync();

        myMMStatusesReturned.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task GetMyMMStatusGetsMyMMStatus()
    {
        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
        _manufacturerManagerContext.SaveChanges();
        var myMMStatusId = _testMyMMStatuses[0].StatusId;

        var myMMStatusReturned = await _myMMStatusHandler.GetMyMMStatusAsync(myMMStatusId);

        myMMStatusReturned.Should().NotBeNull();
        myMMStatusReturned.StatusId.Should().Be(myMMStatusId);
    }
}
