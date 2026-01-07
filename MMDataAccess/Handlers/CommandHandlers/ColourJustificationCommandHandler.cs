namespace MMDataAccess.Handlers.CommandHandlers;

public class ColourJustificationCommandHandler(ManufacturerManagerContext context) : IColourJustificationCommandHandler
{
    public async Task CreateColourJustificationAsync(ColourJustificationModel colourJustification, bool callSaveChanges)
    {
        context.ColourJustifications.Add(colourJustification);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteColourJustificationAsync(int colourJustificationId, bool callSaveChanges)
    {
        var colourJustificationToDelete = context.ColourJustifications.SingleOrDefault(c => c.ColourJustificationId == colourJustificationId);
        if (colourJustificationToDelete is null)
            return;
        context.ColourJustifications.Remove(colourJustificationToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task UpdateColourJustificationAsync(ColourJustificationModel colourJustification, bool callSaveChanges)
    {
        var colourJustificationToUpdate = context.ColourJustifications.SingleOrDefault(c => c.ColourJustificationId == colourJustification.ColourJustificationId);
        if (colourJustificationToUpdate is null)
            return;
        colourJustificationToUpdate.Justification = colourJustification.Justification;
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
