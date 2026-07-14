namespace MMUserInterface.Components.Pages.MyMMs;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        MyMMModel = await MyMMQueryHandler.GetMyMMAsync(MyMMId);

        if (MyMMModel.MyMMId == 0)
        {
            MainLayout.SetHeaderValue(MyMMNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        MainLayout.SetHeaderValue("Delete MyMM");

        _isLoaded = true;
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
            async () => await MyMMCommandHandler.DeleteMyMMAsync(MyMMId),
            $"MyMM {MyMMModel.Title} successfully deleted.",
            $"An error occurred deleting MyMM {MyMMModel.Title}. Please try again."
        );
        if (actionSuccessful)
            NavigationManager.NavigateTo("/mymms/index");
    }
}