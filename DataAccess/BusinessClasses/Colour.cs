

using Microsoft.AspNetCore.Mvc;

namespace ManufacturerManager.DataAccess
{
    [ModelMetadataType(typeof(ColourModelMetadata))]
    public partial class Colour
    {
        public const int Red = 1;
        public const int Green = 2;
        public const int Blue = 3;
        public const int Pink = 4;
    }
}
