namespace TestsUnit.Helpers;

public static class ManufacturerObjectFactory
{
    public static List<ManufacturerModel> GetTestManufacturers()
    {
        return
        [
            new ManufacturerModel
            {
                Name = "Manufacturer1",
                StatusId = (int)PublicEnums.ManufacturerStatusEnum.Active,
            },
            new ManufacturerModel
            {
                Name = "Manufacturer2",
                StatusId = (int)PublicEnums.ManufacturerStatusEnum.Active,
            },
            new ManufacturerModel
            {
                Name = "Manufacturer3",
                StatusId = (int)PublicEnums.ManufacturerStatusEnum.Inactive,
            },
            new ManufacturerModel
            {
                Name = "Manufacturer4",
                StatusId = (int)PublicEnums.ManufacturerStatusEnum.Inactive,
            }
        ];
    }
}
