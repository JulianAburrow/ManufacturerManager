namespace MMUserInterface.Components.Pages.Admin.LLMs;

public partial class Index
{
    private bool IsError = false;

    private string ErrorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            LLMs =
            [
                .. (
                    await ChatService.GetAvailableModelsAsync())
                    .OrderBy(a => a.Size)
                    .ThenBy(a => a.Name),
            ];
            var count = LLMs.Count;
            Snackbar.Add($"{count} LLM{(count == 1 ? "" : "s")} found", count > 0 ? Severity.Info : Severity.Warning);
            MainLayout.SetHeaderValue("LLMs");
        }
        catch
        {
            IsError = true;
            ErrorMessage = "Unable to retrieve LLMs. Please ensure that Ollama is installed and running and that at least one LLM is present.";
            return;
        }
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetLLMHomeBreadcrumbItem(true),
        ]);
    }
}