using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class ProductFormModel
    {
        public ProductModel Product { get; set; }
        public ProductCategoryModel ProductCategory { get; set; }
        public List<CompetitorPricesModel> CompetitorPrices { get; set; }
    }
}
