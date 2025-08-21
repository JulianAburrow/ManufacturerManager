namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IColourQueryHandler
{
    Task<ColourModel> GetColourAsync(int colourId);

    Task<List<ColourModel>> GetColoursAsync();
}
