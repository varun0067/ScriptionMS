using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroserviceProject.DTO
{
    public class DispatchableDrugStockDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int AvailableStock { get; set; }

        public double CostPerUnit { get; set; }

        public DispatchableDrugStockDTO()
        {

        }
    }
}
