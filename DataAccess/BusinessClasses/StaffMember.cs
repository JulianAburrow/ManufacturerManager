using Microsoft.AspNetCore.Mvc;

namespace ManufacturerManager.DataAccess
{
    [ModelMetadataType(typeof(StaffMemberModelMetadata))]
    public partial class StaffMember
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}
