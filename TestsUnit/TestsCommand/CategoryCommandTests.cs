namespace TestsUnit.TestsCommand;

public class CategoryCommandTests
{
    private readonly ManufacturerManagerContext _manufacturerManagerContext;
    private readonly ICategoryCommandHandler _categoryCommandHandler;
    private readonly List<CategoryModel> _testCategories = CategoryObjectFactory.GetTestCategories();
    public CategoryCommandTests()
    {
        _manufacturerManagerContext = TestsUnitHelper.GetContextWithOptions();
        _categoryCommandHandler = new CategoryCommandHandler(_manufacturerManagerContext);
    }

    [Fact]
    public async Task CreateCatgeoryCreatesCategory()
    {
        var initialCount = _manufacturerManagerContext.Categories.Count();

        await _categoryCommandHandler.CreateCategoryAsync(_testCategories[0], false);
        await _categoryCommandHandler.CreateCategoryAsync(_testCategories[1], false);
        await _categoryCommandHandler.CreateCategoryAsync(_testCategories[2], false);
        await _categoryCommandHandler.CreateCategoryAsync(_testCategories[3], true);

        _manufacturerManagerContext.Categories.Count().Should().Be(initialCount + 4);
    }

    [Fact]
    public async Task DeleteCategoryDeletesCategory()
    {
        int categoryId;

        _manufacturerManagerContext.Categories.Add(_testCategories[1]);
        _manufacturerManagerContext.SaveChanges();
        categoryId = _testCategories[1].CategoryId;

        await _categoryCommandHandler.DeleteCategoryAsync(categoryId, true);

        var deletedCategory = _manufacturerManagerContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

        deletedCategory.Should().BeNull();
    }

    [Fact]
    public async Task UpdateCategoryUpdatesCategory()
    {
        var newCategoryName = "NewCategory";

        _manufacturerManagerContext.Categories.Add(_testCategories[0]);
        _manufacturerManagerContext.SaveChanges();
        
        var categoryToUpdate = _manufacturerManagerContext.Categories.First(c => c.CategoryId == _testCategories[0].CategoryId);
        categoryToUpdate.Name = newCategoryName;
        await _categoryCommandHandler.UpdateCategoryAsync(categoryToUpdate, true);
        
        var updatedCategory = _manufacturerManagerContext.Categories.First(c => c.CategoryId == _testCategories[0].CategoryId);
        updatedCategory.Name.Should().Be(newCategoryName);
    }
}
