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
    public async Task GetCategoriesAsync_ReturnsAllCategories_OrderedByName()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.Categories.AddRange(_testCategories);
            await context.SaveChangesAsync();
        }

        // Act
        var categories = await _categoryQueryHandler.GetCategoriesAsync();

        // Assert
        categories.Should().NotBeNull();
        categories.Should().HaveCount(_testCategories.Count);

        // Ensure ordering by Name
        categories.Should().BeInAscendingOrder(c => c.Name);
    }

    [Fact]
    public async Task GetCategoryAsync_ReturnsCategory_WhenFound()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.Categories.Add(_testCategories[0]);
            await context.SaveChangesAsync();
        }

        var id = _testCategories[0].CategoryId;

        // Act
        var category = await _categoryQueryHandler.GetCategoryAsync(id);

        // Assert
        category.Should().NotBeNull();
        category.CategoryId.Should().Be(id);
        category.Name.Should().Be(_testCategories[0].Name);
    }

    [Fact]
    public async Task GetCategoryAsync_ReturnsEmptyModel_WhenNotFound()
    {
        // Act
        var category = await _categoryQueryHandler.GetCategoryAsync(-1);

        // Assert
        category.Should().NotBeNull();
        category.Should().BeOfType<CategoryModel>();
        category.CategoryId.Should().Be(0);
        category.Name.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task CategoryExistsAsync_ReturnsTrue_WhenCategoryExists()
    {
        // Arrange
        await using (var context = await _factory.CreateDbContextAsync())
        {
            context.Categories.Add(_testCategories[0]);
            await context.SaveChangesAsync();
        }

        // Act
        var exists = await _categoryQueryHandler.CategoryExistsAsync(_testCategories[0].Name);

        // Assert
        exists.Should().BeTrue();
    }

    [Fact]
    public async Task CategoryExistsAsync_ReturnsFalse_WhenCategoryDoesNotExist()
    {
        // Act
        var exists = await _categoryQueryHandler.CategoryExistsAsync("NonExistentCategory");

        // Assert
        exists.Should().BeFalse();
    }
}
