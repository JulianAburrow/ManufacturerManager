using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using ManufacturerManager.FrontEnd.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.WidgetPages
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

        public Widget Widget { get; set; }

        public SelectList Colours { get; set; }

        public SelectList ColourJustifications { get; set; }

        public SelectList Manufacturers { get; set; }

        public SelectList WidgetStatuses { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Widget = await _context.Widget
                .FirstOrDefaultAsync(m => m.WidgetId == id);

            if (Widget == null)
            {
                return NotFound();
            }

            if (Widget.ColourId == null)
            {
                Widget.ColourId = ConfigValues.PleaseSelect.Int;
            }
            if (Widget.ColourJustificationId == null)
            {
                Widget.ColourJustificationId = ConfigValues.PleaseSelect.Int;
            }

            await DoPopulationsAsync();
            return Page();
        }

        #endregion

        #region Post

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await DoPopulationsAsync();
                return Page();
            }

            var widget = await _context.Widget
                .FindAsync(id);

            if (widget == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                widget,
                nameof(Widget),
                w => w.WidgetId,
                w => w.Name,
                w => w.ManufacturerId,
                w => w.ColourId,
                w => w.ColourJustificationId,
                w => w.StatusId))
            {
                if (widget.ColourId == ConfigValues.PleaseSelect.Int)
                {
                    widget.ColourId = null;
                }
                if (widget.ColourId != Colour.Pink)
                {
                    widget.ColourJustificationId = null;
                }

                await _context.SaveChangesAsync();
                return RedirectToPage(ConfigValues.Pages.WidgetIndex);
            }

            await DoPopulationsAsync();
            return Page();
        }

        #endregion

        #region Private Methods

        private async Task DoPopulationsAsync()
        {
            await PopulateColoursAsync();
            await PopulateColourJustificationsAsync();
            await PopulateManufacturersAsync();
            await PopulateStatusesAsync();
        }

        private async Task PopulateColoursAsync()
        {
            var colours = await _context.Colour
                            .OrderBy(c => c.Name)
                            .ToListAsync();
            colours.Insert(0, new Colour
            {
                ColourId = ConfigValues.PleaseSelect.Int,
                Name = ConfigValues.PleaseSelect.Text
            });

            Colours = new SelectList(
                        colours,
                        nameof(Colour.ColourId),
                        nameof(Colour.Name));
        }

        private async Task PopulateColourJustificationsAsync()
        {
            var colourJustifications = await _context.ColourJustification
                            .OrderBy(c => c.Justification)
                            .ToListAsync();
            colourJustifications.Insert(0, new ColourJustification
            {
                ColourJustificationId = ConfigValues.PleaseSelect.Int,
                Justification = ConfigValues.PleaseSelect.Text
            });

            ColourJustifications = new SelectList(
                        colourJustifications,
                        nameof(ColourJustification.ColourJustificationId),
                        nameof(ColourJustification.Justification));
        }

        private async Task PopulateManufacturersAsync()
        {
            Manufacturers = new SelectList(
                await _context.Manufacturer
                            .OrderBy(m => m.Name)
                            .ToListAsync(),
                        nameof(Manufacturer.ManufacturerId),
                        nameof(Manufacturer.Name));
        }

        private async Task PopulateStatusesAsync()
        {
            WidgetStatuses = new SelectList(
                await _context.WidgetStatus
                            .ToListAsync(),
                        nameof(WidgetStatus.StatusId),
                        nameof(WidgetStatus.StatusName));
        }

        #endregion
    }
}
