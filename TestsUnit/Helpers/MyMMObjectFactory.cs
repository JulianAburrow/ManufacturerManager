namespace TestsUnit.Helpers;

public static class MyMMObjectFactory
{
    public static List<MyMMModel> GetTestMyMMs()
    {
        var myMMStatusActive = new MyMMStatusModel
        {
            StatusId = (int)PublicEnums.MyMMStatusEnum.Active,
            StatusName = PublicEnums.MyMMStatusEnum.Active.ToString()
        };
        var myMMStatusInactive = new MyMMStatusModel
        {
            StatusId = (int)PublicEnums.MyMMStatusEnum.Inactive,
            StatusName = PublicEnums.MyMMStatusEnum.Inactive.ToString()
        };
        var myMMStatusPending = new MyMMStatusModel
        {
            StatusId = (int)PublicEnums.MyMMStatusEnum.Pending,
            StatusName = PublicEnums.MyMMStatusEnum.Pending.ToString()
        };
        return
        [
            new MyMMModel
            {
                Title = "MyMM1",
                URL = "http://mymm1.example.com",
                Notes = "Test notes for MyMM1",
                ActionDate = DateTime.Now,
                IsExternal = false,
                StatusId = (int)PublicEnums.MyMMStatusEnum.Active,
                Status = myMMStatusActive,
            },
            new MyMMModel
            {
                Title = "MyMM2",
                URL = "http://mymm2.example.com",
                Notes = "Test notes for MyMM2",
                ActionDate = DateTime.Now,
                IsExternal = true,
                StatusId = (int)PublicEnums.MyMMStatusEnum.Inactive,
                Status = myMMStatusInactive,
            },
            new MyMMModel
            {
                Title = "MyMM3",
                URL = "http://mymm3.example.com",
                Notes = "Test notes for MyMM3",
                ActionDate = DateTime.Now,
                IsExternal = true,
                StatusId = (int)PublicEnums.MyMMStatusEnum.Pending,
                Status = myMMStatusPending,
            }
        ];
    }
}
