namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IWidgetStatusQueryHandler
{
    Task<List<WidgetStatusModel>> GetWidgetStatusesAsync();
}
