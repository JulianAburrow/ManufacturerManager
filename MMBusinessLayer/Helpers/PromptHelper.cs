namespace MMBusinessLayer.Helpers;

public static class PromptHelper
{
    public static string Load(PromptEnum prompt)
    {
        var fileName = string.Empty;

        switch (prompt)
        {
            case PromptEnum.RagAiPrompt:
                fileName = "RagAiPrompt.txt";
                break;
            case PromptEnum.SqlPrompt:
                fileName = "SqlPrompt.txt";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(prompt), prompt, "Unknown prompt type");
        }
        var path = Path.Combine(AppContext.BaseDirectory, "Prompts", fileName);
        return File.ReadAllText(path);
    }
}
