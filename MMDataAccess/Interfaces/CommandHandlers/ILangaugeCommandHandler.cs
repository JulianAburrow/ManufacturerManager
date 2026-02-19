namespace MMDataAccess.Interfaces.CommandHandlers;

public interface ILanguageCommandHandler
{
    Task SetUnsetUseForHelpPage(int languageId, bool useInHelpPage, bool callSaveChanges);

    Task SaveChangesAsync();
}
