namespace MMUserInterface.Components.Pages.Admin.Categories;

public partial class Create
{
    protected override void OnInitialized()
    {
        MainLayout.SetHeaderValue("Create Category");
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCategoryHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb),
        ]);
    }

    private async Task CreateCategory()
    {
        CopyDisplayModelToModel();

        CategoryExists = CategoryQueryHandler.CategoryExists(CategoryModel.Name);

        if (CategoryExists)
        {
            return;
        }

        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await CategoryCommandHandler.CreateCategoryAsync(CategoryModel, true),
            $"Category {CategoryModel.Name} succcessfully created",
            $"An error occurred creating colour {CategoryModel.Name}. Please try again."
        );

        var directory = Path.Combine("Documents", CategoryModel.Name);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        NavigationManager.NavigateTo("/categories/index");
    }
}
