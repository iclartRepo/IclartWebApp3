using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Entities
{
    public class PasswordExpiryEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
