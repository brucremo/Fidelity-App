using Microsoft.AspNetCore.Identity;
using System;

namespace FidelityHub.Database.Entities.UsrSchema
{
    public partial class AspNetUser : IdentityUser
    {
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public int? UserTypeId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
