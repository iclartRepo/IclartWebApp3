using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Entities
{
    public class CompetitorPricesEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CompetitorId { get; set; }
        public double Price { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductEntity Product { get; set; }
        [ForeignKey("CompetitorId")]
        public virtual CompetitorEntity Competitor { get; set; }
    }
}
