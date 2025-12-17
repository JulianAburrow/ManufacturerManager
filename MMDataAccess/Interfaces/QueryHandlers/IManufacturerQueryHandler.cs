namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IManufacturerQueryHandler
{
    Task<ManufacturerModel> GetManufacturerAsync(int manufacturerId);

    Task<List<ManufacturerSummary>> GetManufacturersAsync();

    Task<int> GetManufacturerStatusByManufacturerId(int manufacturerId);
}
