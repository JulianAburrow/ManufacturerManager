namespace MMDataAccess.Handlers.CommandHandlers;

public class LanguageCommandHandler(ManufacturerManagerContext context) : ILanguageCommandHandler
{

    public async Task SetUnsetUseForHelpPage(int languageId, bool useInHelpPage)
    {
        var language = await context.Languages.FindAsync(languageId);
        if (language is null)
            return;

        language.UseInHelpPage = useInHelpPage;
        await context.SaveChangesAsync();
    }
}
