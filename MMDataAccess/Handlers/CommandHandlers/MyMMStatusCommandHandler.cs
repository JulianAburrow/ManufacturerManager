namespace MMDataAccess.Handlers.CommandHandlers;

public class MyMMStatusCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IMyMMStatusCommandHandler
{
    public async Task CreateMyMMStatusAsync(MyMMStatusModel myMMStatusModel)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.MyMMStatuses.Add(myMMStatusModel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteMyMMStatusAsync(int myMMStatusId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var myMMStatusToDelete = context.MyMMStatuses.SingleOrDefault(m => m.StatusId == myMMStatusId);
        if (myMMStatusToDelete is null)
            return;
        context.MyMMStatuses.Remove(myMMStatusToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateMyMMStatusAsync(MyMMStatusModel myMMStatusModel)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var myMMStatusToUpdate = context.MyMMStatuses.SingleOrDefault(m => m.StatusId == myMMStatusModel.StatusId);
        if (myMMStatusToUpdate is null)
            return;

        myMMStatusToUpdate.StatusName = myMMStatusModel.StatusName;
        await context.SaveChangesAsync();
    }
}
