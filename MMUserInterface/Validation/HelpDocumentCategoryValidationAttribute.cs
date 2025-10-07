namespace MMUserInterface.Validation;

public class HelpDocumentCategoryValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string category || string.IsNullOrWhiteSpace(category) || category == SharedValues.PleaseSelectText)
        {
            return new ValidationResult("Category is required");
        }
        return ValidationResult.Success;
    }
}
