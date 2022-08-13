using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ManufacturerManager.DataAccess
{
    [ModelMetadataType(typeof(ManufacturerModelMetadata))]
    public partial class Manufacturer : IAuditableObject, IValidatableObject
    {
        private readonly ManufacturerManagerContext _context;

        public Manufacturer(ManufacturerManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            var duplicates = _context.Manufacturer
                .Where(m =>
                    m.Name.Replace(" ", "").ToLower() == Name.Replace(" ", "").ToLower());
            if (ManufacturerId > 0) // This is an update not an insert
            {
                duplicates = duplicates.Where(m => m.ManufacturerId != ManufacturerId);
            }

            if (duplicates.Any())
            {
                results.Add(new ValidationResult(
                    "This Manufacturer already exists in the database",
                    new List<string>
                    {
                        nameof(Name)
                    }));
            }

            return results;
        }
    }
}
