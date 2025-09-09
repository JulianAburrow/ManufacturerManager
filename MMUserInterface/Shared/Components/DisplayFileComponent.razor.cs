namespace MMUserInterface.Shared.Components;

public partial class DisplayFileComponent
{
    [Parameter] public string Url { get; set; } = string.Empty;

    [Parameter] public string DisplayText { get; set; } = string.Empty;
}