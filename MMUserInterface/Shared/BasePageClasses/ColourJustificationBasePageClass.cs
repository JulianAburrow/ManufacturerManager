namespace MMUserInterface.Shared.BasePageClasses;

public class ColourJustificationBasePageClass : BasePageClass
{
    [Inject] protected IColourJustificationCommandHandler ColourJustificationCommandHandler { get; set; } = default!;

    [Inject] protected IColourJustificationQueryHandler ColourJustificationQueryHandler { get; set; } = default!;

    [Parameter] public int ColourJustificationId {  get; set; }

    protected ColourJustificationModel ColourJustificationModel { get; set; } = new();

    protected ColourJustificationDisplayModel ColourJustificationDisplayModel { get; set; } = new();

    protected string ColourJustification = "Colour Justification";

    protected string ColourJustificationPlural = "Colour Justifications";

    protected BreadcrumbItem GetColourJustificationHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new (ColourJustificationPlural, "/colourjustifications/index", isDisabled);
    }

    protected void CopyModelToDisplayModel()
    {
        ColourJustificationDisplayModel.Justification = ColourJustificationModel.Justification;
    }

    protected void CopyDisplayModelToModel()
    {
        ColourJustificationModel.Justification = ColourJustificationDisplayModel.Justification;
    }
}
