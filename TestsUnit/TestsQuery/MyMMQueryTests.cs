namespace TestsUnit.TestsQuery;

public class MyMMQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly IMyMMQueryHandler _myMMHandler;
    private readonly List<MyMMModel> _testMyMMs = MyMMObjectFactory.GetTestMyMMs();

    public MyMMQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _myMMHandler = new MyMMQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetMyMMGetsMyMM()
    {
        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[1]);
        _manufacturerManagerContext.SaveChanges();

        var returnedMyMM = await _myMMHandler.GetMyMMAsync(_testMyMMs[1].MyMMId);
        returnedMyMM.Should().NotBeNull();
        Assert.Equal(_testMyMMs[1].Title, returnedMyMM.Title);
        Assert.Equal(_testMyMMs[1].URL, returnedMyMM.URL);
    }

    [Fact]
    public async Task GetMyMMsGetsMyMMs()
    {
        var initialCount = _manufacturerManagerContext.MyMMs.Count();

        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[1]);
        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[2]);
        _manufacturerManagerContext.SaveChanges();

        var myMMsReturned = await _myMMHandler.GetMyMMsAsync();

        myMMsReturned.Count.Should().Be(initialCount + 2);
    }
}
