using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityHub.Database.Entities.UsrSchema
{
    public partial class Address
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public int StreetNumber { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Complement { get; set; }
    }
}
