namespace MMUserInterface.Services;

public class HelpDocumentService : IHelpDocumentService
{
    public Task<List<HelpDocumentModel>> GetHelpDocumentModelsAsync()
    {
        var helpDocumentModels = new List<HelpDocumentModel>();
        var rootFolderName = "Documents";

        var folderPath = Path.Combine(rootFolderName, SharedValues.DocumentCategories.Colour.ToString());
        foreach (var name in GetHelpDocumentNamesFromFolder(folderPath))
        {
            helpDocumentModels.Add(new HelpDocumentModel
            {
                DocumentCategory = SharedValues.DocumentCategories.Colour.ToString(),
                DocumentName = GetFileNameWithoutExtension(name)
            });
        }
        folderPath = Path.Combine(rootFolderName, SharedValues.DocumentCategories.ColourJustification.ToString());
        foreach (var name in GetHelpDocumentNamesFromFolder(folderPath))
        {
            helpDocumentModels.Add(new HelpDocumentModel
            {
                DocumentCategory = SharedValues.DocumentCategories.ColourJustification.ToString(),
                DocumentName = GetFileNameWithoutExtension(name)
            });
        }
        folderPath = Path.Combine(rootFolderName, SharedValues.DocumentCategories.Manufacturer.ToString());
        foreach (var name in GetHelpDocumentNamesFromFolder(folderPath))
        {
            helpDocumentModels.Add(new HelpDocumentModel
            {
                DocumentCategory = SharedValues.DocumentCategories.Manufacturer.ToString(),
                DocumentName = GetFileNameWithoutExtension(name)
            });
        }
        folderPath = Path.Combine(rootFolderName, SharedValues.DocumentCategories.Widget.ToString());
        foreach (var name in GetHelpDocumentNamesFromFolder(folderPath))
        {
            helpDocumentModels.Add(new HelpDocumentModel
            {
                DocumentCategory = SharedValues.DocumentCategories.Widget.ToString(),
                DocumentName = GetFileNameWithoutExtension(name)
            });
        }
        return Task.FromResult(helpDocumentModels);
    }

    public Task AddDocument()
    {
        throw new NotImplementedException();
    }

    public Task DeleteDocument(string documentName)
    {
        throw new NotImplementedException();
    }

    private static List<string> GetHelpDocumentNamesFromFolder(string folderPath)
    {
        var helpDocumentNames = new List<string>();
        if (Directory.Exists(folderPath))
        {
            var helpDocumentNamesInFolder = Directory.GetFiles(folderPath)
                .Select(Path.GetFileName)
                .Where(name => name is not null)
                .ToList();
            foreach (var helpDocumentName in helpDocumentNamesInFolder)
            {
                helpDocumentNames.Add(helpDocumentName!);
            }
        }
        return helpDocumentNames;
    }

    private static string GetFileNameWithoutExtension(string fileName)
    {
        return fileName.Substring(0, fileName.LastIndexOf("."));
    }
}
