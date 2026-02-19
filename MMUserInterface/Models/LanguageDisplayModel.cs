namespace MMUserInterface.Models;

public class LanguageDisplayModel
{
    public int LanguageId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(100, ErrorMessage = "{0} cannot be more than {2} characters")]
    public string Name { get; set; } = default!;
}
