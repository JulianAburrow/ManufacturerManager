namespace MMUserInterface.Components.Pages.Admin.LLMs;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        OllamaModel = await ChatService.GetModelAsync(LLMName);
        MainLayout.SetHeaderValue("View LLM");
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
