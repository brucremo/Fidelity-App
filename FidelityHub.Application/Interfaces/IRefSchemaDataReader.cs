using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FidelityHub.Application.Interfaces
{
    public interface IRefSchemaDataReader
    {
        // --- Commands ---
        public Task<bool> RegisterContactRequest(DateTime timestamp, string sentTo, string status, bool success);
    }
}
