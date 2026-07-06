namespace MMDataAccess.Handlers.CommandHandlers;

public class ColourCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IColourCommandHandler
{
    public async Task CreateColourAsync(ColourModel colour)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.Colours.Add(colour);
        await context.SaveChangesAsync();
    }

    public async Task DeleteColourAsync(int colourId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var colourToDelete = await context.Colours.SingleOrDefaultAsync(c => c.ColourId == colourId);
        if (colourToDelete is null)
            return;
        context.Colours.Remove(colourToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateColourAsync(ColourModel colour)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var colourToUpdate = await context.Colours.SingleOrDefaultAsync(c => c.ColourId == colour.ColourId);
        if (colourToUpdate is null)
            return;

        colourToUpdate.Name = colour.Name;
        await context.SaveChangesAsync();
    }
}
