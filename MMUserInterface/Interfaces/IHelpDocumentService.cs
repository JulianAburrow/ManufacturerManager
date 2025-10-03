namespace MMUserInterface.Interfaces;

public interface IHelpDocumentService
{
    Task<List<HelpDocumentModel>> GetHelpDocumentModelsAsync();

    Task AddDocument();

    Task DeleteDocument(string documentName);
}
