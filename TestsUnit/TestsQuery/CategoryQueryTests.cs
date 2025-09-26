namespace TestsUnit.TestsQuery;

public class CategoryQueryTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly ICategoryQueryHandler _categoryQueryHandler;
    private readonly List<CategoryModel> _testCategories = CategoryObjectFactory.GetTestCategories();
    
    public CategoryQueryTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _categoryQueryHandler = new CategoryQueryHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task GetCategoriesGetsCategories()
    {
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
