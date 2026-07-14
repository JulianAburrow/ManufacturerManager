namespace MMUserInterface.Components.Pages.MyMMs;

public partial class Edit
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

        MyMMStatuses = await MyMMStatusQueryHandler.GetMyMMStatusesAsync();
        CopyModelToDisplayModel();
        MainLayout.SetHeaderValue("Edit MyMM");

        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetMyMMHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(EditTextForBreadcrumb),
        ]);
    }

    private async Task UpdateMyMM()
    {
        CopyDisplayModelToModel();
        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await MyMMCommandHandler.UpdateMyMMAsync(MyMMModel),
            $"MyMM {MyMMModel.Title} successfully updated.",
            $"An error occurred updating MyMM {MyMMModel.Title}. Please try again."
        );
        NavigationManager.NavigateTo("/mymms/index");
    }
}