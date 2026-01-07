namespace MMDataAccess.Handlers.CommandHandlers;

public class ColourCommandHandler(ManufacturerManagerContext context) : IColourCommandHandler
{
    public async Task CreateColourAsync(ColourModel colour, bool callSaveChanges)
    {
        context.Colours.Add(colour);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteColourAsync(int colourId, bool callSaveChanges)
    {
        var colourToDelete = context.Colours.SingleOrDefault(c => c.ColourId == colourId);
        if (colourToDelete is null)
            return;
        context.Colours.Remove(colourToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task UpdateColourAsync(ColourModel colour, bool callSaveChanges)
    {
        var colourToUpdate = context.Colours.SingleOrDefault(c => c.ColourId == colour.ColourId);
        if (colourToUpdate is null)
            return;

        colourToUpdate.Name = colour.Name;
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
