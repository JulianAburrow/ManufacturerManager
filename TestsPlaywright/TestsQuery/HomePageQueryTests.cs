namespace TestsPlaywright.TestsQuery;

public class HomePageQueryTests
{
    [Fact]
    public async Task HomePageLoads()
    {
        var page = await PlaywrightTestHelper.CreatePageAsync();

        await page.GotoAsync(GlobalValues.BaseUrl, GlobalValues.GetPageOptions());
        var title = await page.TitleAsync();

        Assert.Equal("Manufacturer Manager", title);

        await PlaywrightTestHelper.DisposeBrowserAndContext(page);
    }
}
