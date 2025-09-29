namespace MMDataAccess.Handlers.CommandHandlers;

public class ColourCommandHandler(ManufacturerManagerContext context) : IColourCommandHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task CreateColourAsync(ColourModel colour, bool callSaveChanges)
    {
        _context.Colours.Add(colour);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteColourAsync(int colourId, bool callSaveChanges)
    {
        var colourToDelete = _context.Colours.SingleOrDefault(c => c.ColourId == colourId);
        if (colourToDelete == null)
            return;
        _context.Colours.Remove(colourToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task UpdateColourAsync(ColourModel colour, bool callSaveChanges)
    {
        var colourToUpdate = _context.Colours.SingleOrDefault(c => c.ColourId == colour.ColourId);
        if (colourToUpdate == null)
            return;

        colourToUpdate.Name = colour.Name;
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
