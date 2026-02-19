namespace MMUserInterface.Shared.Components;

public partial class LanguageGridViewComponent
{
    [Parameter] public List<LanguageModel> Languages { get; set; } = null!;

    [Parameter] public EventCallback<(int languageId, bool IsChecked)> UseInHelpPageChanged { get; set; }

    private Task SetUnsetUseInHelpPage (int languageId, bool isChecked)
        => UseInHelpPageChanged.InvokeAsync((languageId, isChecked));
}