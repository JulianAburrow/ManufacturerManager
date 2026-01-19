namespace MMUserInterface.Components.Pages.Admin.Categories;

public partial class Index
{
    private List<CategoryModel>? Categories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Categories = await CategoryQueryHandler.GetCategoriesAsync();
        var count = Categories.Count;
        Snackbar.Add($"{count} {(count == 1 ? "category" : "categories")} found", count > 0 ? Severity.Info : Severity.Warning);
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