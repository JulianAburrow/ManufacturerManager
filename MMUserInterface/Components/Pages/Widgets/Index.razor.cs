﻿namespace MMUserInterface.Components.Pages.Widgets;

public partial class Index
{
    protected List<WidgetModel> Widgets { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Widgets = await WidgetQueryHandler.GetWidgetsAsync();
        Snackbar.Add($"{Widgets.Count} item(s) found.", Widgets.Count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Widgets");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetWidgetHomeBreadcrumbItem(true),
        ]);
    }
}
