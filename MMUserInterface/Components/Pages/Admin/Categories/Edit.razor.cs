namespace MMUserInterface.Components.Pages.Admin.Categories;

public partial class Edit
{
    private string OriginalCategoryName { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        CategoryModel = await CategoryQueryHandler.GetCategoryAsync(CategoryId);
        CopyModelToDisplayModel();
        MainLayout.SetHeaderValue("Edit Category");

        OkToDeleteOrEdit = CategoryHelper.OkToDeleteOrEdit(CategoryModel.Name);
        if (!OkToDeleteOrEdit)
            return;
        OriginalCategoryName = CategoryModel.Name;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCategoryHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(EditTextForBreadcrumb),
        ]);
    }

    private async Task UpdateCategory()
    {
        CopyDisplayModelToModel();

        CategoryExists = CategoryQueryHandler.CategoryExists(CategoryDisplayModel.Name);

        if (CategoryExists)
        {
            return;
        }

        CategoryHelper.DeleteCategoryDirectory(OriginalCategoryName);
        CategoryHelper.CreateCategoryDirectory(CategoryModel.Name);

        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await CategoryCommandHandler.UpdateCategoryAsync(CategoryModel, true),
            $"Category {CategoryModel.Name} successfully updated.",
            $"An error occurred updating category {CategoryModel.Name}. Please try again."
        );
        NavigationManager.NavigateTo("/categories/index");
    }
}