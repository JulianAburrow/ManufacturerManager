namespace MMUserInterface.Components.Pages.Admin.AdhocQueries;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        AdhocQueryModel = await AdhocQueryQueryHandler.GetAdhocQueryAsync(AdhocQueryId);
        MainLayout.SetHeaderValue("Delete Ad hoc Query");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetAdhocQueryHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);
    }

    private async Task DeleteAdhocQuery()
    {
        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await AdhocQueryCommandHandler.DeleteAdhocQueryAsync(AdhocQueryId),
            $"Ad hoc query {AdhocQueryModel.NaturalLanguageQuery} successfully deleted",
            $"An error occurred deleting ad hoc query {AdhocQueryModel.NaturalLanguageQuery}. Please try again"
        );
        if (actionSuccessful)
            NavigationManager.NavigateTo("/adhocqueries/index");
    }
}