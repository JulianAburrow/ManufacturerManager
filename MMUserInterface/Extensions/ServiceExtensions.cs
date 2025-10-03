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
        services.AddTransient<ICategoryQueryHandler, CategoryQueryHandler>();
        services.AddScoped<IChatService, ChatService>();
        services.AddTransient<IColourCommandHandler, ColourCommandHandler>();
        services.AddTransient<IColourQueryHandler, ColourQueryHandler>();
        services.AddTransient<IColourJustificationCommandHandler, ColourJustificationCommandHandler>();
        services.AddTransient<IColourJustificationQueryHandler, ColourJustificationQueryHandler>();
        services.AddTransient<ICrudWithErrorHandlingHelper, CrudWithErrorHandlingHelper>();
        services.AddTransient<IErrorCommandHandler, ErrorCommandHandler>();
        services.AddTransient<IErrorQueryHandler, ErrorQueryHandler>();
        services.AddTransient<IHelpDocumentService, HelpDocumentService>();
        services.AddTransient<IManufacturerCommandHandler, ManufacturerCommandHandler>();
        services.AddTransient<IManufacturerQueryHandler, ManufacturerQueryHandler>();
        services.AddTransient<IManufacturerStatusQueryHandler, ManufacturerStatusQueryHandler>();
        services.AddTransient<IWidgetCommandHandler, WidgetCommandHandler>();
        services.AddTransient<IWidgetQueryHandler, WidgetQueryHandler>();
        services.AddTransient<IWidgetStatusQueryHandler, WidgetStatusQueryHandler>();
    }
}
