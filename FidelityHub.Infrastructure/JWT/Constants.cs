

namespace FidelityHub.Infrastructure.JWT
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string AuthenticatedUser = "AuthenticatedUser";
                public const string CustomerAccess = "Customer";
                public const string VendorAccess = "Vendor";
                public const string VendorAdminAccess = "VendorAdmin";
                public const string SupportAccess = "Support";
                public const string AdminAccess = "Admin";
            }
        }
    }
}
