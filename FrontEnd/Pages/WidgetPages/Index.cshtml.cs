using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.WidgetPages
{
    public class IndexModel : ManufacturerManagerPageModel
    {
        public IndexModel(ManufacturerManagerContext context)
            : base (context)
        {
        }

        public IList<Widget> Widget { get;set; }

        public async Task OnGetAsync()
        {
            Widget = await _context.Widget
                .Include(w => w.Manufacturer)
                .Include(w => w.WidgetStatus)
                .ToListAsync();
        }
    }
}
