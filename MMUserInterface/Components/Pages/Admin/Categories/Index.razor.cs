namespace MMUserInterface.Components.Pages.Admin.Categories;

public partial class Index
{
    List<CategoryModel> Categories { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Categories = await CategoryQueryHandler.GetCategoriesAsync();
        Snackbar.Add($"{Categories.Count} item(s) found", Categories.Count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Categories");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCategoryHomeBreadcrumbItem(true),
        ]);
    }
}