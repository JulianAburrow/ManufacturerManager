using Microsoft.AspNetCore.Components.Web;

namespace MMUserInterface.Shared.BasePageClasses;

public class CategoryBasePageClass : BasePageClass
{

    [Parameter] public int CategoryId { get; set; }

    [Inject] protected ICategoryHelper CategoryHelper { get; set; } = default!;

    protected CategoryModel CategoryModel { get; set; } = new();

    protected CategoryDisplayModel CategoryDisplayModel { get; set; } = new();

    protected string Category = "Category";

    protected string CategoryPlural = "Categories";

    protected bool CategoryExists { get; set; }

    protected bool OkToDeleteOrEdit { get; set; }

    protected BreadcrumbItem GetCategoryHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new (CategoryPlural, "/categories/index", isDisabled);
    }

    protected void CopyModelToDisplayModel()
    {
        CategoryDisplayModel.CategoryId = CategoryModel.CategoryId;
        CategoryDisplayModel.Name = CategoryModel.Name;
    }

    protected void CopyDisplayModelToModel()
    {
        CategoryModel.Name = CategoryDisplayModel.Name;
    }
}
