namespace MMUserInterface.Shared.Components;

public partial class AdHocQueryResultsComponent
{
    [Parameter] public DataTable AdhocQueryResults { get; set; } = null!;
}