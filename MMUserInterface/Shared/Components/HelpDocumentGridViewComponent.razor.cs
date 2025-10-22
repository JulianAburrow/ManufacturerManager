namespace MMUserInterface.Shared.Components;

public partial class HelpDocumentGridViewComponent
{
    [Parameter] public List<HelpDocumentModel> HelpDocumentModels { get; set; } = null!;
}