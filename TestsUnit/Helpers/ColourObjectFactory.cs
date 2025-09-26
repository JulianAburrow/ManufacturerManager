namespace TestsUnit.Helpers;

public static class ColourObjectFactory
{
    public static List<ColourModel> GetTestColours()
    {
        return
        [
            new ColourModel
            {
                Name = "Colour1",
            },
            new ColourModel
            {
                Name = "Colour2",
            },
            new ColourModel
            {
                Name = "Colour3",
            },
            new ColourModel
            {
                Name = "Colour4",
            }
        ];
    }
}
