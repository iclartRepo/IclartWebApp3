using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class SOSCustomModel
    {
        public int Id { get; set; }
        public int SOSId { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public int QuantityDelivered { get; set; }
        public bool Discarded { get; set; }
        public SOSModel SalesOrderSlip { get; set; }
    }
}
