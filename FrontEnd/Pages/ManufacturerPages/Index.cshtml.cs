using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using ManufacturerManager.FrontEnd.Classes.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.ManufacturerPages
{
    public class IndexModel : ManufacturerManagerPageModel
    {
        public readonly ReplacementHelper _replacementHelper;

        public IndexModel(ManufacturerManagerContext context, ReplacementHelper replacementHelper)
            : base (context)
        {
            _replacementHelper = replacementHelper;
        }

        public IList<Manufacturer> Manufacturer { get; set; }

        public async Task OnGetAsync()
        {
            Manufacturer = await _context.Manufacturer
                .Include(m => m.ManufacturerStatus)
                .ToListAsync();
        }
    }
}

