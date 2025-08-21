namespace TestsPlaywright.Helpers;

public static class ColourHelper
{
    public static int AddColour(ManufacturerManagerContext context)
    {
        var newColour = new ColourModel
        {
            Name = "Test Colour 123546",
        };
        context.Colours.Add(newColour);
        context.SaveChanges();
        return newColour.ColourId;
    }

    public static void RemoveColour(int colourId, ManufacturerManagerContext context)
    {
        var colour = context.Colours.Find(colourId);
        if (colour != null)
        {
            context.Colours.Remove(colour);
            context.SaveChanges();
        }
    }
}
