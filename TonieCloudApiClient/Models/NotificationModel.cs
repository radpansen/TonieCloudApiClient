using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TonieCloudApiClient.Models
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public bool Read { get; set; }
        public DateTime Timestamp { get; set; }
        public string NType { get; set; }
        public object InvitationToken { get; set; } // TODO don't know yet, what type this is
        public Guid MembershipId { get; set; }
        public Guid? TonieId { get; set; }
        public string TonieReason { get; set; }
        public string Text { get; set; }
    }
}
