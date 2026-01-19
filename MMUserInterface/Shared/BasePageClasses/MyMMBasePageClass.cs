namespace MMUserInterface.Shared.BasePageClasses;

public abstract class MyMMBasePageClass : BasePageClass
{
    [Inject] protected IMyMMQueryHandler MyMMQueryHandler { get; set; } = default!;

    [Inject] protected IMyMMCommandHandler MyMMCommandHandler { get; set; } = default!;

    [Inject] protected IMyMMStatusQueryHandler MyMMStatusQueryHandler { get; set; } = default!;

    [Parameter] public int MyMMId { get; set; }

    protected MyMMModel MyMMModel { get; set; } = new();

    protected MyMMDisplayModel MyMMDisplayModel { get; set; } = new();

    protected List<MyMMModel>? MyMMs { get; set; }

    public required List<MyMMStatusModel> MyMMStatuses { get; set; }

    protected string MyMM = "MyMM";

    protected string MyMMPlural = "MyMMs";

    protected void CopyDisplayModelToModel()
    {
        MyMMModel.Title = MyMMDisplayModel.Title;
        MyMMModel.URL = MyMMDisplayModel.URL;
        MyMMModel.Notes = MyMMDisplayModel.Notes;
        MyMMModel.ActionDate = MyMMDisplayModel.ActionDate;
        MyMMModel.IsExternal = MyMMDisplayModel.IsExternal;
        MyMMModel.StatusId = MyMMDisplayModel.StatusId;
    }

    protected void CopyModelToDisplayModel()
    {
        MyMMDisplayModel.MyMMId = MyMMId;
        MyMMDisplayModel.Title = MyMMModel.Title;
        MyMMDisplayModel.URL = MyMMModel.URL;
        MyMMDisplayModel.Notes = MyMMModel.Notes;
        MyMMDisplayModel.ActionDate = MyMMModel.ActionDate;
        MyMMDisplayModel.IsExternal = MyMMModel.IsExternal;
        MyMMDisplayModel.StatusId = MyMMModel.StatusId;
    }

    protected BreadcrumbItem GetMyMMHomeBreadcrumbItem(bool isActive = false)
    {
        return new BreadcrumbItem("MyMMs", "/mymm/index", isActive);
    }
}
