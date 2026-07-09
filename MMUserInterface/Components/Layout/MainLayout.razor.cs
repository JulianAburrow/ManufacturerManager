using MMUserInterface.Themes;

namespace MMUserInterface.Components.Layout;

public partial class MainLayout
{
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    [Inject] IMyMMCommandHandler MyMMCommandHandler { get; set; } = default!;

    [Inject] ISnackbar Snackbar { get; set; } = default!;

    private MudThemeProvider _mudThemeProvider = null!;

    private string CurrentTheme { get; set; } = ThemeEnum.Standard.ToString();

    private MudTheme ActiveTheme = ManufacturerManagerStandardTheme.Theme;

    bool _drawerOpen = true;

    private void DrawerToggle() => _drawerOpen = !_drawerOpen;

    private readonly List<BreadcrumbItem> BreadCrumbs = [];

    private string HeaderText { get; set; } = null!;

    public void SetHeaderValue(string headerText)
    {
        HeaderText = headerText;
        StateHasChanged();
    }

    public void SetBreadcrumbs(List<BreadcrumbItem> breadcrumbs)
    {
        BreadCrumbs.Clear();
        BreadCrumbs.AddRange(breadcrumbs);
    }

    private async Task SaveToMyMM()
    {
        var myMM = new MyMMModel
        {
            Title = $"Saved {HeaderText} Page",
            URL = NavigationManager.Uri,
            Notes = "Saved from My Manufacturer",
            ActionDate = DateTime.Today.AddDays(7),
            IsExternal = false,
            StatusId = (int)SharedValues.MyMMStatuses.Pending,
        };

        await MyMMCommandHandler.CreateMyMMAsync(myMM);
        Snackbar.Add("Page saved to MyMM successfully.", Severity.Success);
    }

    private void OnThemeChanged(string theme)
    {
        CurrentTheme = theme;

        ActiveTheme = theme switch
        {
            nameof(ThemeEnum.Standard) => ManufacturerManagerStandardTheme.Theme,
            nameof(ThemeEnum.Large) => ManufacturerManagerLargeTheme.Theme,
            nameof(ThemeEnum.ExtraLarge) => ManufacturerManagerExtraLargeTheme.Theme,
            _ => ManufacturerManagerStandardTheme.Theme
        };
    }
}
