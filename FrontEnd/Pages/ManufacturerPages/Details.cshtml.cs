using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.ManufacturerPages
{
    public class DetailsModel : ManufacturerManagerPageModel
    {
        public DetailsModel(ManufacturerManagerContext context)
            : base (context)
        {
        }

        public Manufacturer Manufacturer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Manufacturer = await _context.Manufacturer
                .Include(m => m.ManufacturerStatus)
                .Include(m => m.StaffMemberCreated)
                .Include(m => m.StaffMemberUpdated).FirstOrDefaultAsync(m => m.ManufacturerId == id);

            if (Manufacturer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
