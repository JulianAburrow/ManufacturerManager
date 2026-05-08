namespace MMUserInterface.Components.Pages.Admin.LLMs;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        OllamaModel = await ChatService.GetModelAsync(LLMName);
        MainLayout.SetHeaderValue("Delete LLM");
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
            async () => await ChatService.DeleteLLMAsync(LLMName),
            $"LLM {LLMName} successfully deleted.",
            $"An error occurred deleting LLM {LLMName}. Please try again."
        );

        NavigationManager.NavigateTo("/llms/index");
    }
}