namespace MMDataAccess.Models;

public class LanguageModel
{
    public int LanguageId { get; set; }

    public string EnglishName { get; set; } = default!;

    public string OriginalName { get; set; } = default!;

    public string TransliteratedName { get; set; } = default!;

    public string Code { get; set; } = default!;

    public bool UseInHelpPage { get; set; }
}
