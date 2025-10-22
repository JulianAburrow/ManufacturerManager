namespace TestsPlaywright.Helpers;

public static class PlaywrightTestHelper
{
    public static async Task<IPage> CreatePageAsync()
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = GlobalValues.IsHeadless
        });
        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        });

        var page = await context.NewPageAsync();
        page.SetDefaultTimeout(GlobalValues.GlobalTimeOut);
        page.Request += (_, request) =>
        {
            Console.WriteLine($"Request: {request.Url}");
        };
        return page;
    }

    public static DbContextOptions<ManufacturerManagerContext> GetContextOptions()
    {
        return new DbContextOptionsBuilder<ManufacturerManagerContext>()
            .UseSqlServer("Server=localhost,11433;initial catalog=ManufacturerManager;persist security info=True;User Id=sa;Password=Pwd12345!;multipleactiveresultsets=True;TrustServerCertificate=true",
                options => options.EnableRetryOnFailure())
            .Options;
    }

    public static async Task DisposeBrowserAndContext(IPage page)
    {
        if (page != null)
        {
            await page.CloseAsync();            
        }

        var context = page?.Context;
        if (context != null)
        {
            await context.CloseAsync();
        }

        var browser = context?.Browser;
        if (browser != null)
        {
            await browser.CloseAsync();
        }
    }
}