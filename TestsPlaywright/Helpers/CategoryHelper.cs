namespace TestsPlaywright.Helpers;

public static class CategoryHelper
{
    public static int AddCategory(ManufacturerManagerContext context)
    {
        var newCategory = new CategoryModel
        {
            Name = "Test Category 123546",
        };
        context.Categories.Add(newCategory);
        context.SaveChanges();
        return newCategory.CategoryId;
    }
    public static void RemoveCategory(int categoryId, ManufacturerManagerContext context)
    {
        var category = context.Categories.Find(categoryId);
        if (category != null)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}
