namespace MMDataAccess.Handlers.CommandHandlers;

public class LanguageCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : ILanguageCommandHandler
{

    public async Task SetUnsetUseForHelpPage(int languageId, bool useInHelpPage)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var language = await context.Languages.FindAsync(languageId);
        if (language is null)
            return;

        language.UseInHelpPage = useInHelpPage;
        await context.SaveChangesAsync();
    }
}
