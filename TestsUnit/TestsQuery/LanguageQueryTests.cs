namespace TestsUnit.TestsQuery;

public class LanguageQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly ILanguageQueryHandler _languageQueryHandler;
    private readonly List<LanguageModel> _testLanguages = LanguageObjectFactory.GetTestLanguages();

    public LanguageQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _languageQueryHandler = new LanguageQueryHandler(_factory);
    }

    [Fact]
    public async Task GetLanguages_GetsLanguages()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync(TestContext.Current.CancellationToken);
        var initialCount = _manufacturerManagerContext.Languages.Count();

        _manufacturerManagerContext.Languages.Add(_testLanguages[0]);
        _manufacturerManagerContext.Languages.Add(_testLanguages[1]);
        _manufacturerManagerContext.Languages.Add(_testLanguages[2]);
        _manufacturerManagerContext.Languages.Add(_testLanguages[3]);
        await _manufacturerManagerContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var languages = await _languageQueryHandler.GetLanguagesAsync();

        languages.Count.Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task GetLanguagesForHelpPage_GetsLanguagesForHelpPage()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync(TestContext.Current.CancellationToken);
        var initialCount = _manufacturerManagerContext.Languages.Count();

        _manufacturerManagerContext.Languages.Add(_testLanguages[0]);
        _manufacturerManagerContext.Languages.Add(_testLanguages[1]);
        _manufacturerManagerContext.Languages.Add(_testLanguages[2]);
        _manufacturerManagerContext.Languages.Add(_testLanguages[3]);
        await _manufacturerManagerContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var expectedCount =
            initialCount +
            _manufacturerManagerContext.Languages.Count(l => l.UseInHelpPage);

        var languages = await _languageQueryHandler.GetLanguagesForHelpPageAsync();

        languages
            .Count
            .Should()
            .Be(
                initialCount + expectedCount);
    }
}
