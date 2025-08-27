namespace MMUserInterface.Components.Pages.Admin.Errors;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        ErrorModel = await ErrorQueryHandler.GetErrorAsync(ErrorId);
        MainLayout.SetHeaderValue("Delete Error");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetErrorHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);
    }

    private async Task DeleteError()
    {
        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ErrorCommandHandler.DeleteErrorAsync(ErrorId, true),
            $"Error {ErrorModel.ErrorMessage} successfully deleted",
            $"An error occurred deleting error {ErrorModel.ErrorMessage}. Please try again"
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/errors/index");
    }
}