namespace MMUserInterface.Shared.Components;

public partial class MyMMGridViewComponent
{
    [Parameter] public ICollection<MyMMModel> MyMMs { get; set; } = null!;
}
