namespace TestsUnit.TestsCommand;

public class MyMMStatusCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IMyMMStatusCommandHandler _myMMStatusCommandHandler;
    private readonly IMyMMStatusQueryHandler _myMMStuStatusQueryHandler;
    private readonly List<MyMMStatusModel> _testMyMMStatuses = MyMMStatusObjectFactory.GetTestMyMMStatuses();

    public MyMMStatusCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _myMMStatusCommandHandler = new MyMMStatusCommandHandler(_factory);
        _myMMStuStatusQueryHandler = new MyMMStatusQueryHandler(_factory);
    }

    [Fact]
    public async Task CreateMyMMStatus_CreatesMyMMStatus()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.MyMMStatuses.Count();

        await _myMMStatusCommandHandler.CreateMyMMStatusAsync(_testMyMMStatuses[0]);
        await _myMMStatusCommandHandler.CreateMyMMStatusAsync(_testMyMMStatuses[1]);

        _manufacturerManagerContext.MyMMStatuses.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task DeleteMyMMStatus_DeletesMyMMStatus()
    {
        await using (var _manufacturerManagerContext = await _factory.CreateDbContextAsync())
        {
            _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
            await _manufacturerManagerContext.SaveChangesAsync();
        }

        var myMMStatusId = _testMyMMStatuses[0].StatusId;
        await _myMMStatusCommandHandler.DeleteMyMMStatusAsync(myMMStatusId);

        Func<Task> act = async () => await _myMMStuStatusQueryHandler.GetMyMMStatusAsync(myMMStatusId);
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateMyMMStatus_UpdatesMyMMStatus()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var newStatusName = "UpdatedStatus";

        _manufacturerManagerContext.MyMMStatuses.Add(_testMyMMStatuses[0]);
        _manufacturerManagerContext.SaveChanges();

        var myMMStatusToUpdate = _manufacturerManagerContext.MyMMStatuses.First(m => m.StatusId == _testMyMMStatuses[0].StatusId);
        myMMStatusToUpdate.StatusName = newStatusName;
        await _myMMStatusCommandHandler.UpdateMyMMStatusAsync(myMMStatusToUpdate);

        var updatedMyMMStatus = _manufacturerManagerContext.MyMMStatuses.First(m => m.StatusId == _testMyMMStatuses[0].StatusId);
        updatedMyMMStatus.StatusName.Should().Be(newStatusName);
    }
}
