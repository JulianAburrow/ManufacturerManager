using Microsoft.Extensions.ObjectPool;

namespace MMUserInterface.Components.Pages;

public partial class AdhocQuery
{
    [Inject] private McpSqlExecutor SqlExecutor { get; set; } = null!;

    private DataTable? ResultsDataTable;

    private string QueryText = string.Empty;

    private string SqlReturned = string.Empty;

    private string? MessageToDisplay = null;

    private bool IsThinking = false;

    private bool ShowLastXPanel = true;

    private List<AdhocQueryListModel> LastXSuccessfulAdhocQueries { get; set; } = null!;

    protected async override Task OnInitializedAsync()
    {
        MainLayout.SetHeaderValue("Ad Hoc Query");
        await GetLastXSuccessfulAdhocQueries();
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
        MessageToDisplay = null;
        ResultsDataTable = null;
        ShowLastXPanel = false;

        if (string.IsNullOrWhiteSpace(QueryText))
        {
            MessageToDisplay = "Please enter a query.";
            IsThinking = false;
            return;
        }

        try
        {
            SqlReturned = await McpService.GetSqlStringFromNaturalQuery(QueryText);

            // Detect refusal messages from the model or service
            // Detect any refusal message
            if (SqlReturned.StartsWith("REFUSAL:"))
            {
                MessageToDisplay = SqlReturned.Substring("REFUSAL:".Length).Trim();
                await LogAdhocQuery(SqlReturned);
                return;
            }

            ResultsDataTable = await SqlExecutor.ExecuteQueryAsync(SqlReturned);

            // SUCCESS LOG
            await LogAdhocQuery(null);
        }
        catch (SqlException ex)
        {
            MessageToDisplay = "There was an error executing the command. Try simplifying the query.";
            await LogAdhocQuery(ex.Message);       // log failure
        }
        catch (Exception ex)
        {
            MessageToDisplay = "An error occurred while processing the query.";
            await LogAdhocQuery(ex.Message);       // log failure
        }
        finally
        {
            IsThinking = false;
            await GetLastXSuccessfulAdhocQueries();
            StateHasChanged();
        }
    }

    private async Task LogAdhocQuery(string? errorMessage)
    {
        var adhocQueryModel = new AdhocQueryModel
        {
            NaturalLanguageQuery = QueryText,
            MessageOrSqlReturned = errorMessage is null ? SqlReturned : errorMessage,
            IsSuccessful = errorMessage is null,
            WhenRun = DateTime.UtcNow,
        };

        await AdhocQueryCommandHandler.CreateAdhocQueryAsync(adhocQueryModel, true);
    }

    private async Task GetLastXSuccessfulAdhocQueries()
    {
        LastXSuccessfulAdhocQueries = await AdhocQueryQueryHandler.GetLastXSuccessfulAdhocQueries(5);
    }

    private async Task HandleRerunQuery(string queryText)
    {
        ShowLastXPanel = false;
        QueryText = queryText;
        await OnQueryClicked();
    }

}