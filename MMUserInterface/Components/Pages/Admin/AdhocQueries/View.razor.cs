namespace MMUserInterface.Components.Pages.Admin.AdhocQueries;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        AdhocQueryModel = await AdhocQueryQueryHandler.GetAdhocQueryAsync(AdhocQueryId);
        MainLayout.SetHeaderValue("View Ad hoc Query");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetAdhocQueryHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }

    private async Task CopySql()
    {
        await JS.InvokeVoidAsync("navigator.clipboard.writeText", AdhocQueryModel.MessageOrSqlReturned);
        Snackbar.Add("SQL copied to clipboard", Severity.Success);
    }
}