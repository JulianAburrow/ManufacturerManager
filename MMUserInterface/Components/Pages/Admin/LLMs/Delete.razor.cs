namespace MMUserInterface.Components.Pages.Admin.LLMs;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        OllamaModel = await ModelManagementService.GetModelAsync(LLMName);
        MainLayout.SetHeaderValue("Delete LLM");

        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetLLMHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);
    }

    private async Task DeleteLLM()
    {
        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ModelManagementService.DeleteModelAsync(LLMName),
            $"LLM {LLMName} successfully deleted.",
            $"An error occurred deleting LLM {LLMName}. Please try again."
        );

        NavigationManager.NavigateTo("/llms/index");
    }
}