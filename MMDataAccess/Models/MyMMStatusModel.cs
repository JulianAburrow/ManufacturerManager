namespace MMDataAccess.Models;

public class MyMMStatusModel
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = default!;

    public ICollection<MyMMModel> MyMMs { get; set; } = null!;
}
