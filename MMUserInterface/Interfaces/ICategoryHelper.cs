namespace MMUserInterface.Interfaces;

public interface ICategoryHelper
{
    public void CreateCategoryDirectory(string categoryName);

    public void DeleteCategoryDirectory(string categoryName);

    bool OkToDeleteOrEdit(string categoryName);
}
