namespace MMDataAccess.Models;

public class MyMMModel
{
    public int MyMMId { get; set; }

    public string Title { get; set; } = default!;

    public string? URL { get; set; }

    public string? Notes { get; set; }

    public DateOnly? ActionDate { get; set; }

    public bool IsExternal { get; set; }

    public int StatusId { get; set; }

    public MyMMStatusModel Status { get; set; } = null!;
}
