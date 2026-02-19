namespace MMDataAccess.Handlers.QueryHandlers;

public class LanguageQueryHandler(ManufacturerManagerContext context) : ILanguageQueryHandler
{
    public async Task<List<LanguageModel>> GetLanguagesAsync() =>
        await context.Languages
            .OrderBy(l => l.EnglishName)
            .AsNoTracking()
            .ToListAsync();

    public Task<List<LanguageModel>> GetLanguagesForHelpPageAsync() =>
        context.Languages
            .Where(l => l.UseInHelpPage)
            .OrderBy(l => l.EnglishName)
            .AsNoTracking()
            .ToListAsync();
}
