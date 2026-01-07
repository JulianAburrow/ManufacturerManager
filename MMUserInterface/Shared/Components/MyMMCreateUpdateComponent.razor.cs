namespace MMUserInterface.Shared.Components;

public partial class MyMMCreateUpdateComponent
{
    [Parameter] public MyMMDisplayModel MyMMDisplayModel { get; set; } = new();

    [Parameter] public List<MyMMStatusModel> MyMMStatuses { get; set; } = [];
}
