namespace MMUserInterface.Components.Pages.Admin.Categories;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        CategoryModel = await CategoryQueryHandler.GetCategoryAsync(CategoryId);
        CopyModelToDisplayModel();
        MainLayout.SetHeaderValue("Delete Category");

        OkToDeleteOrEdit = CategoryHelper.OkToDeleteOrEdit(CategoryModel.Name);
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCategoryHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);
    }

    private async Task DeleteCategory()
    {
        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await CategoryCommandHandler.DeleteCategoryAsync(CategoryId, true),
            $"Category {CategoryModel.Name} successfully deleted.",
            $"An error occurred deleting category {CategoryModel.Name}. Please try again."
        );
        CategoryHelper.DeleteCategoryDirectory(CategoryModel.Name);
        NavigationManager.NavigateTo("/categories/index");
    }
}