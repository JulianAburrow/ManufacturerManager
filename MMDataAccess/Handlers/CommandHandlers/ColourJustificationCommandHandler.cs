namespace MMDataAccess.Handlers.CommandHandlers;

public class ColourJustificationCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IColourJustificationCommandHandler
{
    public async Task CreateColourJustificationAsync(ColourJustificationModel colourJustification)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.ColourJustifications.Add(colourJustification);
        await context.SaveChangesAsync();
    }

    public async Task DeleteColourJustificationAsync(int colourJustificationId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var colourJustificationToDelete = await context.ColourJustifications.SingleOrDefaultAsync(c => c.ColourJustificationId == colourJustificationId);
        if (colourJustificationToDelete is null)
            return;
        context.ColourJustifications.Remove(colourJustificationToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateColourJustificationAsync(ColourJustificationModel colourJustification)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var colourJustificationToUpdate = await context.ColourJustifications.SingleOrDefaultAsync(c => c.ColourJustificationId == colourJustification.ColourJustificationId);
        if (colourJustificationToUpdate is null)
            return;
        colourJustificationToUpdate.Justification = colourJustification.Justification;
        await context.SaveChangesAsync();
    }
}
