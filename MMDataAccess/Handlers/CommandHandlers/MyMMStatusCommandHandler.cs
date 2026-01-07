namespace MMDataAccess.Handlers.CommandHandlers;

public class MyMMStatusCommandHandler(ManufacturerManagerContext context) : IMyMMStatusCommandHandler
{
    public async Task CreateMyMMStatusAsync(MyMMStatusModel myMMStatusModel, bool callSaveChanges)
    {
        context.MyMMStatuses.Add(myMMStatusModel);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteMyMMStatusAsync(int myMMStatusId, bool callSaveChanges)
    {
        var myMMStatusToDelete = context.MyMMStatuses.SingleOrDefault(m => m.StatusId == myMMStatusId);
        if (myMMStatusToDelete is null)
            return;
        context.MyMMStatuses.Remove(myMMStatusToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();

    public async Task UpdateMyMMStatusAsync(MyMMStatusModel myMMStatusModel, bool callSaveChanges)
    {
        var myMMStatusToUpdate = context.MyMMStatuses.SingleOrDefault(m => m.StatusId == myMMStatusModel.StatusId);
        if (myMMStatusToUpdate is null)
            return;

        myMMStatusToUpdate.StatusName = myMMStatusModel.StatusName;
        if (callSaveChanges)
            await SaveChangesAsync();
    }
}
