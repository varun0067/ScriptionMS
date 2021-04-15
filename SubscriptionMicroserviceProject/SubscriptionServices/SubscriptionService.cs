using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SubscriptionMicroserviceProject.DBHelper;
using SubscriptionMicroserviceProject.DTO;
using SubscriptionMicroserviceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.SubscriptionServices
{
    public class SubscriptionService : ISubscriptionService
    {
        private ISubscriptionDBHelper _dbHelper;
        int PrescriptionNumber;
        int SubscriptionNumber;

        public SubscriptionService(ISubscriptionDBHelper dBHelper)
        {
            _dbHelper = dBHelper;
            PrescriptionNumber = 101;
            SubscriptionNumber = 1001;
        }
        


        public bool CreatePrescription(MemberPrescription prescription)
        {
            prescription.PrescriptionId = PrescriptionNumber;
            bool PrescriptionCreated=_dbHelper.AddPrescription(prescription);
            if(PrescriptionCreated==true)
            {
                PrescriptionNumber++;
                return true;
            }
            return false;
        }

        public string Subscribe(SubscriptionDTO subscription,string token)
        {
            MemberPrescription prescription = getPrescriptionId(subscription.PrescriptionId);
            if(prescription==null)
            {
                return "Prescription Not Found";
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:60177");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            StringContent content = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/Drugs/getDispatchableDrugStock/" + prescription.DrugId + "/"+subscription.MemberLocation,content).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                DispatchableDrugStockDTO stock = response.Content.ReadAsAsync<DispatchableDrugStockDTO>().Result;

                int quantityRequired = prescription.DosagePerDay * (7 * prescription.PrescriptionCourseInWeeks);

                if (quantityRequired > stock.AvailableStock)
                {
                    return "Stock Not Available Cannot add subscription";
                }
                else
                {
                    RefillDTO refill = new RefillDTO();
                    refill.SubscriptionId = SubscriptionNumber;
                    refill.RefillDate = subscription.SubscriptionDate;
                    refill.DosagePerDay = prescription.DosagePerDay;
                    refill.CourseInWeeks = prescription.PrescriptionCourseInWeeks;
                    refill.Location = subscription.MemberLocation;
                    refill.CostPerUnit = stock.CostPerUnit;
                    refill.RefillOccurrence = subscription.RefillOccurrence;

                    HttpClient client1 = new HttpClient();
                    client1.BaseAddress = new Uri("http://localhost:4010");
                    client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(refill), Encoding.UTF8, "application/json");
                    HttpResponseMessage response1 = client1.PostAsync("/api/Refill/CreateRefill", content1).Result;

                    if (response1.StatusCode == HttpStatusCode.OK)
                    {
                        try
                        {
                            MemberSubscription subscription1 = new MemberSubscription()
                            {
                                SubscriptionId = SubscriptionNumber,
                                MemberId = subscription.MemberId,
                                SubscriptionDate=subscription.SubscriptionDate,
                                MemberLocation=subscription.MemberLocation,
                                PrescriptionId=subscription.PrescriptionId,
                                SubscriptionStatus=true
                            };
                            if (subscription.RefillOccurrence == DTO.Occurrence.Weekly)
                                subscription1.RefillOccurrence = Models.Occurrence.Weekly;
                            else
                                subscription1.RefillOccurrence = Models.Occurrence.Monthly;

                            bool subAdded=_dbHelper.AddSubscription(subscription1);
                            if (subAdded)
                            {
                                SubscriptionNumber++;
                                return "Subscription Created.";
                            }
                            else
                                return "Could Not Create Subscription";
                        }
                        catch(Exception)
                        {
                            return "Could Not Create Subscription";
                        }
                    }
                    else
                    {
                        return "Could Not Create RefillOrders";
                    }
                }
            }
            else
            {
                return "Drug Not Found";
            }

        }



        public string UnSubscribe(int subscriptionId,string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:4010");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response1 = client.GetAsync("/api/Refill/checkPendingPaymentStatus/" + subscriptionId).Result;
            

            if (response1.StatusCode == HttpStatusCode.OK)
            {
                bool unsubscribed = UnSubscribePrescription(subscriptionId);
                if (unsubscribed)
                    return "Unsubscribed";
                else
                    return "Cannot Unsubscribe Due to pending payment";
            }
            else
            {
                return "Cannot Unsubscribe Due to pending payment";
            }
        }

        public MemberPrescription getPrescriptionId(int prescriptionId)
        {
            MemberPrescription prescription= _dbHelper.getPrescriptionId(prescriptionId);
            if (prescription != null)
                return prescription;
            return null;
        }

        public bool UnSubscribePrescription(int subscriptionId)
        {
            bool subscriptionRemoved = _dbHelper.RemoveSubscription(subscriptionId);
            return subscriptionRemoved;
        }

        public List<MemberPrescription> GetPrescriptions(int memberId)
        {
            return _dbHelper.GetPrescriptions(memberId);
        }

        public List<MemberSubscription> GetSubscriptions(int memberId)
        {
            return _dbHelper.GetSubscriptions(memberId);
        }
    }
}
