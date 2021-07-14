namespace FidelityHub.Database.Entities.UsrSchema
{
    public class VendorRelationship
    {
        public string UserId { get; set; }
        public int VendorId { get; set; }
        public int VendorUnitId { get; set; }
        public bool IsVendorAdmin { get; set; }
        public bool IsVendorUnitAdmin { get; set; }
    }
}
