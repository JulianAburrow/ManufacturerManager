namespace MMUserInterface.Shared.BasePageClasses;

public abstract class ColourBasePageClass : BasePageClass
{    
    [Inject] protected IColourCommandHandler ColourCommandHandler { get; set; } = default!;

    [Inject] protected IColourQueryHandler ColourQueryHandler { get; set; } = default!;

    [Parameter] public int ColourId { get; set; }

    protected ColourModel ColourModel { get; set; } = new();

    protected ColourDisplayModel ColourDisplayModel { get; set; } = new();

    protected string ColourSingular = "Colour";

    protected string ColourPlural = "Colours";

    protected BreadcrumbItem GetColourHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new (ColourPlural, "/colours/index", isDisabled);
    }

    protected void CopyModelToDisplayModel()
    {
        ColourDisplayModel.ColourId = ColourModel.ColourId;
        ColourDisplayModel.Name = ColourModel.Name;
    }

    protected void CopyDisplayModelToModel()
    {
        ColourModel.Name = ColourDisplayModel.Name;
    }
}
