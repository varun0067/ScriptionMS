using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.DTO
{
    public class RefillDTO
    {
        public DateTime RefillDate { get; set; }
        public int SubscriptionId { get; set; }
        public int DosagePerDay { get; set; }
        public int CourseInWeeks { get; set; }
        public string Location { get; set; }
        public double CostPerUnit { get; set; }
        public Occurrence RefillOccurrence { get; set; }
    }
}
