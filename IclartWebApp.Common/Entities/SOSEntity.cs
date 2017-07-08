using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Entities
{
    public class SOSEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Sos_Date { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public bool Pickup { get; set; }
        public bool Exported { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [ForeignKey("ClientId")]
        public virtual ClientEntity ClientEntity { get; set; }
        public virtual ICollection<SOSProductEntity> Orders { get; set; }
        public virtual ICollection<SOSCustomEntity> CustomOrders { get; set; }
    }
}
