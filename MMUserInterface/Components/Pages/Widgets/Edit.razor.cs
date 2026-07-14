namespace MMUserInterface.Components.Pages.Widgets;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {

        WidgetModel = await WidgetQueryHandler.GetWidgetAsync(WidgetId);

        if (WidgetModel.WidgetId == 0)
        {
            MainLayout.SetHeaderValue(WidgetNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        WidgetStatuses = await WidgetStatusQueryHandler.GetWidgetStatusesAsync();
        WidgetStatuses.Insert(0, new WidgetStatusModel
        {
            StatusId = SharedValues.PleaseSelectValue,
            StatusName = SharedValues.PleaseSelectText,
        });
        Colours = await ColourHandler.GetColoursAsync();
        Colours.Insert(0, new ColourModel
        {
            ColourId = SharedValues.NoneValue,
            Name = SharedValues.NoneText,
        });
        ColourJustifications = await ColourJustificationHandler.GetColourJustificationsAsync();
        ColourJustifications.Insert(0, new ColourJustificationModel
        {
            ColourJustificationId = SharedValues.NoneValue,
            Justification = SharedValues.NoneText,
        });
        Manufacturers = await ManufacturerHandler.GetManufacturersAsync();

        CopyModelToDisplayModel();

        MainLayout.SetHeaderValue("Edit Widget");

        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetWidgetHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(EditTextForBreadcrumb),
        ]);
    }

    private async Task UpdateWidget()
    {
        CopyDisplayModelToModel();

        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await WidgetCommandHandler.UpdateWidgetAsync(WidgetModel),
            $"Widget {WidgetModel.Name} successfully updated.",
            $"An error occurred updating widget {WidgetModel.Name}. Please try again."
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/widgets/index");
    }
}
