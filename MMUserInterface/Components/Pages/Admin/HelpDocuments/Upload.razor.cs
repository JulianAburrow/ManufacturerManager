

namespace MMUserInterface.Components.Pages.Admin.HelpDocuments;

public partial class Upload
{
    [Inject] public IWebHostEnvironment Env { get; set; } = default!;

    private List<string> HelpCategories = [];

    private HelpDocumentDisplayModel HelpDocumentDisplayModel = new();
    private string FileName = string.Empty;
    private string FileMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        HelpCategories = (await CategoryQueryHandler.GetCategoriesAsync()).Select(c => c.Name).ToList();
        HelpCategories.Insert(0, SharedValues.PleaseSelectText);
        HelpDocumentDisplayModel.Category = SharedValues.PleaseSelectText;
        MainLayout.SetHeaderValue("Upload Help Document");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetHelpDocumentHomeBreadcrumbItem(),
            new BreadcrumbItem("Upload", null, true)
        ]);
    }

    private async Task CreateFile()
    {
        try
        {
            var filePath = Path.Combine(Env.ContentRootPath, "Documents", HelpDocumentDisplayModel.Category, FileName);

            using var stream = HelpDocumentDisplayModel.HelpFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10 MB limit
            using var fileStream = new FileStream(filePath, FileMode.Create);

            await stream.CopyToAsync(fileStream);

            Snackbar.Add($"File {FileName} successfully saved", Severity.Success);
            NavigationManager.NavigateTo("/helpdocuments/index");
        }
        catch
        {
            Snackbar.Add($"An error occurred uploading {FileName}.", Severity.Error);
        }
    }

    private void UploadFile(IBrowserFile file)
    {
        if (file == null)
        {
            return;
        }
        if (!file.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            FileMessage = "Only PDF files are allowed.";
            return;
        }
        if (!file.Name.Contains(HelpDocumentDisplayModel.Category))
        {
            FileMessage = $"File name must contain the category name. Please select a category first.";
            return;
        }   
        try
        {
            FileMessage = "File successfully uploaded.";
            HelpDocumentDisplayModel.HelpFile = file;
            FileName = file.Name;
            Snackbar.Add($"File {FileName} successfully uploaded", Severity.Success);
        }
        catch
        {
            FileMessage = $"An error occurred uploading {file.Name}. Please try again.";
            Snackbar.Add(FileMessage, Severity.Error);
        }
    }
}