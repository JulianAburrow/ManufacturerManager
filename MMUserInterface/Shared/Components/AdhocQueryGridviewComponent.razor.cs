namespace MMUserInterface.Shared.Components;

public partial class AdhocQueryGridviewComponent
{
    [Parameter] public List<AdhocQueryModel> AdhocQueries { get; set; } = null!;
}