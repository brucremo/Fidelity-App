namespace FidelityHub.Application.Models.ThirdParty.Uber
{
    public class UberOrderNotificationWebhook
    {
        public string event_type { get; set; }
        public string event_id { get; set; }
        public int event_time { get; set; }
        public UberOrderNotificationMetadata meta { get; set; }
        public string resource_href { get; set; }
    }

    public class UberOrderNotificationMetadata
    {
        public string resource_id { get; set; }
        public string status { get; set; }
        public string user_id { get; set; }
    }
}
