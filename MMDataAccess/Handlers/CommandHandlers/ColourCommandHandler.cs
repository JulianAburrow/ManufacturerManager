namespace MMDataAccess.Handlers.CommandHandlers;

public class ColourCommandHandler(ManufacturerManagerContext context) : IColourCommandHandler
{
    public async Task CreateColourAsync(ColourModel colour)
    {
        context.Colours.Add(colour);
        await context.SaveChangesAsync();
    }

    public async Task DeleteColourAsync(int colourId)
    {
        var colourToDelete = context.Colours.SingleOrDefault(c => c.ColourId == colourId);
        if (colourToDelete is null)
            return;
        context.Colours.Remove(colourToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateColourAsync(ColourModel colour)
    {
        var colourToUpdate = context.Colours.SingleOrDefault(c => c.ColourId == colour.ColourId);
        if (colourToUpdate is null)
            return;

        colourToUpdate.Name = colour.Name;
        await context.SaveChangesAsync();
    }
}
