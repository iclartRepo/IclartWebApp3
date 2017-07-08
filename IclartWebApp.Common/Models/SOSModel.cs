using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class SOSModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Sos_Date { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public bool Pickup { get; set; }
        public bool Exported { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ClientModel Client { get; set; }
        public List<SOSProductModel> Orders { get; set; }
        public List<SOSCustomModel> CustomOrders { get; set; }
    }
}
