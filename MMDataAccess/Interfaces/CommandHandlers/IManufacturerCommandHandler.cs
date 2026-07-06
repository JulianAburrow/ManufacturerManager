namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IManufacturerCommandHandler
{
    Task CreateManufacturerAsync(ManufacturerModel manufacturer);

    Task UpdateManufacturerAsync(ManufacturerModel manufacturer);

    Task DeleteManufacturerAsync(int manufacturerId);
}
