namespace MMUserInterface.Models;

public class MyMMDisplayModel
{
    public int MyMMid { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string Title { get; set; } = default!;

    [StringLength(200, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string? URL { get; set; }

    [StringLength(500, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string? Notes { get; set; }

    public DateOnly? ActionDate { get; set; }

    public bool IsExternal { get; set; }

    public int StatusId { get; set; }
}
