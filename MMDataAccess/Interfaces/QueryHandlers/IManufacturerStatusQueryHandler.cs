namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IManufacturerStatusQueryHandler
{
    Task<List<ManufacturerStatusModel>> GetManufacturerStatusesAsync();
}
