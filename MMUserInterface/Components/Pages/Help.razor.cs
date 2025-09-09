namespace MMUserInterface.Components.Pages;

public partial class Help
{
    readonly List<string> HelpCategories = [ SharedValues.PleaseSelectText, "Manufacturer", "Widget", "Colour", "ColourJustification" ];

    protected ChatSearchModel ChatSearchModel = new();

    protected string _response = string.Empty;

    private bool _isThinking = false;

    protected List<string> MatchingFiles = [];

    protected override void OnInitialized()
    {
        ChatSearchModel.SearchCategory = HelpCategories[0];
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem("Help")
        ]);
    }

    protected async Task OnSearchClicked()
    {
        _response = string.Empty;
        _isThinking = true;

        if (ChatSearchModel.SearchCategory == SharedValues.PleaseSelectText || string.IsNullOrWhiteSpace(ChatSearchModel.SearchQuestion))
        {
            _response = "❌ Please select a category and enter a question.";
            _isThinking = false;
            return;
        }

        try
        {
            MatchingFiles = ChatService.GetMatchingFiles(ChatSearchModel.SearchCategory).ToList();

            if (MatchingFiles.Count == 0)
            {
                _response = $"📂 No documents found for category '{ChatSearchModel.SearchCategory}'.";
                return;
            }

            _response = await ChatService.AskQuestionAsync(ChatSearchModel.SearchCategory, ChatSearchModel.SearchQuestion, strictMode: true);
        }
        catch
        {
            _response = "❌ An error occurred: it may be the case that you do not have Ollama and TinyLlama.";
            await ErrorCommandHandler.CreateErrorAsync(new Exception("An error occurred in Help.razor.cs OnSearchClicked method. Suspect lack of Ollama/TinyLlama."), true);
        }
        finally
        {
            _isThinking = false;
            StateHasChanged();
        }
    }
}
