namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IColourCommandHandler
{
    Task CreateColourAsync(ColourModel colour, bool callSaveChanges);

    Task UpdateColourAsync(ColourModel colour, bool callSaveChanges);

    Task DeleteColourAsync(int colourId, bool callSaveChanges);

    Task SaveChangesAsync();
}
