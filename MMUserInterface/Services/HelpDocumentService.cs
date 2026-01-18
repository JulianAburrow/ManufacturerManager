namespace MMUserInterface.Services;

public class HelpDocumentService(IWebHostEnvironment env, IErrorCommandHandler errorCommandHandler, ICategoryQueryHandler categoryQueryHandler) : IHelpDocumentService
{
    public async Task<List<HelpDocumentModel>> GetHelpDocumentModelsAsync()
    {
        var helpDocumentModels = new List<HelpDocumentModel>();

        var categories = await categoryQueryHandler.GetCategoriesAsync();

        foreach (var category in categories)
        {
            var folderPath = Path.Combine("Documents", category.Name);
            foreach (var name in GetHelpDocumentNamesFromFolder(folderPath))
            {
                helpDocumentModels.Add(new HelpDocumentModel
                {
                    DocumentCategory = category.Name,
                    DocumentName = GetFileNameWithoutExtension(name)
                });
            }
        }

        return helpDocumentModels.OrderBy(m => m.DocumentCategory).ThenBy(m => m.DocumentName).ToList();
    }

    public async Task AddDocument(string category, IBrowserFile file)
    {
        try
        {
            var filePath = Path.Combine(env.ContentRootPath, "Documents", category, file.Name);

            using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10 MB limit
            using var fileStream = new FileStream(filePath, FileMode.Create);

            await stream.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            await errorCommandHandler.CreateErrorAsync(ex, true);
        }
    }

    public async Task DeleteDocument(string category, string documentName)
    {
        try
        {
            var filePath = Path.Combine(env.ContentRootPath, "Documents", category, $"{documentName}.pdf");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception ex)
        {
            await errorCommandHandler.CreateErrorAsync(ex, true);
        }
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
