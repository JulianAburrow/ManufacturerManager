using Microsoft.AspNetCore.Mvc;

namespace ManufacturerManager.DataAccess
{
    [ModelMetadataType(typeof(ColourJustificationModelMetadata))]
    public partial class ColourJustification
    {
        public static int PleaseSelect = -1;
    }
}
