namespace MMUserInterface.Components.Pages;

public partial class Help
{
    private List<string> HelpCategories = [];

    readonly List<OllamaModel> AvailableModels = [];

    protected ChatSearchModel ChatSearchModel = new();

    protected string Response= string.Empty;

    private bool IsThinking = false;

    protected List<string> MatchingFiles = [];

    protected override async Task OnInitializedAsync()
    {
        HelpCategories = (await CategoryQueryHandler.GetCategoriesAsync()).Select(c => c.Name).ToList();
        HelpCategories.Insert(0, SharedValues.PleaseSelectText);
        AvailableModels.AddRange(await ChatService.GetAvailableModelsAsync());
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
        catch
        {
            Response = $"❌ An error occurred: it may be the case that you do not have Ollama and / or {ChatSearchModel.SearchModel}.";
            await ErrorCommandHandler.CreateErrorAsync(new Exception($"An error occurred in Help.razor.cs OnSearchClicked method. Suspect lack of Ollama/{ChatSearchModel.SearchModel}."), true);
        }
        finally
        {
            IsThinking = false;
            StateHasChanged();
        }
    }
}
