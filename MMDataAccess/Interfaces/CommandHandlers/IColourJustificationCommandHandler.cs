namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IColourJustificationCommandHandler
{
    Task CreateColourJustificationAsync(ColourJustificationModel colourJustification, bool callSaveChanges);

    Task UpdateColourJustificationAsync(ColourJustificationModel colourJustification, bool callSaveChanges);

    Task DeleteColourJustificationAsync(int colourJustificationId, bool callSaveChanges);

    Task SaveChangesAsync();
}
