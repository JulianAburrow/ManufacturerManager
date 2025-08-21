namespace TestsUnit.Helpers;

public static class ColourObjectFactory
{
    public static List<ColourModel> GetTestColours()
    {
        return
        [
            new ColourModel
            {
                Name = "Red",
            },
            new ColourModel
            {
                Name = "Blue",
            },
            new ColourModel
            {
                Name = "Green",
            },
            new ColourModel
            {
                Name = "Yellow",
            }
        ];
    }
}
