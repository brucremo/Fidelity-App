using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FidelityHub.Database.Entities.RefSchema
{
    public class SentContactRequests
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string SentTo { get; set; }
        public string Status { get; set; }
        public bool Success { get; set; }

        public SentContactRequests(DateTime timestamp, string sentTo, string status, bool success)
        {
            this.Timestamp = timestamp;
            this.SentTo = sentTo;
            this.Status = status;
            this.Success = success;
        }
    }
}
