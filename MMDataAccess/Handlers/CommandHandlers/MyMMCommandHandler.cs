
namespace MMDataAccess.Handlers.CommandHandlers;

public class MyMMCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IMyMMCommandHandler
{
    public async Task CreateMyMMAsync(MyMMModel myMM)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.MyMMs.Add(myMM);
        await context.SaveChangesAsync();
    }

    public async Task DeleteMyMMAsync(int myMMid)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var myMMToDelete = context.MyMMs.SingleOrDefault(m => m.MyMMId == myMMid);
        if (myMMToDelete is null)
            return;
        context.MyMMs.Remove(myMMToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateMyMMAsync(MyMMModel myMM)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var myMMToUpdate = context.MyMMs.SingleOrDefault(m => m.MyMMId == myMM.MyMMId);
        if (myMMToUpdate is null)
            return;

        myMMToUpdate.Title = myMM.Title;
        myMMToUpdate.URL = myMM.URL;
        myMMToUpdate.Notes = myMM.Notes;
        myMMToUpdate.ActionDate = myMM.ActionDate;
        myMMToUpdate.IsExternal = myMM.IsExternal;
        myMMToUpdate.StatusId = myMM.StatusId;
        await context.SaveChangesAsync();
    }
}
