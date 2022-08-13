using Microsoft.AspNetCore.Mvc;

namespace ManufacturerManager.DataAccess
{
    [ModelMetadataType(typeof(ManufacturerStatusModelMetadata))]
    public partial class ManufacturerStatus
    {
        public const int All = 0;
        public const int Active = 1;
        public const int Inactive = 2;
    }
}
