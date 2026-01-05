
namespace MMDataAccess.Handlers.CommandHandlers;

public class MyMMCommandHandler(ManufacturerManagerContext context) : IMyMMCommandHandler
{
    public async Task CreateMyMMAsync(MyMMModel myMM, bool callSaveChanges)
    {
        context.MyMMs.Add(myMM);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteMyMMAsync(int myMMid, bool callSaveChanges)
    {
        var myMMToDelete = context.MyMMs.SingleOrDefault(m => m.MyMMId == myMMid);
        if (myMMToDelete is null)
            return;
        context.MyMMs.Remove(myMMToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();

    public async Task UpdateMyMMAsync(MyMMModel myMM, bool callSaveChanges)
    {
        var myMMToUpdate = context.MyMMs.SingleOrDefault(m => m.MyMMId == myMM.MyMMId);
        if (myMMToUpdate is null)
            return;

        myMMToUpdate.Title = myMM.Title;
        myMMToUpdate.URL = myMM.URL;
        myMMToUpdate.Notes = myMM.Notes;
        myMMToUpdate.ActionDate = myMM.ActionDate;
        myMMToUpdate.IsExternal = myMM.IsExternal;
        if (callSaveChanges)
            await SaveChangesAsync();
    }
}
