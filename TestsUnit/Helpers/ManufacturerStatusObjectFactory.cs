namespace TestsUnit.Helpers;

public static class ManufacturerStatusObjectFactory
{
    public static List<ManufacturerStatusModel> GetTestManufacturerStatuses()
    {
        return
        [
            new ManufacturerStatusModel
            {
                StatusName = PublicEnums.ManufacturerStatusEnum.Active.ToString(),
            },
            new ManufacturerStatusModel
            {
                StatusName = PublicEnums.ManufacturerStatusEnum.Inactive.ToString(),
            },
        ];
    }
}