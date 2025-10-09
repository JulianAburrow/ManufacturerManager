namespace TestsPlaywright.Helpers;

public static class HelpDocumentHelper
{
    public static readonly string DocumentName = "AAAAAAAATestColourHelpDocument.pdf"; // Should ideally come first in the list

    public static string GetHelpDocumentPath()
    {
        string rootPath = Environment.GetEnvironmentVariable("MM_UI_ROOT_PATH")
                          ?? @"C:\inetpub\ManufacturerManagerDev";

        return Path.Combine(rootPath, "Documents", "Colour", DocumentName);

    }

    public static void AddHelpDocument()
    {
        var document = new PdfDocument();
        document.Info.Title = "Test Help Document";
        document.AddPage();

        using var stream = new FileStream(GetHelpDocumentPath(), FileMode.Create);
        document.Save(stream);
    }

    public static void RemoveHelpDocument()
    {
        File.Delete(GetHelpDocumentPath());
    }
}
