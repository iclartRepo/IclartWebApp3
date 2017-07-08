using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class ClientFormModel
    {
        public ClientModel Client { get; set; }
        public List<CompetitorDiscountSchemesModel> CompetitorDiscountSchemes { get; set; }
    }
}
