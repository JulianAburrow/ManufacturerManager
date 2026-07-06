namespace TestsUnit.TestsCommand;

public class ErrorCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly IErrorCommandHandler _errorCommandHandler;
    private readonly List<Exception> _testExceptions = ErrorObjectFactory.GetTestExceptions();
    private readonly List<ErrorModel> _testErrors = ErrorObjectFactory.GetTestErrors();

    public ErrorCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _errorCommandHandler = new ErrorCommandHandler(_factory);
    }

    [Fact]
    public async Task CreateError_CreatesError()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.Errors.Count();

        await _errorCommandHandler.CreateErrorAsync(_testExceptions[0]);
        await _errorCommandHandler.CreateErrorAsync(_testExceptions[1]);

        _manufacturerManagerContext.Errors.Count().Should().Be(initialCount + 2);
    }

    [Fact]
    public async Task DeleteError_DeletesError()
    {
        int errorId;
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();

        _manufacturerManagerContext.Errors.Add(_testErrors[1]);
        _manufacturerManagerContext.SaveChanges();
        errorId = _testErrors[1].ErrorId;

        await _errorCommandHandler.DeleteErrorAsync(errorId);

        var deletedError = _manufacturerManagerContext.Errors.FirstOrDefault(e => e.ErrorId == errorId);

        deletedError.Should().BeNull();
    }

    [Fact]
    public async Task UpdateError_UpdatesError()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var newErrorMessage = "UpdatedError1";
        var resolvedDate = DateTime.Now;

        _manufacturerManagerContext.Errors.Add(_testErrors[0]);
        _manufacturerManagerContext.SaveChanges();

        var errorToUpdate = _manufacturerManagerContext.Errors.First(e => e.ErrorId == _testErrors[0].ErrorId);
        errorToUpdate.ErrorMessage = newErrorMessage;
        errorToUpdate.Resolved = true;
        errorToUpdate.ResolvedDate = resolvedDate;
        await _errorCommandHandler.UpdateErrorAsync(errorToUpdate);

        var updatedError = _manufacturerManagerContext.Errors.First(e => e.ErrorId == _testErrors[0].ErrorId);
        updatedError.Should().NotBeNull();
        updatedError.ErrorMessage.Should().Be(newErrorMessage);
        updatedError.Resolved.Should().BeTrue();
        updatedError.ResolvedDate.Should().Be(resolvedDate);
    }
}
