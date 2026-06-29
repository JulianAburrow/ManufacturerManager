namespace MMBusinessLayer.Interfaces;

public interface IDocumentService
{
    IReadOnlyList<string> GetMatchingFiles(string Category);

    string ExtractTextFromPdf(string filePath);
}
