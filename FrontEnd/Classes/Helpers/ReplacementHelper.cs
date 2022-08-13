namespace ManufacturerManager.FrontEnd.Classes.Helpers
{
    public class ReplacementHelper
    {
        public string ShowCorrectRecordText(int recordCount)
        {
            // Just to be really pedantic so that we don't get
            // "1 records found" above our tables
            return recordCount == 1 ? "" : "s";
        }
    }
}
