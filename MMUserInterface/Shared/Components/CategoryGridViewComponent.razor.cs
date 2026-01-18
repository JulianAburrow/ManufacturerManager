namespace MMUserInterface.Shared.Components;

public partial class CategoryGridViewComponent
{
    [Parameter] public List<CategoryModel> Categories { get; set; } = null!;

    private bool OkToDeleteOrEdit(string categoryName)
    {
        var categoryPath = Path.Combine("Documents", categoryName);

        if (!Directory.Exists(categoryPath))
            return true;

        var fileCount = Directory.GetFiles(categoryPath).Length;

        return fileCount == 0;
    }
}