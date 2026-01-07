namespace TestsUnit.Helpers;

public static class MyMMStatusObjectFactory
{
    public static List<MyMMStatusModel> GetTestMyMMStatuses()
    {
        return
            [
            new MyMMStatusModel
            {
                StatusName = PublicEnums.MyMMStatusEnum.Active.ToString(),
            },
            new MyMMStatusModel
            {
                StatusName = PublicEnums.MyMMStatusEnum.Inactive.ToString(),
            },
            new MyMMStatusModel
            {
                StatusName = PublicEnums.MyMMStatusEnum.Pending.ToString(),
            },
        ];
    }
}
