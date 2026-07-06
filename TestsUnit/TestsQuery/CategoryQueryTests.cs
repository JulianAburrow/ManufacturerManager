namespace TestsUnit.TestsQuery;

public class CategoryQueryTests
{
    private readonly IDbContextFactory<ManufacturerManagerContext> _factory;
    private readonly ICategoryQueryHandler _categoryQueryHandler;
    private readonly List<CategoryModel> _testCategories = CategoryObjectFactory.GetTestCategories();
    
    public CategoryQueryTests()
    {
        _factory = TestsUnitHelper.GetInMemoryFactory();
        _categoryQueryHandler = new CategoryQueryHandler(_factory);
    }

    [Fact]
    public async Task GetCategories_GetsCategories()
    {
        await using var _manufacturerManagerContext = await _factory.CreateDbContextAsync();
        var initialCount = _manufacturerManagerContext.Categories.Count();

        _manufacturerManagerContext.Categories.Add(_testCategories[0]);
        _manufacturerManagerContext.Categories.Add(_testCategories[1]);
        _manufacturerManagerContext.Categories.Add(_testCategories[2]);
        _manufacturerManagerContext.Categories.Add(_testCategories[3]);
        _manufacturerManagerContext.SaveChanges();

        var categories = await _categoryQueryHandler.GetCategoriesAsync();

        categories.Count.Should().Be(initialCount + 4);
    }
}
