using Microsoft.AspNetCore.Mvc;
using SubscriptionMicroserviceProject.Models;
using SubscriptionMicroserviceProject.DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SubscriptionMicroserviceProject.SubscriptionServices;
using SubscriptionMicroserviceProject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using log4net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubscriptionMicroserviceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SubscriptionController : ControllerBase
    {
        ISubscriptionService _subscriptionService;
        private readonly ILog _log4net = LogManager.GetLogger(typeof(SubscriptionController));
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("createPrescription")]
        public ActionResult CreatePrescription([FromBody] MemberPrescription prescription)
        {

            _log4net.Info("Subscription MicroService : " + nameof(CreatePrescription));
            try
            {
                bool added = _subscriptionService.CreatePrescription(prescription);
                if (added)
                {
                    return Ok(prescription);
                }
                return BadRequest("Could not add Prescription");
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(CreatePrescription));
                return null;
            }

        }

        [HttpPost("Subscribe")]
        public ActionResult Subscription([FromBody] SubscriptionDTO subscription)
        {
            _log4net.Info("Subscription MicroService : " + nameof(Subscription));
            try
            {

                string subscribeResult = _subscriptionService.Subscribe(subscription, subscription.Token);
                if (subscribeResult == "Subscription Create")
                    return Ok(subscribeResult);
                else
                    return BadRequest(subscribeResult);
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(Subscription));
                return null;
            }
        }

        [HttpPost("UnSubscribe/{subscriptionId}")]
        public ActionResult UnSubscription(int subscriptionId, [FromBody] string token)
        {
            _log4net.Info("Subscription MicroService : " + nameof(UnSubscription));
            try
            {
                string unSubscribeResult = _subscriptionService.UnSubscribe(subscriptionId, token);
                if (unSubscribeResult == "Unsubscribed")
                    return Ok(unSubscribeResult);
                else
                    return BadRequest(unSubscribeResult);
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(UnSubscription));
                return null;
            }
        }
        [HttpGet("getAllPrescriptions/{memberId}")]
        public ActionResult GetAllPrescriptions(int memberId)
        {
            _log4net.Info("Subscription MicroService : " + nameof(GetAllPrescriptions));
            try
            {
                List<MemberPrescription> prescriptions = _subscriptionService.GetPrescriptions(memberId);
                if (prescriptions != null)
                    return Ok(prescriptions);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(GetAllPrescriptions));
                return null;
            }

        }

        [HttpGet("getAllSubscriptions/{memberId}")]
        public ActionResult GetAllSubscriptions(int memberId)
        {
            _log4net.Info("Subscription MicroService : " + nameof(GetAllSubscriptions));
            try
            {

                List<MemberSubscription> subscriptions = _subscriptionService.GetSubscriptions(memberId);
                if (subscriptions != null)
                    return Ok(subscriptions);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(GetAllSubscriptions));
                return null;
            }
        }
    }
}
