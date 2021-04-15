using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.Models
{
    public class DrugDTO
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Composition { get; set; }
        public List<DrugLocationDTO> QuantityByLocation { get; set; }
        public int UnitsInPackage { get; set; }
        public double CostPerPackage { get; set; }


        public DrugDTO()
        {
            QuantityByLocation = new List<DrugLocationDTO>();
        }
    }
}
