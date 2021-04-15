using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.Models
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string MailId { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
