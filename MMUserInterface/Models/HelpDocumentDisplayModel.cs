using MMUserInterface.Validation;

namespace MMUserInterface.Models;

public class HelpDocumentDisplayModel
{
    [HelpDocumentCategoryValidation]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Help File")]
    public IBrowserFile HelpFile { get; set; } = null!;
}
