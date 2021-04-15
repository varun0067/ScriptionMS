using SubscriptionMicroserviceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.DBHelper
{
    public class SubscriptionDBHelper: ISubscriptionDBHelper
    {
        List<MemberPrescription> memberPrescriptions = new List<MemberPrescription>();
        List<MemberSubscription> memberSubscriptions = new List<MemberSubscription>();

        public bool AddPrescription(MemberPrescription prescription)
        {
            try
            {
                memberPrescriptions.Add(prescription);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool AddSubscription(MemberSubscription subscription)
        {
            try
            {
                memberSubscriptions.Add(subscription);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MemberPrescription getPrescriptionId(int prescriptionId)
        {
            return memberPrescriptions.Where(p => p.PrescriptionId == prescriptionId).FirstOrDefault();
        }

        public bool RemoveSubscription(int subscriptionId)
        {
            MemberSubscription subscription = memberSubscriptions.Where(s => s.SubscriptionId == subscriptionId).FirstOrDefault();
            if (subscription != null)
            {
                subscription.SubscriptionStatus = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<MemberPrescription> GetPrescriptions(int memberId)
        {
            List<MemberPrescription> prescriptions = memberPrescriptions.Where(p => p.MemberId == memberId).ToList();
            return prescriptions;
        }

        public List<MemberSubscription> GetSubscriptions(int memberId)
        {
            List<MemberSubscription> subscriptions = memberSubscriptions.Where(p => p.MemberId == memberId).ToList();
            return subscriptions;
        }

        //public MemberPrescription getPrescriptionId(int prescriptionId)
        //{
        //    MemberPrescription prescription = memberPrescriptions.Where(m => m.PrescriptionId == prescriptionId).FirstOrDefault();

        //    return prescription;
        //}
        //public List<MemberSubscription> Get()
        //{
        //    return memberSubscriptions.ToList();
        //}

        //public void Add(MemberSubscription memberSubscription)
        //{
        //    memberSubscriptions.Add(memberSubscription);
        //}

        //public void Remove(int subscriptionId)
        //{
        //    memberSubscriptions.RemoveAll(s => s.SubscriptionId == subscriptionId);
        //}

    }
}
