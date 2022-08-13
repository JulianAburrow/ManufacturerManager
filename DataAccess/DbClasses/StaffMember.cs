using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerManager.DataAccess
{
    [Table("StaffMember")]
    public partial class StaffMember
    {
        [Key]
        public int StaffMemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Manufacturer> ManufacturerCreated { get; set; }

        public ICollection<Manufacturer> ManufacturerUpdated { get; set; }

        public ICollection<Widget> WidgetCreated { get; set; }

        public ICollection<Widget> WidgetUpdated { get; set; }
    }
}
