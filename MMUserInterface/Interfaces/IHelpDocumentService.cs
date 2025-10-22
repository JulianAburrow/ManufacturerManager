namespace MMUserInterface.Interfaces;

public interface IHelpDocumentService
{
    Task<List<HelpDocumentModel>> GetHelpDocumentModelsAsync();

    Task AddDocument(string category, IBrowserFile file);

    Task DeleteDocument(string category, string documentName);
}
