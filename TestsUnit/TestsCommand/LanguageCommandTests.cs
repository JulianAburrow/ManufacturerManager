namespace TestsUnit.TestsCommand;

public class LanguageCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly ILanguageCommandHandler _languageCommandHandler;
    private readonly List<LanguageModel> _testLanguages = LanguageObjectFactory.GetTestLanguages();

    public LanguageCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _languageCommandHandler = new LanguageCommandHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task SetUnsetUseForHelpPageSetsUnsetsUseforHelpPage()
    {
        var initialCount = _manufacturerManagerContext.Languages.Count();
        _testLanguages[0].UseInHelpPage = false;
        _manufacturerManagerContext.Languages.Add(_testLanguages[0]);
        _manufacturerManagerContext.SaveChanges();

        await _languageCommandHandler.SetUnsetUseForHelpPage(_testLanguages[0].LanguageId, true, true);

        var updated = _manufacturerManagerContext.Languages
            .Single(l => l.LanguageId == _testLanguages[0].LanguageId);

        updated.UseInHelpPage.Should().BeTrue();

        await _languageCommandHandler.SetUnsetUseForHelpPage(_testLanguages[0].LanguageId, false, true);

        var updatedAgain = _manufacturerManagerContext.Languages
            .Single(l => l.LanguageId == _testLanguages[0].LanguageId);

        updatedAgain.UseInHelpPage.Should().BeFalse();
    }
}
