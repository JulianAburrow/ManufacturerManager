using System.Threading.Tasks;

namespace MMUserInterface.Components.Pages.Admin.Errors;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        ErrorModel = await ErrorQueryHandler.GetErrorAsync(ErrorId);
        CopyModelToDisplayModel();
        MainLayout.SetHeaderValue("Edit Error");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetErrorHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(EditTextForBreadcrumb),
        ]);
    }

    private void SetResolvedDate()
    {
        ErrorDisplayModel.ResolvedDate = ErrorDisplayModel.Resolved
            ? DateTime.Now
            : null;
    }

    private async Task UpdateError()
    {
        CopyDisplayModelToModel();

        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ErrorCommandHandler.UpdateErrorAsync(ErrorModel, true),
            $"Error {ErrorModel.ErrorId} successfully updated.",
            $"An error occurred updating error {ErrorModel.ErrorId}. Please try again."
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/errors/index");
    }
}