using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class CompetitorPricesModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CompetitorId { get; set; }
        public double Price { get; set; }
        public CompetitorModel Competitor { get; set; }
    }
}
