using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Entities
{
    public class CompetitorDiscountSchemesEntity
    {
        [Key]
        public int Id { get; set; }
        public int CompetitorId { get; set; }
        public int ClientId { get; set; }
        public double DiscountScheme { get; set; }
        [ForeignKey("CompetitorId")]
        public virtual CompetitorEntity CompetitorEntity { get; set; }
        [ForeignKey("ClientId")]
        public virtual ClientEntity ClientEntity { get; set; }
    }
}
