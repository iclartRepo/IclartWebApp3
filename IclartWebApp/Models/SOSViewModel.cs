using IclartWebApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IclartWebApp.Models
{
    public class SOSViewModel
    {
        public MessageResult<SOSModel> SOSList { get; set; }
        public MessageResult<ClientModel> ClientList { get; set; }
        public MessageResult<ProductModel> Products { get; set; }
        public MessageResult<ProductCategoryModel> ProductCategories { get; set; }
        public SOSModel SingleSOS { get; set; }
    }
}