namespace MMUserInterface.Shared.Components;

public partial class LoadingMessageComponent
{
    [Parameter]
    public string ValueToShow { get; set; } = default!;

    [Parameter]
    public bool IsLoading { get; set; } = true;
}