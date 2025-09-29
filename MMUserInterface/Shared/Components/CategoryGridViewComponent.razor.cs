namespace MMUserInterface.Shared.Components;

public partial class CategoryGridViewComponent
{
    [Parameter] public List<CategoryModel> Categories { get; set; } = null!;
}