namespace TestsUnit.TestsCommand;

public class MyMMCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IMyMMCommandHandler _myMMCommandHandler;
    private readonly IMyMMQueryHandler _myMMQueryHandler;
    private readonly List<MyMMModel> _testMyMMs = MyMMObjectFactory.GetTestMyMMs();

    public MyMMCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _myMMCommandHandler = new MyMMCommandHandler(_manufacturerManagerContext);
        _myMMQueryHandler = new MyMMQueryHandler(_manufacturerManagerContext);
    }
    
    [Fact]
    public async Task CreateMyMMCreatesMyMM()
    {
        var initialCount = _manufacturerManagerContext.MyMMs.Count();

        await _myMMCommandHandler.CreateMyMMAsync(_testMyMMs[0], false);
        await _myMMCommandHandler.CreateMyMMAsync(_testMyMMs[1], true);

        _manufacturerManagerContext.MyMMs.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task DeleteMyMMDeletesMyMM()
    {
        int myMMId;

        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[0]);
        _manufacturerManagerContext.SaveChanges();
        myMMId = _testMyMMs[0].MyMMId;

        await _myMMCommandHandler.DeleteMyMMAsync(myMMId, true);
        Func<Task> act = async () => await _myMMQueryHandler.GetMyMMAsync(myMMId);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateMyMMUpdatesMyMM()
    {
        var newTitle = "UpdatedTitle";

        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[0]);
        _manufacturerManagerContext.SaveChanges();

        var myMMToUpdate = _manufacturerManagerContext.MyMMs.First(m => m.MyMMId == _testMyMMs[0].MyMMId);
        myMMToUpdate.Title = newTitle;
        await _myMMCommandHandler.UpdateMyMMAsync(myMMToUpdate, true);

        var updatedMyMM = _manufacturerManagerContext.MyMMs.First(m => m.MyMMId == _testMyMMs[0].MyMMId);
        updatedMyMM.Title.Should().Be(newTitle);
    }
}
