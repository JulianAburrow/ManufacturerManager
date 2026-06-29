namespace MMUserInterface.Shared.BasePageClasses;

public class AdhocQueryBasePageClass : BasePageClass
{
    [Parameter] public int AdhocQueryId { get; set; }

    protected AdhocQueryModel AdhocQueryModel { get; set; } = new();

    [Inject] protected INaturalLanguageService McpService { get; set; } = default!;

    [Inject] protected IAdhocQueryCommandHandler AdhocQueryCommandHandler { get; set; } = default!;

    [Inject] protected IAdhocQueryQueryHandler AdhocQueryQueryHandler { get; set; } = default!;

    protected string AdhocQuerySingular = "Ad hoc query";


    protected string AdhocQueryPlural = "Ad hoc queries";

    
    protected BreadcrumbItem GetAdhocQueryHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new(AdhocQueryPlural, "/admin/adhocqueries/index", isDisabled);
    }
}