namespace MMUserInterface.Shared.Components;

public partial class AdhocQueriesListAndRerunComponent
{
    [Parameter] public List<AdhocQueryListModel> LastXSuccessfulAdhocQueries { get; set; } = null!;

    [Parameter] public EventCallback<string> OnAdhocQuerySelected { get; set; }
}