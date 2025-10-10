namespace MMUserInterface.Components.Pages.Admin.HelpDocuments;

public partial class Delete
{
    [Parameter] public string DocumentName { get; set; } = string.Empty;

    [Parameter] public string DocumentCategory { get; set; } = string.Empty ;

    private string ErrorMessage = string.Empty;

    protected override void OnInitialized()
    {

        MainLayout.SetHeaderValue("Delete Document");
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetHelpDocumentHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);        
    }

    private async Task DeleteDocument()
    {
        try
        {
            await HelpDocumentService.DeleteDocument(DocumentCategory, DocumentName);
            Snackbar.Add($"Document {DocumentName} successfully deleted.", Severity.Success);
            NavigationManager.NavigateTo("/helpdocuments/index");
        }
        catch
        {
            Snackbar.Add($"An error occurred deleting {DocumentName}. Please try again.", Severity.Error);
        }
    }
}