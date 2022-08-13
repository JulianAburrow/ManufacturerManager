using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using ManufacturerManager.FrontEnd.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.ManufacturerPages
{
    public class EditModel : ManufacturerManagerPageModel
    {
        #region Constructor

        public EditModel(ManufacturerManagerContext context)
            : base (context)
        {
        }

        #endregion

        #region Properties

        public Manufacturer Manufacturer { get; set; }

        public SelectList ManufacturerStatuses { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Manufacturer = await _context.Manufacturer
                .Include(m => m.ManufacturerStatus)
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);

            if (Manufacturer == null)
            {
                return NotFound();
            }
            PopulateStatuses();
            return Page();
        }

        #endregion

        #region Post

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                PopulateStatuses();
                return Page();
            }

            if (!ModelState.IsValid)
            {
                PopulateStatuses();
                return Page();
            }

            var manufacturer = await _context.Manufacturer
                .Include(m => m.Widget)
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                manufacturer,
                nameof(Manufacturer),
                m => m.ManufacturerId,
                m => m.Name,
                m => m.StatusId))
            {
                if (manufacturer.StatusId == ManufacturerStatus.Inactive &&
                    manufacturer.Widget.Count > 0)
                {
                    foreach (var widget in manufacturer.Widget)
                    {
                        widget.StatusId = WidgetStatus.Inactive;
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToPage(ConfigValues.Pages.ManufacturerIndex);
            }
            PopulateStatuses();
            return Page();
        }

        #endregion

        #region Private Methods

        private void PopulateStatuses()
        {
            ManufacturerStatuses = new SelectList(
                _context.ManufacturerStatus,
                nameof(ManufacturerStatus.StatusId),
                nameof(ManufacturerStatus.StatusName));
        }

        #endregion
    }
}
