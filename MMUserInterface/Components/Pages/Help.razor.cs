namespace MMUserInterface.Components.Pages;

public partial class Help
{
    private List<string> HelpCategories = null!;

    readonly List<OllamaModel> AvailableModels = [];

    protected ChatSearchModel ChatSearchModel = new();

    protected string Response = string.Empty;

    private bool IsThinking = false;

    protected List<string> MatchingFiles = [];

    private bool IsError = false;

    private string ErrorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            AvailableModels.AddRange((
                await ChatService.GetAvailableModelsAsync())
                    .OrderBy(a => a.Size)
                    .ThenBy(a => a.Name));
        }
        catch
        {
            IsError = true;
            HelpCategories = [];
            ErrorMessage = "Unable to retrieve available models. Please ensure that Ollama is installed and running and that at least one LLM is present.";
            return;
        }
        HelpCategories = (await CategoryQueryHandler.GetCategoriesAsync()).Select(c => c.Name).ToList();
        HelpCategories.Insert(0, SharedValues.PleaseSelectText);       
        ChatSearchModel.SearchModel = AvailableModels.FirstOrDefault()?.Name ?? string.Empty;
        ChatSearchModel.SearchCategory = HelpCategories[0];
    }

    protected override void OnInitialized()
    {        
        MainLayout.SetHeaderValue("Help");
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem("Help")
        ]);
    }

    protected async Task OnSearchClicked()
    {
        Response = string.Empty;
        IsThinking = true;

        if (ChatSearchModel.SearchCategory == SharedValues.PleaseSelectText || string.IsNullOrWhiteSpace(ChatSearchModel.SearchQuestion))
        {
            Response = "❌ Please select a category and enter a question.";
            IsThinking = false;
            return;
        }

        try
        {
            MatchingFiles = ChatService.GetMatchingFiles(ChatSearchModel.SearchCategory).ToList();

            if (MatchingFiles.Count == 0)
            {
                Response = $"📂 No documents found for category '{ChatSearchModel.SearchCategory}'.";
                return;
            }

            Response = await ChatService.AskQuestionAsync(ChatSearchModel.SearchCategory, ChatSearchModel.SearchQuestion, ChatSearchModel.SearchModel, strictMode: true);
            if (string.IsNullOrWhiteSpace(Response))
                Response = "❌ Could not get an answer from this model. Please try a different model.";
        }
        catch (Exception ex)
        {
            Response = $"❌ An error occurred: it may be the case that you do not have Ollama and / or {ChatSearchModel.SearchModel}.";
            await ErrorCommandHandler.CreateErrorAsync(new Exception($"An error occurred in Help.razor.cs OnSearchClicked method. Suspect lack of Ollama/{ChatSearchModel.SearchModel}. {ex.Message}"), true);
        }
        finally
        {
            IsThinking = false;
            StateHasChanged();
        }
    }

    private string GetTooltip(long sizeBytes)
    {
        double sizeGB = sizeBytes / (1024.0 * 1024.0 * 1024.0);

        if (sizeGB < 2)
            return "Fast";
        if (sizeGB < 5)
            return "Balanced";
        if (sizeGB < 10)
            return "Detailed";

        return "Very detailed";
    }
}
