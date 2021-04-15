using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.DTO
{
    public enum Occurrence
    {
        Weekly, Monthly
    };
    public class SubscriptionDTO
    {
        public int MemberId { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public int PrescriptionId { get; set; }
        public Occurrence RefillOccurrence { get; set; }
        public string MemberLocation { get; set; }
        public string Token { get; set; }
    }
}
