namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IColourCommandHandler
{
    Task CreateColourAsync(ColourModel colour);

    Task UpdateColourAsync(ColourModel colour);

    Task DeleteColourAsync(int colourId);
}
