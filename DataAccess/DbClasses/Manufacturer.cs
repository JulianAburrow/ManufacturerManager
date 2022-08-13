using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerManager.DataAccess
{
    [Table("Manufacturer")]
    public partial class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }

        public string Name { get; set; }

        public int StatusId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public int CreatedById { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; }

        public int LastUpdatedById { get; set; }

        public ManufacturerStatus ManufacturerStatus { get; set; }

        public ICollection<Widget> Widget { get; set; }

        public StaffMember StaffMemberCreated { get; set; }

        public StaffMember StaffMemberUpdated { get; set; }
    }
}
