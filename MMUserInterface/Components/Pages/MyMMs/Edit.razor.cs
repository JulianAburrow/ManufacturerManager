namespace MMUserInterface.Components.Pages.MyMMs;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        MyMMModel = await MyMMQueryHandler.GetMyMMAsync(MyMMId);
        MyMMStatuses = await MyMMStatusQueryHandler.GetMyMMStatusesAsync();
        CopyModelToDisplayModel();
        MainLayout.SetHeaderValue("Edit MyMM");
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
            async () => await MyMMCommandHandler.UpdateMyMMAsync(MyMMModel, true),
            $"MyMM {MyMMModel.Title} successfully updated.",
            $"An error occurred updating MyMM {MyMMModel.Title}. Please try again."
        );
        NavigationManager.NavigateTo("/mymms/index");
    }
}