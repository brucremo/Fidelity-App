namespace FidelityHub.Application.Models.Registration
{
    public class ThirdPartyRegistrationModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public Database.Entities.UsrSchema.Customer GetCustomerEntity(string id, int userTypeId)
        {
            return new Database.Entities.UsrSchema.Customer
            {
                Email = this.Email,
                LegalName = this.Name,
                Id = id,
                UserName = this.Name,
                UserTypeId = userTypeId
            };
        }
    }
}
