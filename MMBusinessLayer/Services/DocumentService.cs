namespace MMBusinessLayer.Services;

public class DocumentService : IDocumentService
{
    public IReadOnlyList<string> GetMatchingFiles(string category)
    {
        string folderPath = Path.Combine("Documents", category);
        return Directory.GetFiles(folderPath)
                        .Where(f => Path.GetFileName(f)
                        .Contains(category, StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }

    public string ExtractTextFromPdf(string filePath)
    {
        using var pdf = PdfDocument.Open(filePath);
        var text = new StringBuilder();
        foreach (var page in pdf.GetPages())
        {
            text.AppendLine(page.Text);
        }
        return text.ToString();
    }
}
