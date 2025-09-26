namespace MMUserInterface.Models;

public class CategoryDisplayModel
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(25, ErrorMessage = "{0} cannot be more than {1} characters")]
    public string Name { get; set; } = default!;
}
