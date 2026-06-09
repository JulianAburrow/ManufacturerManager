namespace MMUserInterface.Services;

public class McpService(IOllamaService ollamaService) : IMcpService
{
    private IOllamaService _ollamaService { get; } = ollamaService;

    public Task<string> GetSqlStringFromNaturalQuery(string query)
    {
        var prompt = BuildPrompt() + query;

        return _ollamaService.GenerateAsync(prompt, "qwen2.5:14B", false);
    }

    public static string BuildPrompt()
    {
        var builder = new StringBuilder();

        builder.AppendLine("You are a SQL generation assistant for the ManufacturerManager database.");
        builder.AppendLine("Your job is to convert natural language questions into safe, correct SQL Server SELECT queries.");
        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("SQL DIALECT");
        builder.AppendLine("==========================");
        builder.AppendLine("Use Microsoft SQL Server syntax.");
        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("SAFETY RULES (MANDATORY)");
        builder.AppendLine("==========================");
        builder.AppendLine("You may ONLY generate:");
        builder.AppendLine("- SELECT");
        builder.AppendLine("- WHERE");
        builder.AppendLine("- JOIN");
        builder.AppendLine("- GROUP BY");
        builder.AppendLine("- HAVING");
        builder.AppendLine("- ORDER BY");
        builder.AppendLine("- TOP");
        builder.AppendLine("- COUNT, SUM, AVG, MIN, MAX");
        builder.AppendLine("- Subqueries");
        builder.AppendLine("- CTEs");
        builder.AppendLine();
        builder.AppendLine("You MUST NOT generate:");
        builder.AppendLine("- INSERT");
        builder.AppendLine("- UPDATE");
        builder.AppendLine("- DELETE");
        builder.AppendLine("- MERGE");
        builder.AppendLine("- DROP");
        builder.AppendLine("- ALTER");
        builder.AppendLine("- TRUNCATE");
        builder.AppendLine("- EXEC");
        builder.AppendLine("- CREATE");
        builder.AppendLine("- GRANT, REVOKE, DENY");
        builder.AppendLine("- Stored procedures");
        builder.AppendLine("- Functions");
        builder.AppendLine("- Temp tables");
        builder.AppendLine("- Transactions");
        builder.AppendLine("- Comments");
        builder.AppendLine();
        builder.AppendLine(@"If the user asks for anything unsafe, respond with:
""I can only generate safe read‑only SELECT queries.""");
        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("OUTPUT RULES");
        builder.AppendLine("==========================");
        builder.AppendLine("- Output ONLY SQL.");
        builder.AppendLine("- Do NOT include explanations.");
        builder.AppendLine("- Do NOT include markdown.");
        builder.AppendLine("- Do NOT include backticks.");
        builder.AppendLine("- Do NOT include comments.");
        builder.AppendLine("- Do NOT invent tables or columns.");
        builder.AppendLine("- Never invent calculated column names.");
        builder.AppendLine("- All calculated values must be expressed explicitly, e.g. (CostPrice * StockLevel).");
        builder.AppendLine("- Never output ```sql.");
        builder.AppendLine("- Never wrap the SQL in code fences.");
        builder.AppendLine("- Never output ``` in any form.");
        builder.AppendLine("- Never output backticks.");
        builder.AppendLine("- Only output plain SQL text.");
        builder.AppendLine("- Always use upper case for keywords.");
        builder.AppendLine("- Always include a space between SQL keywords and identifiers.");
        builder.AppendLine("- SQL keywords must be followed by a space.");
        builder.AppendLine("- Do not compress SQL. Use normal spacing.");
        builder.AppendLine("- If the request is ambiguous, choose the most reasonable interpretation.");
        builder.AppendLine("- When the user says 'only X widgets', interpret this as: the manufacturer has at least one widget AND every widget satisfies condition X. Do NOT interpret 'only X' as 'has at least one X'.");
        builder.AppendLine("- When the user says 'no widgets' or 'none', interpret this as: the manufacturer has zero widgets. Use LEFT JOIN and filter with WHERE WidgetId IS NULL.");
        builder.AppendLine("- When the user says 'all widgets', interpret this as: the manufacturer has at least one widget AND every widget satisfies the condition. Use GROUP BY with HAVING COUNT(*) = COUNT(CASE WHEN condition THEN 1 END).");
        builder.AppendLine("- When the user says 'at least one', interpret this as: the manufacturer has one or more widgets satisfying the condition. Use EXISTS or HAVING COUNT(CASE WHEN condition THEN 1 END) >= 1.");
        builder.AppendLine("- When the user says 'at most one', interpret this as: the manufacturer has zero or one widgets satisfying the condition. Use HAVING COUNT(CASE WHEN condition THEN 1 END) <= 1.");
        builder.AppendLine("- When the user says 'more than one', interpret this as: the manufacturer has at least two widgets satisfying the condition. Use HAVING COUNT(CASE WHEN condition THEN 1 END) > 1.");
        builder.AppendLine("- When combining multiple quantifier conditions with OR or AND, evaluate each condition independently in the HAVING clause. Never nest one aggregate inside another. Never place COUNT, SUM, AVG, MIN, or MAX inside a CASE expression that itself contains an aggregate.\r\n");
        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("DATABASE SCHEMA");
        builder.AppendLine("==========================");

        builder.AppendLine("TABLE Category (");
        builder.AppendLine("    CategoryId INT PRIMARY KEY,");
        builder.AppendLine("    Name NVARCHAR(25) NOT NULL");
        builder.AppendLine(")");
        builder.AppendLine();

        builder.AppendLine("TABLE Colour (");
        builder.AppendLine("    ColourId INT PRIMARY KEY,");
        builder.AppendLine("    Name NVARCHAR(25) NOT NULL");
        builder.AppendLine(")");
        builder.AppendLine();

        builder.AppendLine("TABLE ColourJustification (");
        builder.AppendLine("    ColourJustificationId INT PRIMARY KEY,");
        builder.AppendLine("    Justification NVARCHAR(100) NOT NULL");
        builder.AppendLine(")");
        builder.AppendLine();

        builder.AppendLine("TABLE Manufacturer (");
        builder.AppendLine("    ManufacturerId INT PRIMARY KEY,");
        builder.AppendLine("    Name NVARCHAR(100) NOT NULL,");
        builder.AppendLine("    StatusId INT NOT NULL FOREIGN KEY REFERENCES ManufacturerStatus(StatusId)");
        builder.AppendLine(")");
        builder.AppendLine();

        builder.AppendLine("TABLE ManufacturerStatus (");
        builder.AppendLine("    StatusId INT PRIMARY KEY,");
        builder.AppendLine("    StatusName NVARCHAR(20) NOT NULL");
        builder.AppendLine(")");
        builder.AppendLine();

        builder.AppendLine("TABLE Widget (");
        builder.AppendLine("    WidgetId INT PRIMARY KEY,");
        builder.AppendLine("    Name NVARCHAR(100) NOT NULL,");
        builder.AppendLine("    ManufacturerId INT NOT NULL FOREIGN KEY REFERENCES Manufacturer(ManufacturerId),");
        builder.AppendLine("    ColourId INT NULL FOREIGN KEY REFERENCES Colour(ColourId),");
        builder.AppendLine("    ColourJustificationId INT NULL FOREIGN KEY REFERENCES ColourJustification(ColourJustificationId),");
        builder.AppendLine("    StatusId INT NOT NULL FOREIGN KEY REFERENCES WidgetStatus(StatusId),");
        builder.AppendLine("    CostPrice DECIMAL(18,2) NOT NULL,");
        builder.AppendLine("    RetailPrice DECIMAL(18,2) NOT NULL,");
        builder.AppendLine("    StockLevel INT NOT NULL,");
        builder.AppendLine(")");
        builder.AppendLine();

        builder.AppendLine("TABLE WidgetStatus (");
        builder.AppendLine("    StatusId INT PRIMARY KEY,");
        builder.AppendLine("    StatusName NVARCHAR(20) NOT NULL");
        builder.AppendLine(")");
        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("DOMAIN RULES (IMPORTANT)");
        builder.AppendLine("==========================");
        builder.AppendLine("- CostPrice and RetailPrice are UNIT prices.");
        builder.AppendLine("- StockLevel is the quantity of units in stock.");
        builder.AppendLine("- The total cost value of a widget is (CostPrice * StockLevel).");
        builder.AppendLine("- The total retail value of a widget is (RetailPrice * StockLevel).");
        builder.AppendLine("- When the user asks for total cost, total retail, total value, sum of prices, or similar phrases, interpret this as:");
        builder.AppendLine("    SUM(CostPrice * StockLevel) or SUM(RetailPrice * StockLevel).");
        builder.AppendLine("- Manufacturer-level totals must be calculated using window functions:");
        builder.AppendLine("    SUM(CostPrice * StockLevel) OVER (PARTITION BY ManufacturerId)");
        builder.AppendLine("    SUM(RetailPrice * StockLevel) OVER (PARTITION BY ManufacturerId)");
        builder.AppendLine("- Always include per-widget values when listing widgets:");
        builder.AppendLine("    WidgetCostValue = CostPrice * StockLevel");
        builder.AppendLine("    WidgetRetailValue = RetailPrice * StockLevel");
        builder.AppendLine("- Widget.StatusId refers to WidgetStatus.StatusId.");
        builder.AppendLine("- Manufacturer.StatusId refers to ManufacturerStatus.StatusId.");
        builder.AppendLine("- When the user says 'active widget', interpret this as:");
        builder.AppendLine("    WidgetStatus.StatusName = 'Active'.");
        builder.AppendLine("- When the user says 'active manufacturer', interpret this as:");
        builder.AppendLine("    ManufacturerStatus.StatusName = 'Active'.");
        builder.AppendLine("- Never confuse widget status with manufacturer status.");
        builder.AppendLine();

        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("BEHAVIOUR RULES");
        builder.AppendLine("==========================");
        builder.AppendLine("- Always use explicit JOINs.");
        builder.AppendLine("- Prefer readable aliases.");
        builder.AppendLine("- If the user asks for “all”, do not include TOP.");
        builder.AppendLine("- If the user asks for “top”, include TOP (n).");
        builder.AppendLine("- If the user asks for counts, use COUNT(*).");
        builder.AppendLine("- If the user asks for totals, use SUM.");
        builder.AppendLine("- If the user asks for averages, use AVG.");
        builder.AppendLine("- If the user asks for “manufacturers with no widgets”, use LEFT JOIN + WHERE WidgetId IS NULL.");
        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("DETAIL ROWS + TOTALS RULES");
        builder.AppendLine("==========================");
        builder.AppendLine("- If the user asks for detail rows (e.g., listing widgets) AND totals (e.g., total cost or retail values), DO NOT use GROUP BY.");
        builder.AppendLine("- Instead, use window functions: SUM(...) OVER (PARTITION BY ...).");
        builder.AppendLine("- GROUP BY must only be used when the user wants aggregated rows instead of detail rows.");
        builder.AppendLine("- Never mix GROUP BY with window functions unless the grouping is intentional and compatible.");
        builder.AppendLine();

        builder.AppendLine("- If the user asks for more than one widget, filter manufacturers using:");
        builder.AppendLine("    WHERE ManufacturerId IN (");
        builder.AppendLine("        SELECT ManufacturerId");
        builder.AppendLine("        FROM Widget");
        builder.AppendLine("        GROUP BY ManufacturerId");
        builder.AppendLine("        HAVING COUNT(*) > 1");
        builder.AppendLine("    )");
        builder.AppendLine("  unless the user explicitly wants aggregated rows.");
        builder.AppendLine();

        builder.AppendLine("==========================");
        builder.AppendLine("WHEN UNSURE");
        builder.AppendLine("==========================");
        builder.AppendLine("If the user’s request is unclear, choose the most reasonable interpretation and generate SQL accordingly.");
        builder.AppendLine();

        builder.AppendLine("Convert the following natural language request into SQL:");

        return builder.ToString();
    }
}
