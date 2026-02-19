namespace TestsUnit.Helpers;

public static class LanguageObjectFactory
{
    public static List<LanguageModel> GetTestLanguages()
    {
        return
        [
            new LanguageModel
            {
                EnglishName = "Test1English",
                OriginalName = "Test1Original",
                TransliteratedName = "Test1Transliterated",
                Code = "te1",
                UseInHelpPage = false,
            },
            new LanguageModel
            {
                EnglishName = "Test2English",
                OriginalName = "Test2Original",
                TransliteratedName = "Test2Transliterated",
                Code = "te2",
                UseInHelpPage = true,
            },
            new LanguageModel
            {
                EnglishName = "Test3English",
                OriginalName = "Test3Original",
                TransliteratedName = "Test3Transliterated",
                Code = "te3",
                UseInHelpPage = false,
            },
            new LanguageModel
            {
                EnglishName = "Test4English",
                OriginalName = "Test4Original",
                TransliteratedName = "Test4Transliterated",
                Code = "te4",
                UseInHelpPage = true,
            },
        ];
    }
}
