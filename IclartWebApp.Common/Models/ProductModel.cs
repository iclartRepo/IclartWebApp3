using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public double CompanyPrice { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ProductCategoryModel ProductCategory { get; set; }
        public List<CompetitorPricesModel> CompetitorPrices { get; set; }


    }
}
