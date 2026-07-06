namespace TestsUnit.TestsQuery;

public class ErrorQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IErrorQueryHandler _errorQueryHandler;
    private readonly List<ErrorModel> _testErrors = ErrorObjectFactory.GetTestErrors();

    public ErrorQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _errorQueryHandler = new ErrorQueryHandler(_factory);
    }

    [Fact]
    public async Task GetError_GetsError()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        _manufacturerManagerContext.Errors.Add(_testErrors[0]);
        _manufacturerManagerContext.SaveChanges();

        var returnedError = await _errorQueryHandler.GetErrorAsync(_testErrors[0].ErrorId);
        returnedError.Should().NotBeNull();
        returnedError.ErrorMessage.Should().Be(_testErrors[0].ErrorMessage);
    }

    [Fact]
    public async Task GetErrors_GetsErrors()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.Errors.Count();

        _manufacturerManagerContext.Errors.Add(_testErrors[0]);
        _manufacturerManagerContext.Errors.Add(_testErrors[1]);
        _manufacturerManagerContext.SaveChanges();

        var errors = await _errorQueryHandler.GetErrorsAsync();

        errors.Count.Should().Be(initialCount + 2);
    }
}
