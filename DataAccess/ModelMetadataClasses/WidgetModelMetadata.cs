using System;
using System.ComponentModel.DataAnnotations;

namespace ManufacturerManager.DataAccess
{
    internal class WidgetModelMetadata
    {
        [StringLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Colour")]
        public int ColourId { get; set; }

        [Display(Name = "Colour")]
        public Colour Widget { get; set; }

        [Display(Name = "Colour Justification")]
        public int ColourJustificationId { get; set; }

        [Display(Name = "Colour Justification")]
        public ColourJustification ColourJustification { get; set; }

        [Display(Name = "Status")]
        public WidgetStatus WidgetStatus { get; set; }

        [Display(Name = "Created By")]
        public int CreatedById { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }

        [Display(Name = "Last Updated By")]
        public int LastUpdatedById { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        [Display(Name = "Created By")]
        public StaffMember StaffMemberCreated { get; set; }

        [Display(Name = "Last Updated By")]
        public StaffMember StaffMemberUpdated { get; set; }
    }
}
