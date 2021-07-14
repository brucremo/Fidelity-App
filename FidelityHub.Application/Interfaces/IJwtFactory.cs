using FidelityHub.Application.Models.Authentication;
using FidelityHub.Database.Entities.UsrSchema;
using System.Threading.Tasks;

namespace FidelityHub.Application.Interfaces
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(AspNetUser user);
    }
}
