namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IManufacturerQueryHandler
{
    Task<ManufacturerModel> GetManufacturerAsync(int manufacturerId);

    Task<List<ManufacturerModel>> GetManufacturersAsync();

    Task<int> GetManufacturerStatusByManufacturerId(int manufacturerId);
}
