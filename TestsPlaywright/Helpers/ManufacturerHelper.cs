namespace TestsPlaywright.Helpers;

public static class ManufacturerHelper
{
    public static int AddManufacturer(ManufacturerManagerContext context)
    {
        var newManufacturer = new ManufacturerModel
        {
            Name = $"Test Manufacturer {Guid.NewGuid()}",
            StatusId = (int)Enums.StatusEnum.Active,
        };
        context.Manufacturers.Add(newManufacturer);
        context.SaveChanges();
        return newManufacturer.ManufacturerId;
    }

    public static void RemoveManufacturer(int manufacturerId, ManufacturerManagerContext context)
    {
        var manufacturer = context.Manufacturers.Find(manufacturerId);
        if (manufacturer != null)
        {
            context.Manufacturers.Remove(manufacturer);
            context.SaveChanges();
        }
    }
}
