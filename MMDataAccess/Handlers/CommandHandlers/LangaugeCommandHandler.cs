namespace MMDataAccess.Handlers.CommandHandlers;

public class LanguageCommandHandler(ManufacturerManagerContext context) : ILanguageCommandHandler
{
    
    public async Task SetUnsetUseForHelpPage(int languageId, bool useInHelpPage, bool callSaveChanges)
    {
        var language = await context.Languages.FindAsync(languageId);
        if (language is null)
            return;

        language.UseInHelpPage = useInHelpPage;

        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
