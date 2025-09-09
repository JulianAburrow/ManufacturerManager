namespace MMUserInterface.Interfaces;

public interface IChatService
{
    Task<string> AskQuestionAsync(string category, string question, bool strictMode = true);

    IReadOnlyList<string> GetMatchingFiles(string category);

}
