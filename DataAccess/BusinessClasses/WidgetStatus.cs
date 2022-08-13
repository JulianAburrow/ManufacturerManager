
using Microsoft.AspNetCore.Mvc;

namespace ManufacturerManager.DataAccess
{
    [ModelMetadataType(typeof(WidgetStatusModelMetadata))]
    public partial class WidgetStatus
    {
        public const int All = 0;
        public const int Active = 1;
        public const int Inactive = 2;
    }
}
