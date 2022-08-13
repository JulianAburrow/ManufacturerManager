using System.ComponentModel.DataAnnotations;

namespace ManufacturerManager.DataAccess
{
    internal class StaffMemberModelMetadata
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0] cannot be be more than {1} characters")]
        [Display(Name= "Last Name")]
        public string LastName { get; set; }
    }
}
