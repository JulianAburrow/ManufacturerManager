namespace MMDataAccess.Handlers.CommandHandlers;

public class ColourJustificationCommandHandler(ManufacturerManagerContext context) : IColourJustificationCommandHandler
{
    public async Task CreateColourJustificationAsync(ColourJustificationModel colourJustification)
    {
        context.ColourJustifications.Add(colourJustification);
        await context.SaveChangesAsync();
    }

    public async Task DeleteColourJustificationAsync(int colourJustificationId)
    {
        var colourJustificationToDelete = context.ColourJustifications.SingleOrDefault(c => c.ColourJustificationId == colourJustificationId);
        if (colourJustificationToDelete is null)
            return;
        context.ColourJustifications.Remove(colourJustificationToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateColourJustificationAsync(ColourJustificationModel colourJustification)
    {
        var colourJustificationToUpdate = context.ColourJustifications.SingleOrDefault(c => c.ColourJustificationId == colourJustification.ColourJustificationId);
        if (colourJustificationToUpdate is null)
            return;
        colourJustificationToUpdate.Justification = colourJustification.Justification;
        await context.SaveChangesAsync();
    }
}
