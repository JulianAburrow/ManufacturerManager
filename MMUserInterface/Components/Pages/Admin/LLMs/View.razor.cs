namespace MMUserInterface.Components.Pages.Admin.LLMs;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        OllamaModel = await ModelManagementService.GetModelAsync(LLMName);
        MainLayout.SetHeaderValue("View LLM");

        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetLLMHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}
