namespace MMUserInterface.Helpers;

public class CategoryHelper : ICategoryHelper
{

    public void CreateCategoryDirectory(string categoryName)
    {
        var directory = Path.Combine("Documents", categoryName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void DeleteCategoryDirectory(string categoryName)
    {
        var categoryPath = Path.Combine("Documents", categoryName);
        if (Directory.Exists(categoryPath))
        {
            Directory.Delete(categoryPath, true);
        }
    }
    public bool OkToDeleteOrEdit(string categoryName)
    {
        var categoryPath = Path.Combine("Documents", categoryName);
        if (!Directory.Exists(categoryPath))
            return true;

        return Directory.GetFiles(categoryPath).Length == 0;
    }
}
