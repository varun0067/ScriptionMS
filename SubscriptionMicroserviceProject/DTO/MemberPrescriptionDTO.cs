using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.DTO
{
    public class MemberPrescriptionDTO
    {
        public int MemberId { get; set; }
        public int InsurancePolicyNumber { get; set; }
        public string InsuranceProvider { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public int DrugId { get; set; }
        public int DosagePerDay { get; set; }
        public int PrescriptionCourseInWeeks { get; set; }
        public string DoctorDetails { get; set; }
    }
}
