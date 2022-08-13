
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ManufacturerManager.DataAccess
{
    [ModelMetadataType(typeof(WidgetModelMetadata))]
    public partial class Widget : IAuditableObject, IValidatableObject
    {
        private readonly ManufacturerManagerContext _context;

        public Widget(ManufacturerManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Do not allow duplicate Names for the same Manufacturer
            var duplicates = _context.Widget
                .Where(w =>
                    w.Name.Replace(" ", "").ToLower() == Name.Replace(" ", "").ToLower() &&
                    w.ManufacturerId == ManufacturerId);
            if (WidgetId > 0) // This is an update not an insert
            {
                duplicates = duplicates.Where(w => w.WidgetId != WidgetId);
            }

            if (duplicates.Any())
            {
                results.Add(new ValidationResult(
                    "This Manufacturer already has a Widget with this name",
                    new List<string>
                    {
                        "Name"
                    }));
            }

            if (ColourId == Colour.Pink &&
                (ColourJustificationId == ColourJustification.PleaseSelect ||
                ColourJustificationId == null))
            {
                results.Add(new ValidationResult("If Colour is Pink, justification is required",
                    new List<string>
                    {
                        "ColourJustificationId"
                    }));
            }

            return results;
        }
    }
}
