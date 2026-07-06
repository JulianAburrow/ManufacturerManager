namespace MMDataAccess.Handlers.QueryHandlers;

public class LanguageQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : ILanguageQueryHandler
{
    public async Task<List<LanguageModel>> GetLanguagesAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Languages
            .OrderBy(l => l.EnglishName)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<LanguageModel>> GetLanguagesForHelpPageAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Languages
            .Where(l => l.UseInHelpPage)
            .OrderBy(l => l.EnglishName)
            .AsNoTracking()
            .ToListAsync();
    }
}
