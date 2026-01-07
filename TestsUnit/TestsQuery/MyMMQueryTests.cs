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
        Assert.Equal(_testMyMMs[1].Notes, returnedMyMM.Notes);
        Assert.Equal(_testMyMMs[1].ActionDate, returnedMyMM.ActionDate);
        Assert.Equal(_testMyMMs[1].IsExternal, returnedMyMM.IsExternal);
        Assert.Equal(_testMyMMs[1].StatusId, returnedMyMM.StatusId);
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

    [Fact]
    public async Task GetMyMMsForHomePageGetsOnlyPastAndTodayMyMMs()
    {
        _testMyMMs[0].ActionDate = DateTime.Today.AddDays(-1);
        _testMyMMs[0].StatusId = (int)PublicEnums.MyMMStatusEnum.Active;
        _testMyMMs[0].Status = new MyMMStatusModel { StatusId = 1, StatusName = "Active" };

        _testMyMMs[1].ActionDate = DateTime.Today;
        _testMyMMs[1].StatusId = (int)PublicEnums.MyMMStatusEnum.Pending;
        _testMyMMs[1].Status = new MyMMStatusModel { StatusId = 3, StatusName = "Pending" };

        _testMyMMs[2].ActionDate = DateTime.Today.AddDays(1);
        _testMyMMs[2].StatusId = (int)PublicEnums.MyMMStatusEnum.Inactive;
        _testMyMMs[2].Status = new MyMMStatusModel { StatusId = 2, StatusName = "Inactive" };

        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[0]);
        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[1]);
        _manufacturerManagerContext.MyMMs.Add(_testMyMMs[2]);

        _manufacturerManagerContext.SaveChanges();

        var myMMsReturned = await _myMMHandler.GetMyMMsForHomePageAsync();

        myMMsReturned.Count.Should().Be(2); // only the two valid ones

        // Ordered by ActionDate: yesterday first, then today
        myMMsReturned[0].ActionDate.Should().Be(DateTime.Today.AddDays(-1));
        myMMsReturned[1].ActionDate.Should().Be(DateTime.Today);

        myMMsReturned.Should().OnlyContain(m =>
            m.ActionDate <= DateTime.Now &&
            (m.StatusId == (int)PublicEnums.MyMMStatusEnum.Active ||
             m.StatusId == (int)PublicEnums.MyMMStatusEnum.Pending));
    }

}
