using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Database.Entities.UsrSchema
{
    public class AspNetUserTokens
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
