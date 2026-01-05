namespace TestsUnit.TestsCommand;

public class MyMMStatusCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IMyMMStatusCommandHandler _myMMStatusCommandHandler;
    private readonly IMyMMStatusQueryHandler _myMMStuStatusQueryHandler;
    private readonly List<MyMMStatusModel> _testMyMMStatuses = MyMMStatusObjectFactory.GetTestMyMMStatuses();

    public MyMMStatusCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _myMMStatusCommandHandler = new MyMMStatusCommandHandler(_manufacturerManagerContext);
        _myMMStuStatusQueryHandler = new MyMMStatusQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task CreateMyMMStatusCreatesMyMMStatus()
    {
        var initialCount = _manufacturerManagerContext.MyMMStatuses.Count();

        await _myMMStatusCommandHandler.CreateMyMMStatusAsync(_testMyMMStatuses[0], false);
        await _myMMStatusCommandHandler.CreateMyMMStatusAsync(_testMyMMStatuses[1], true);

        _manufacturerManagerContext.MyMMStatuses.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task DeleteMyMMStatusDeletesMyMMStatus()
    {
        int myMMStatusId;

        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
        _manufacturerManagerContext.SaveChanges();
        myMMStatusId = _testMyMMStatuses[0].StatusId;

        await _myMMStatusCommandHandler.DeleteMyMMStatusAsync(myMMStatusId, true);
        Func<Task> act = async () => await _myMMStuStatusQueryHandler.GetMyMMStatusAsync(myMMStatusId);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateMyMMStatusUpdatesMyMMStatus()
    {
        var newStatusName = "UpdatedStatus";

        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
        _manufacturerManagerContext.SaveChanges();

        var myMMStatusToUpdate = _manufacturerManagerContext.MyMMStatuses.First(m => m.StatusId == _testMyMMStatuses[0].StatusId);
        myMMStatusToUpdate.StatusName = newStatusName;
        await _myMMStatusCommandHandler.UpdateMyMMStatusAsync(myMMStatusToUpdate, true);

        var updatedMyMMStatus = _manufacturerManagerContext.MyMMStatuses.First(m => m.StatusId == _testMyMMStatuses[0].StatusId);
        updatedMyMMStatus.StatusName.Should().Be(newStatusName);
    }
}
