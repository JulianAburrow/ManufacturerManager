namespace MMUserInterface.Theme;

public class ManufacturerManagerTheme
{
    public static MudTheme Theme => new MudTheme
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1565C0",   // Material Blue 800 - deep, professional
            PrimaryDarken = "#003c8f",
            PrimaryLighten = "#5e92f3",
            Secondary = "#F9A825",   // Material Amber 800 - gold/cinematic
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

        PaletteDark = new PaletteDark
        {
            Primary = "#90CAF9",   // Blue 200 - readable on dark bg
            PrimaryDarken = "#5d99c6",
            PrimaryLighten = "#c3fdff",
            Secondary = "#FFD54F",   // Amber 300 - gold on dark
            SecondaryDarken = "#c8a415",
            SecondaryLighten = "#ffff81",
            AppbarBackground = "#1A237E",   // Indigo 900 - dark cinematic blue
            AppbarText = "rgba(255,255,255,0.95)",
            DrawerBackground = "#1A1A2E",
            DrawerText = "rgba(255,255,255,0.72)",
            DrawerIcon = "#90CAF9",
            Background = "#121212",
            Surface = "#1E1E1E",
            Success = "#66BB6A",
            Error = "#EF5350",
            Warning = "#FFA726",
            Info = "#29B6F6",
            TextPrimary = "rgba(255,255,255,0.87)",
            TextSecondary = "rgba(255,255,255,0.60)",
            ActionDefault = "rgba(255,255,255,0.54)",
            Divider = "rgba(255,255,255,0.12)",
        },

        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
                FontSize = "0.875rem",
                FontWeight = "400",
                LineHeight = "1.43",
                LetterSpacing = "0.01071em",
            },
            H1 = new H1Typography { FontSize = "6rem", FontWeight = "300", LineHeight = "1.167" },
            H2 = new H2Typography { FontSize = "3.75rem", FontWeight = "300", LineHeight = "1.2" },
            H3 = new H3Typography { FontSize = "3rem", FontWeight = "400", LineHeight = "1.167" },
            H4 = new H4Typography { FontSize = "2.125rem", FontWeight = "400", LineHeight = "1.235" },
            H5 = new H5Typography { FontSize = "1.5rem", FontWeight = "400", LineHeight = "1.334" },
            H6 = new H6Typography { FontSize = "1.25rem", FontWeight = "500", LineHeight = "1.6" },
            Subtitle1 = new Subtitle1Typography { FontSize = "1rem", FontWeight = "400", LineHeight = "1.75" },
            Subtitle2 = new Subtitle2Typography { FontSize = "0.875rem", FontWeight = "500", LineHeight = "1.57" },
            Body1 = new Body1Typography { FontSize = "1rem", FontWeight = "400", LineHeight = "1.5" },
            Body2 = new Body2Typography { FontSize = "0.875rem", FontWeight = "400", LineHeight = "1.43" },
            Button = new ButtonTypography { FontSize = "0.875rem", FontWeight = "500", LineHeight = "1.75", LetterSpacing = "0.02857em", TextTransform = "uppercase" },
            Caption = new CaptionTypography { FontSize = "0.75rem", FontWeight = "400", LineHeight = "1.66" },
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
