namespace MMUserInterface.Shared.Components;

public partial class ErrorGridViewComponent
{
    [Parameter] public List<ErrorModel> Errors { get; set; } = null!;
}
