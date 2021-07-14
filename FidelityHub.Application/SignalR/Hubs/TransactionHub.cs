using FidelityHub.Application.Models.Vendor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FidelityHub.Application.SignalR.Hubs
{
    public interface ITransactionHubService
    {
    }
    
    public class TransactionHub : Hub<ITransactionHubService>
    {
    }
}
