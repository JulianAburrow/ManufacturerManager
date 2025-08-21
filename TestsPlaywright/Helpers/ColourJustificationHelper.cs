namespace TestsPlaywright.Helpers;

public static class ColourJustificationHelper
{
    public static int AddColourJustification(ManufacturerManagerContext context)
    {
        var newColourJustification = new ColourJustificationModel
        {
            Justification = $"Colour Justification {Guid.NewGuid()}"
        };
        context.ColourJustifications.Add(newColourJustification);
        context.SaveChanges();
        return newColourJustification.ColourJustificationId;
    }

    public static void RemoveColourJustification(int colourJustificationId, ManufacturerManagerContext context)
    {
        var colourJustification = context.ColourJustifications.Find(colourJustificationId);
        if (colourJustification != null)
        {
            context.ColourJustifications.Remove(colourJustification);
            context.SaveChanges();
        }
    }
}
