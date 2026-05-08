namespace MMUserInterface.Shared.Components;

public partial class LanguageGridViewComponent
{
    [Parameter] public List<LanguageModel> Languages { get; set; } = null!;

    [Parameter] public EventCallback<(int languageId, bool IsChecked)> UseInHelpPageChanged { get; set; }

    private string SearchString { get; set; } = string.Empty;

    private List<LanguageModel> LanguagesToShow = null!;

    protected override void OnInitialized()
    {
        LanguagesToShow = Languages.ToList();
    }

    private void Search()
    {
        if (string.IsNullOrWhiteSpace(SearchString))
        {
            return;
        }

        SearchString = SearchString.Trim();

        LanguagesToShow = Languages
            .Where(l => l.EnglishName.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private void ClearSearch()
    {
        LanguagesToShow = Languages.ToList();
        SearchString = string.Empty;
    }

    private Task SetUnsetUseInHelpPage (int languageId, bool isChecked)
        => UseInHelpPageChanged.InvokeAsync((languageId, isChecked));
}