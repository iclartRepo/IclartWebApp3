using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Entities
{
    public class SOSProductEntity
    {
        public int Id { get; set; }
        public int SOSId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int QuantityDelivered { get; set; }
        public bool Discarded { get; set; }
        [ForeignKey("SOSId")]
        public virtual SOSEntity SalesOrderSlip { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductEntity Product { get; set; }

    }
}
