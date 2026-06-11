using MMDataAccess.MCP;

namespace MMUserInterface.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlConnections(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ManufacturerManagerContext>(
            options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ManufacturerManager"), sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }));

    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddHttpClient<IOllamaService, OllamaService>();
        services.AddTransient<IAdhocQueryCommandHandler, AdhocQueryCommandHandler>();
        services.AddTransient<IAdhocQueryQueryHandler, AdhocQueryQueryHandler>();
        services.AddTransient<ICategoryCommandHandler, CategoryCommandHandler>();
        services.AddTransient<ICategoryHelper, CategoryHelper>();
        services.AddTransient<ICategoryQueryHandler, CategoryQueryHandler>();
        services.AddTransient<IColourCommandHandler, ColourCommandHandler>();
        services.AddTransient<IColourQueryHandler, ColourQueryHandler>();
        services.AddTransient<IColourJustificationCommandHandler, ColourJustificationCommandHandler>();
        services.AddTransient<IColourJustificationQueryHandler, ColourJustificationQueryHandler>();
        services.AddTransient<ICrudWithErrorHandlingHelper, CrudWithErrorHandlingHelper>();
        services.AddTransient<ICSVStringHelper, CSVStringHelper>();
        services.AddTransient<IErrorCommandHandler, ErrorCommandHandler>();
        services.AddTransient<IErrorQueryHandler, ErrorQueryHandler>();
        services.AddTransient<IHelpDocumentService, HelpDocumentService>();
        services.AddTransient<ILanguageCommandHandler, LanguageCommandHandler>();
        services.AddTransient<ILanguageQueryHandler, LanguageQueryHandler>();
        services.AddTransient<IManufacturerCommandHandler, ManufacturerCommandHandler>();
        services.AddTransient<IManufacturerQueryHandler, ManufacturerQueryHandler>();
        services.AddTransient<IManufacturerStatusQueryHandler, ManufacturerStatusQueryHandler>();
        services.AddTransient<IModelManagementService, ModelManagementService>();
        services.AddTransient<IMyMMCommandHandler, MyMMCommandHandler>();
        services.AddTransient<IMyMMQueryHandler, MyMMQueryHandler>();
        services.AddTransient<IMyMMStatusCommandHandler, MyMMStatusCommandHandler>();
        services.AddTransient<IMyMMStatusQueryHandler, MyMMStatusQueryHandler>();
        services.AddTransient<IOllamaService, OllamaService>();
        services.AddTransient<IRagAiService, RagAiService>();
        services.AddTransient<IWidgetCommandHandler, WidgetCommandHandler>();
        services.AddTransient<IWidgetQueryHandler, WidgetQueryHandler>();
        services.AddTransient<IWidgetStatusQueryHandler, WidgetStatusQueryHandler>();
        services.AddSingleton<McpSqlExecutor>();
        services.AddTransient<IMcpService, McpService>();
    }
}
