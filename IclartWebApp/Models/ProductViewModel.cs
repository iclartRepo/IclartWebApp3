using IclartWebApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IclartWebApp.Models
{
    public class ProductViewModel
    {
        public MessageResult<ProductCategoryModel> ProductCategories { get; set; }
        public MessageResult<ProductModel> Products { get; set; }
    }
}