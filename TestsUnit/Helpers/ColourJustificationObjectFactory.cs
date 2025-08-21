namespace TestsUnit.Helpers;

public static class ColourJustificationObjectFactory
{
    public static List<ColourJustificationModel> GetTestColourJustifications()
    {
        return
            [
            new ColourJustificationModel
            {
                Justification = "Justification1",
            },
            new ColourJustificationModel
            {
                Justification = "Justification2",
            },
            new ColourJustificationModel
            {
                Justification = "Justification3",
            },
            new ColourJustificationModel
            {
                Justification = "Justification4",
            }
        ];
    }
}
