namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IManufacturerCommandHandler
{
    Task CreateManufacturerAsync(ManufacturerModel manufacturer, bool callSaveChanges);

    Task UpdateManufacturerAsync(ManufacturerModel manufacturer, bool callSaveChanges);

    Task DeleteManufacturerAsync(int manufacturerId, bool callSaveChanges);

    Task SaveChangesAsync();
}
