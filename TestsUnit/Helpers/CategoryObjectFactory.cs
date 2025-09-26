namespace TestsUnit.Helpers;

public static class CategoryObjectFactory
{
    public static List<CategoryModel> GetTestCategories()
    {
        return
        [
            new CategoryModel
            {
                Name = "Colour",
            },
            new CategoryModel
            {
                Name = "ColourJustification",
            },
            new CategoryModel
            {
                Name = "Manufacturer",
            },
            new CategoryModel
            {
                Name = "Widget",
            }
        ];
    }
}
