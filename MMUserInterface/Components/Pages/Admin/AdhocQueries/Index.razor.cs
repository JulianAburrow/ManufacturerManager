namespace MMUserInterface.Components.Pages.Admin.AdhocQueries;

public partial class Index
{
    private List<AdhocQueryModel>? AdhocQueries { get; set; }

    protected override async Task OnInitializedAsync()
    {
        AdhocQueries = await AdhocQueryQueryHandler.GetAdhocQueriesAsync();
        var count = AdhocQueries.Count;
        Snackbar.Add($"{count} {(count == 1 ? "adhoc query" : "adhoc queries")} found.", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Ad hoc Queries");
    }
}