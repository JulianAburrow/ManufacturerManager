namespace MMDataAccess.Handlers.CommandHandlers;

public class MyMMStatusCommandHandler(ManufacturerManagerContext context) : IMyMMStatusCommandHandler
{
    public async Task CreateMyMMStatusAsync(MyMMStatusModel myMMStatusModel)
    {
        context.MyMMStatuses.Add(myMMStatusModel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteMyMMStatusAsync(int myMMStatusId)
    {
        var myMMStatusToDelete = context.MyMMStatuses.SingleOrDefault(m => m.StatusId == myMMStatusId);
        if (myMMStatusToDelete is null)
            return;
        context.MyMMStatuses.Remove(myMMStatusToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateMyMMStatusAsync(MyMMStatusModel myMMStatusModel)
    {
        var myMMStatusToUpdate = context.MyMMStatuses.SingleOrDefault(m => m.StatusId == myMMStatusModel.StatusId);
        if (myMMStatusToUpdate is null)
            return;

        myMMStatusToUpdate.StatusName = myMMStatusModel.StatusName;
        await context.SaveChangesAsync();
    }
}
