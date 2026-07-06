namespace TestsUnit.TestsCommand;

public class MyMMCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IMyMMCommandHandler _myMMCommandHandler;
    private readonly IMyMMQueryHandler _myMMQueryHandler;
    private readonly List<MyMMModel> _testMyMMs = MyMMObjectFactory.GetTestMyMMs();

    public MyMMCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _myMMCommandHandler = new MyMMCommandHandler(_factory);
        _myMMQueryHandler = new MyMMQueryHandler(_factory);
    }
    
    [Fact]
    public async Task CreateMyMM_CreatesMyMM()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.MyMMs.Count();

        await _myMMCommandHandler.CreateMyMMAsync(_testMyMMs[0]);
        await _myMMCommandHandler.CreateMyMMAsync(_testMyMMs[1]);

        _manufacturerManagerContext.MyMMs.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task DeleteMyMM_DeletesMyMM()
    {
        await using (var _manufacturerManagerContext = await _factory.CreateDbContextAsync())
        {
            _manufacturerManagerContext.MyMMs.Add(_testMyMMs[0]);
            await _manufacturerManagerContext.SaveChangesAsync();
        }

        var myMMId = _testMyMMs[0].MyMMId;
        await _myMMCommandHandler.DeleteMyMMAsync(myMMId);

        Func<Task> act = async () => await _myMMQueryHandler.GetMyMMAsync(myMMId);
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateMyMM_UpdatesMyMM()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var newTitle = "UpdatedTitle";

        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[0]);
        _manufacturerManagerContext.SaveChanges();

        var myMMToUpdate = _manufacturerManagerContext.MyMMs.First(m => m.MyMMId == _testMyMMs[0].MyMMId);
        myMMToUpdate.Title = newTitle;
        await _myMMCommandHandler.UpdateMyMMAsync(myMMToUpdate);

        var updatedMyMM = _manufacturerManagerContext.MyMMs.First(m => m.MyMMId == _testMyMMs[0].MyMMId);
        updatedMyMM.Title.Should().Be(newTitle);
    }
}
