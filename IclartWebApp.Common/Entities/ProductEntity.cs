using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public double CompanyPrice { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategoryEntity ProductCategory { get; set; }
        public virtual ICollection<CompetitorPricesEntity> CompetitorPrices { get; set; }
        public virtual ICollection<SOSProductEntity> SOSOrders { get; set; }
    }
}
