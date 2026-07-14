namespace MMUserInterface.Components.Pages.Admin.Categories;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        CategoryModel = await CategoryQueryHandler.GetCategoryAsync(CategoryId);

        if (CategoryModel.CategoryId == 0)
        {
            MainLayout.SetHeaderValue(CategoryNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        MainLayout.SetHeaderValue("View Category");
        OkToDeleteOrEdit = CategoryHelper.OkToDeleteOrEdit(CategoryModel.Name);

        _isLoaded = true;
    }
}