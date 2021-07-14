using FidelityHub.Application.Interfaces;
using FidelityHub.Database.Entities.RefSchema;
using FidelityHub.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FidelityHub.Persistence
{
    public class RefSchemaDataReader : IRefSchemaDataReader
    {
        private RefDbContext Context { get; }

        public RefSchemaDataReader(RefDbContext context)
        {
            this.Context = context;
        }

        public async Task<bool> RegisterContactRequest(DateTime timestamp, string sentTo, string status, bool success)
        {
            await this.Context.ContactRequests.AddAsync(new SentContactRequests(timestamp, sentTo, status, success));
            return await this.Context.SaveChangesAsync() > 0;
        }
    }
}
