namespace MMUserInterface.Themes;

public class ManufacturerManagerExtraLargeTheme
{
    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1565C0",
            PrimaryDarken = "#003c8f",
            PrimaryLighten = "#5e92f3",
            Secondary = "#F9A825",
            SecondaryDarken = "#c17900",
            SecondaryLighten = "#ffd95a",
            AppbarBackground = "#1565C0",
            AppbarText = "rgba(255,255,255,0.95)",
            DrawerBackground = "#FFFFFF",
            DrawerText = "rgba(0,0,0,0.72)",
            DrawerIcon = "#1565C0",
            Background = "#F5F5F5",
            Surface = "#FFFFFF",
            Success = "#2E7D32",
            Error = "#C62828",
            Warning = "#E65100",
            Info = "#0277BD",
            TextPrimary = "rgba(0,0,0,0.87)",
            TextSecondary = "rgba(0,0,0,0.60)",
            ActionDefault = "rgba(0,0,0,0.54)",
            Divider = "rgba(0,0,0,0.12)",
        },

        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
                FontSize = "1.25rem",        // was 0.875rem
                FontWeight = "400",
                LineHeight = "1.60",
                LetterSpacing = "0.01071em",
            },

            H1 = new H1Typography { FontSize = "8rem", FontWeight = "300", LineHeight = "1.22" },       // was 6rem
            H2 = new H2Typography { FontSize = "5rem", FontWeight = "300", LineHeight = "1.28" },       // was 3.75rem
            H3 = new H3Typography { FontSize = "4rem", FontWeight = "400", LineHeight = "1.22" },       // was 3rem
            H4 = new H4Typography { FontSize = "3rem", FontWeight = "400", LineHeight = "1.32" },       // was 2.125rem
            H5 = new H5Typography { FontSize = "2.25rem", FontWeight = "400", LineHeight = "1.42" },    // was 1.5rem
            H6 = new H6Typography { FontSize = "1.75rem", FontWeight = "500", LineHeight = "1.70" },    // was 1.25rem

            Subtitle1 = new Subtitle1Typography { FontSize = "1.5rem", FontWeight = "400", LineHeight = "1.85" }, // was 1rem
            Subtitle2 = new Subtitle2Typography { FontSize = "1.25rem", FontWeight = "500", LineHeight = "1.65" }, // was 0.875rem

            Body1 = new Body1Typography { FontSize = "1.5rem", FontWeight = "400", LineHeight = "1.60" }, // was 1rem
            Body2 = new Body2Typography { FontSize = "1.25rem", FontWeight = "400", LineHeight = "1.55" }, // was 0.875rem

            Button = new ButtonTypography
            {
                FontSize = "1.25rem",        // was 0.875rem
                FontWeight = "500",
                LineHeight = "1.95",
                LetterSpacing = "0.02857em",
                TextTransform = "uppercase"
            },

            Caption = new CaptionTypography
            {
                FontSize = "1rem",           // was 0.75rem
                FontWeight = "400",
                LineHeight = "1.75"
            },
        },

        LayoutProperties = new LayoutProperties
        {
            DrawerWidthLeft = "240px",
            AppbarHeight = "64px",
        },

        Shadows = new Shadow(),
        ZIndex = new ZIndex(),
    };
}
