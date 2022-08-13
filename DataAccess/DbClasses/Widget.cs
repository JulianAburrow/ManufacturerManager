

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerManager.DataAccess
{
    [Table("Widget")]
    public partial class Widget
    {
        [Key]
        public int WidgetId { get; set; }

        public string Name { get; set; }

        public int ManufacturerId { get; set; }

        public int? ColourId { get; set; }

        public int? ColourJustificationId { get; set; }

        public int StatusId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public int CreatedById { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; }

        public int LastUpdatedById { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public Colour Colour { get; set; }

        public WidgetStatus WidgetStatus { get; set; }

        public StaffMember StaffMemberCreated { get; set; }

        public StaffMember StaffMemberUpdated { get; set; }

        public ColourJustification ColourJustification { get; set; }

    }
}
