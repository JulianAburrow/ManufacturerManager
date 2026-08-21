namespace MMUserInterface.Components.Pages;

public partial class AdhocQuery
{
    [Inject] private McpSqlExecutor SqlExecutor { get; set; } = null!;

    [Inject] private ILlmClient LlmClient { get; set; } = null!;

    private DataTable? ResultsDataTable;

    private string QueryText = string.Empty;

    private string SqlReturned = string.Empty;

    private string? ErrorMessage = null;

    private string? MessageToDisplay = null;

    private bool IsThinking = false;

    private bool ShowLastXPanel = true;

    private CancellationTokenSource? _cts;

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
        ErrorMessage = null;

        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        if (string.IsNullOrWhiteSpace(QueryText))
        {
            MessageToDisplay = "Please enter a query.";
            IsThinking = false;
            return;
        }

        try
        {
            SqlReturned = await McpService.GetSqlStringFromNaturalQuery(QueryText, _cts.Token);

            if (SqlReturned.StartsWith("CANCELLATION"))
            {
                var strippedMessage = SqlReturned.Substring("CANCELLATION".Length).TrimStart();
                MessageToDisplay = strippedMessage;
                ErrorMessage = strippedMessage;
                SqlReturned = null;
                await LogAdhocQuery();
                return;
            }

            // Detect refusal messages from the model or service
            // Detect any refusal message
            if (SqlReturned.StartsWith("REFUSAL:"))
            {
                MessageToDisplay = SqlReturned;
                ErrorMessage = SqlReturned;
                SqlReturned = null;
                await LogAdhocQuery();
                return;
            }

            ResultsDataTable = await SqlExecutor.ExecuteQueryAsync(SqlReturned);

            // SUCCESS LOG
            await LogAdhocQuery();
        }
        catch (SqlException ex)
        {
            MessageToDisplay = "There was an error executing the command.";
            ErrorMessage = ex.Message;
            SqlReturned = SqlReturned;
            await LogAdhocQuery();       // log failure
        }
        catch (Exception ex)
        {
            MessageToDisplay = "An error occurred while processing the query.";
            ErrorMessage = ex.Message;
            SqlReturned = SqlReturned;
            await LogAdhocQuery();       // log failure
        }
        finally
        {
            IsThinking = false;
            await GetLastXSuccessfulAdhocQueries();
            StateHasChanged();
        }
    }

    private async Task LogAdhocQuery()
    {
        var adhocQueryModel = new AdhocQueryModel
        {
            NaturalLanguageQuery = QueryText,
            Message = ErrorMessage,
            SqlReturned = SqlReturned,
            IsSuccessful = ErrorMessage is null,
            WhenRun = DateTime.UtcNow,
            AiProvider = LlmClient.GetType().Name,
        };

        await AdhocQueryCommandHandler.CreateAdhocQueryAsync(adhocQueryModel);
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

    protected void OnCancelClicked()
    {
        _cts?.Cancel();
        IsThinking = false;
        ShowLastXPanel = true;
        SqlReturned = null;
    }
}