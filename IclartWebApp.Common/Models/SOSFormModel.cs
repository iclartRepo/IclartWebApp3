using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class SOSFormModel
    {
        public SOSModel Sos {get;set;}
        public List<SOSProductModel> StandardOrders { get; set; }
        public List<SOSCustomModel> CustomOrders { get; set; }
    }
}
