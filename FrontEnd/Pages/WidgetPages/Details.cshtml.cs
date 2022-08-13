using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.WidgetPages
{
    public class DetailsModel : ManufacturerManagerPageModel
    {
        public DetailsModel(ManufacturerManagerContext context)
            : base (context)
        {
        }

        public Widget Widget { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Widget = await _context.Widget
                .Include(w => w.Colour)
                .Include(w => w.ColourJustification)
                .Include(w => w.Manufacturer)
                .Include(w => w.StaffMemberCreated)
                .Include(w => w.StaffMemberUpdated)
                .Include(w => w.WidgetStatus).FirstOrDefaultAsync(m => m.WidgetId == id);

            if (Widget == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
