using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerManager.DataAccess
{
    [Table("ColourJustification")]
    public partial class ColourJustification
    {
        [Key]
        public int ColourJustificationId { get; set; }

        public string Justification { get; set; }

        public ICollection<Widget> Widget { get; set; }
    }
}
