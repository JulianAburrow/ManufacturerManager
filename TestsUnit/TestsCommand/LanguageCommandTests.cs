namespace TestsUnit.TestsCommand;

public class LanguageCommandTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly ILanguageCommandHandler _languageCommandHandler;
    private readonly List<LanguageModel> _testLanguages = LanguageObjectFactory.GetTestLanguages();

    public LanguageCommandTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _languageCommandHandler = new LanguageCommandHandler(_factory);
    }

    [Fact]
    public async Task SetUnsetUseForHelpPage_SetsUnsetsUseforHelpPage()
    {
        // Arrange: seed using a fresh context
        await using (var seedContext = await _factory.CreateDbContextAsync())
        {
            _testLanguages[0].UseInHelpPage = false;
            seedContext.Languages.Add(_testLanguages[0]);
            await seedContext.SaveChangesAsync();
        }

        // Act: call handler (it uses its own fresh context)
        await _languageCommandHandler.SetUnsetUseForHelpPage(_testLanguages[0].LanguageId, true);

        // Assert: read using a new fresh context
        await using (var assertContext = await _factory.CreateDbContextAsync())
        {
            var updated = assertContext.Languages
                .Single(l => l.LanguageId == _testLanguages[0].LanguageId);

            updated.UseInHelpPage.Should().BeTrue();
        }

        // Act again
        await _languageCommandHandler.SetUnsetUseForHelpPage(_testLanguages[0].LanguageId, false);

        // Assert again using another fresh context
        await using var assertContext2 = await _factory.CreateDbContextAsync();
        var updatedAgain = assertContext2.Languages
            .Single(l => l.LanguageId == _testLanguages[0].LanguageId);

        updatedAgain.UseInHelpPage.Should().BeFalse();
    }
}
