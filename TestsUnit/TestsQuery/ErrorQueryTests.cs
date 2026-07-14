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
    public async Task GetErrorAsync_ReturnsError_WhenFound()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.Errors.Add(_testErrors[0]);
            await context.SaveChangesAsync();
        }

        var id = _testErrors[0].ErrorId;

        // Act
        var returnedError = await _errorQueryHandler.GetErrorAsync(id);

        // Assert
        returnedError.Should().NotBeNull();
        returnedError.ErrorId.Should().Be(id);
        returnedError.ErrorMessage.Should().Be(_testErrors[0].ErrorMessage);
    }

    [Fact]
    public async Task GetErrorAsync_ReturnsEmptyModel_WhenNotFound()
    {
        // Act
        var returnedError = await _errorQueryHandler.GetErrorAsync(-1);

        // Assert
        returnedError.Should().NotBeNull();
        returnedError.Should().BeOfType<ErrorModel>();
        returnedError.ErrorId.Should().Be(0);
        returnedError.ErrorMessage.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task GetErrorsAsync_ReturnsAllErrors()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.Errors.AddRange(_testErrors);
            await context.SaveChangesAsync();
        }

        // Act
        var errors = await _errorQueryHandler.GetErrorsAsync();

        // Assert
        errors.Should().NotBeNull();
        errors.Should().HaveCount(_testErrors.Count);
    }
}
