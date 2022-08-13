using ManufacturerManager.DataAccess;
using ManufacturerManager.FrontEnd.Classes;
using ManufacturerManager.FrontEnd.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace ManufacturerManager.FrontEnd.WidgetPages
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
            DoPopulations();
            return Page();
        }

        #endregion

        #region Properties

        public Widget Widget { get; set; }

        public SelectList Colours { get; set; }

        public SelectList ColourJustifications { get; set; }

        public SelectList Manufacturers { get; set; }

        public SelectList WidgetStatuses { get; set; }

        #endregion

        #region Post

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DoPopulations();
                return Page();
            }

            var newWidget = new Widget(_context);

            if (await TryUpdateModelAsync(
                newWidget,
                nameof(Widget),
                w => w.WidgetId,
                w => w.Name,
                w => w.ManufacturerId,
                w => w.ColourId,
                w => w.ColourJustificationId,
                w => w.StatusId))
            {
                if (newWidget.ColourId == ConfigValues.PleaseSelect.Int)
                {
                    newWidget.ColourId = null;
                }
                if (newWidget.ColourId != Colour.Pink)
                {
                    newWidget.ColourJustificationId = null;
                }

                _context.Widget.Add(newWidget);
                await _context.SaveChangesAsync();
                return RedirectToPage(ConfigValues.Pages.WidgetIndex);
            }
            DoPopulations();
            return Page();
        }

        #endregion

        #region Private Methods

        private void DoPopulations()
        {
            PopulateColours();
            PopulateColourJustifications();
            PopulateManufacturers();
            PopulateStatuses();
        }

        private void PopulateColours()
        {
            var colours = _context.Colour
                .OrderBy(c => c.Name)
                .ToList();
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

        private void PopulateColourJustifications()
        {
            var colourJustifications = _context.ColourJustification
                .OrderBy(c => c.Justification)
                .ToList();
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

        private void PopulateManufacturers()
        {
            var manufacturers = _context.Manufacturer
                .OrderBy(m => m.Name)
                .ToList();
            manufacturers.Insert(0, new Manufacturer(_context)
            {
                ManufacturerId = ConfigValues.PleaseSelect.Int,
                Name = ConfigValues.PleaseSelect.Text
            });
            Manufacturers = new SelectList(
                manufacturers,
                nameof(Manufacturer.ManufacturerId),
                nameof(Manufacturer.Name));
        }

        private void PopulateStatuses()
        {
            WidgetStatuses = new SelectList(
                _context.WidgetStatus,
                nameof(WidgetStatus.StatusId),
                nameof(WidgetStatus.StatusName));
        }

        #endregion
    }
}
