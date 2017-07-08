using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IclartWebApp.Common.Entities;

namespace IclartWebApp.Common.Models
{
    public class CompetitorDiscountSchemesModel
    {
        public int Id { get; set; }
        public int CompetitorId { get; set; }
        public int ClientId { get; set; }
        public double DiscountScheme { get; set; }
        public CompetitorModel CompetitorEntity { get; set; }

    }
}
