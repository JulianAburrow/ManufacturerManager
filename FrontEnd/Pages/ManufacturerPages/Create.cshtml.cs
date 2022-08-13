using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using ManufacturerManager.FrontEnd.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.ManufacturerPages
{
    public class CreateModel : ManufacturerManagerPageModel
    {
        #region Constructor

        public CreateModel(ManufacturerManagerContext context)
            : base (context)
        {
        }

        #endregion

        #region Get

        public IActionResult OnGet()
        {
            PopulateStatuses();
            return Page();
        }

        #endregion

        #region Properties

        public Manufacturer Manufacturer { get; set; }

        public SelectList ManufacturerStatuses { get; set; }

        #endregion

        #region Post

        public async Task<IActionResult> OnPostAsync()
        {
            var newManufacturer = new Manufacturer(_context);

            if (await TryUpdateModelAsync(
                newManufacturer,
                nameof(Manufacturer),
                m => m.ManufacturerId,
                m => m.Name,
                m => m.StatusId))
            {
                _context.Manufacturer.Add(newManufacturer);
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
