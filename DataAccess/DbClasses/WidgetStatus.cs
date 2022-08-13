
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerManager.DataAccess
{
    [Table("WidgetStatus")]
    public partial class WidgetStatus
    {
        [Key]
        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public ICollection<Widget> Widget { get; set; }
    }
}
