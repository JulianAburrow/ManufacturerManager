namespace MMUserInterface.Components.Pages.MyMMs;

public partial class Create
{
    protected override async Task OnInitializedAsync()
    {
        MyMMStatuses = await MyMMStatusQueryHandler.GetMyMMStatusesAsync();
        MyMMStatuses.Insert(0, new()
        {
            StatusId = SharedValues.PleaseSelectValue,
            StatusName = SharedValues.PleaseSelectText,
        });
        MyMMDisplayModel.StatusId = SharedValues.PleaseSelectValue;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetHeaderValue("Create MyMM");
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetMyMMHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb),
        ]);
    }

    private async Task CreateMyMM()
    {
        CopyDisplayModelToModel();

        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await MyMMCommandHandler.CreateMyMMAsync(MyMMModel, true),
            $"MyMM {MyMMModel.Title} successfully created.",
            $"An error occurred creating MyMM {MyMMModel.Title}. Please try again."
        );

        NavigationManager.NavigateTo("/mymms/index");
    }
}