

using System.ComponentModel.DataAnnotations;

namespace ManufacturerManager.DataAccess
{
    internal class ColourModelMetadata
    {
        [StringLength(20, ErrorMessage = "{0} cannot be more than {1} characters")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
    }
}
