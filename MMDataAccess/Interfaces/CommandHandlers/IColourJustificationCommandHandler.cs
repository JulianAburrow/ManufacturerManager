namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IColourJustificationCommandHandler
{
    Task CreateColourJustificationAsync(ColourJustificationModel colourJustification);

    Task UpdateColourJustificationAsync(ColourJustificationModel colourJustification);

    Task DeleteColourJustificationAsync(int colourJustificationId);
}
