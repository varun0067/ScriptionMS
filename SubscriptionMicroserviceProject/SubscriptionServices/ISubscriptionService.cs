using Microsoft.AspNetCore.Mvc;
using SubscriptionMicroserviceProject.DTO;
using SubscriptionMicroserviceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.SubscriptionServices
{
    public interface ISubscriptionService
    {
        public bool CreatePrescription(MemberPrescription prescription);
        public string Subscribe(SubscriptionDTO subscription,string token);
        public string UnSubscribe(int subscriptionId,string token);
        public MemberPrescription getPrescriptionId(int prescriptionId);
        public List<MemberPrescription> GetPrescriptions(int memberId);
        public List<MemberSubscription> GetSubscriptions(int memberId);
        public bool UnSubscribePrescription(int subscriptionId);

        //public string Unsubscribe(int memberId, int subscriptionId);
        //public List<MemberSubscription> GetData();
    }
}
