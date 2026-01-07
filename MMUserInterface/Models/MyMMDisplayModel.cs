namespace MMUserInterface.Models;

public class MyMMDisplayModel
{
    public int MyMMId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string Title { get; set; } = default!;

    [StringLength(200, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string? URL { get; set; }

    [StringLength(500, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string? Notes { get; set; }

    public DateTime? ActionDate { get; set; }

    public bool IsExternal { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
    [Display(Name = "Status")]
    public int StatusId { get; set; }

    public MyMMStatusModel Status { get; set; } = null!;
}
