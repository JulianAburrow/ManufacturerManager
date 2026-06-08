namespace MMUserInterface.Components.Pages;

public partial class AdhocQuery
{
    [Inject] McpSqlExecutor SqlExecutor { get; set; } = null!;

    private DataTable? ResultsDataTable;

    private string QueryText = string.Empty;

    private string SqlReturned = string.Empty;

    private string? ErrorMessage = null;

    private bool IsThinking = false;

    protected async override Task OnInitializedAsync()
    {
        MainLayout.SetHeaderValue("Ad Hoc Query");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            new BreadcrumbItem("Ad Hoc Query", "/adhocquery", true)
        ]);
    }

    protected async Task OnQueryClicked()
    {
        IsThinking = true;
        ErrorMessage = null;
        ResultsDataTable = null;

        if (string.IsNullOrWhiteSpace(QueryText))
        {
            ErrorMessage = "Please enter a query.";
            IsThinking = false;
            return;
        }

        try
        {
            SqlReturned = await McpService.GetSqlStringFromNaturalQuery(QueryText);

            // Detect the model's safe refusal message
            if (SqlReturned.Contains("I can only generate safe read-only SELECT queries."))
            {
                ErrorMessage = SqlReturned; // Show the model's own message
                return;                     // Do NOT execute SQL
            }

            ResultsDataTable = await SqlExecutor.ExecuteQueryAsync(SqlReturned);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred while processing the query: {ex.Message}";
        }
        finally
        {
            IsThinking = false;
        }
    }
}