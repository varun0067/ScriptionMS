using SubscriptionMicroserviceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.DBHelper
{
    public interface ISubscriptionDBHelper
    {

        public MemberPrescription getPrescriptionId(int prescriptionId);
        public bool AddPrescription(MemberPrescription prescription);
        public bool AddSubscription(MemberSubscription subscription);

        public List<MemberPrescription> GetPrescriptions(int memberId);

        public List<MemberSubscription> GetSubscriptions(int memberId);
        public bool RemoveSubscription(int subscriptionId);
        //public void Add(MemberSubscription memberSubscription);
        //public void Remove(int subscriptionId);
    }
}
