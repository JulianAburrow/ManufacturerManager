namespace MMUserInterface.Components.Pages.MyMMs;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        MyMMModel = await MyMMQueryHandler.GetMyMMAsync(MyMMId);
        MainLayout.SetHeaderValue("Delete MyMM");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetMyMMHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);
    }

    private async Task DeleteMyMM()
    {
        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await MyMMCommandHandler.DeleteMyMMAsync(MyMMId, true),
            $"MyMM {MyMMModel.Title} successfully deleted.",
            $"An error occurred deleting MyMM {MyMMModel.Title}. Please try again."
        );
        if (actionSuccessful)
            NavigationManager.NavigateTo("/mymms/index");
    }
}