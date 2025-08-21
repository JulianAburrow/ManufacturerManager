namespace TestsUnit.Helpers;

public static class WidgetStatusObjectFactory
{
    public static List<WidgetStatusModel> GetTestWidgetStatuses()
    {
        return
            [
            new WidgetStatusModel
            {
                StatusName = PublicEnums.WidgetStatusEnum.Active.ToString(),
            },
            new WidgetStatusModel
            {
                StatusName = PublicEnums.WidgetStatusEnum.Inactive.ToString(),
            },
        ];
    }
}