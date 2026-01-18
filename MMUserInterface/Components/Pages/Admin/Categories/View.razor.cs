namespace MMUserInterface.Components.Pages.Admin.Categories;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        CategoryModel = await CategoryQueryHandler.GetCategoryAsync(CategoryId);
        MainLayout.SetHeaderValue("View Category");
        OkToDeleteOrEdit = CategoryHelper.OkToDeleteOrEdit(CategoryModel.Name);
    }
}