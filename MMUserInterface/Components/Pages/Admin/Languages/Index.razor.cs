namespace MMUserInterface.Components.Pages.Admin.Languages;

public partial class Index
{
    private List<LanguageModel>? Languages { get; set; }

    private string Language { get; set; } = "Language";

    private string LanguagePlural { get; set; } = "Languages";

    private BreadcrumbItem GetOfficialLanguageHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new(LanguagePlural, "/languages/index", isDisabled);
    }

    private async Task UseInHelpPageChanged((int languageId, bool isChecked) args)
    {
        var (languageId, isChecked) = args;

        var language = Languages?.FirstOrDefault(l => l.LanguageId == languageId);
        language?.UseInHelpPage = isChecked;

        await LanguageCommandHandler.SetUnsetUseForHelpPage(languageId, isChecked, true);

        StateHasChanged();

        Snackbar.Add($"Language {language?.OriginalName} has been {(isChecked ? "added to" : "removed from")} the list of languages available to the Help page.", Severity.Info);
    }


    protected override async Task OnInitializedAsync()
    {
        Languages = await LanguageQueryHandler.GetLanguagesAsync();
        var count = Languages.Count;
        Snackbar.Add($"{count} language {(count == 1 ? "" : "s")} found", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Languages");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetOfficialLanguageHomeBreadcrumbItem(true),
        ]);
    }

    private async Task UnsetSelection(int languageId)
    {
        var language = Languages?.FirstOrDefault(l => l.LanguageId == languageId);
        if (language is null)
            return;

        language.UseInHelpPage = false;

        await LanguageCommandHandler.SetUnsetUseForHelpPage(languageId, false, true );

        Snackbar.Add($"Language {language.EnglishName} has been removed from the list of languages available to the Help page.", Severity.Info);
    }
}