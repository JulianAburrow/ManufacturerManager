
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerManager.DataAccess
{
    [Table("Colour")]
    public partial class Colour
    {
        [Key]
        public int ColourId { get; set; }
        public string Name { get; set; }
        public ICollection<Widget> Widget { get; set; }
    }
}
