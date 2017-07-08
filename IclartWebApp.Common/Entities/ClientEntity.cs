using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Entities
{
    public class ClientEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
        public string Office_Address { get; set; }
        public string Warehouse_Address { get; set; }
        public double Credit_Limit { get; set; }
        public bool Dealer { get; set; }
        public double Accounts_Receivables { get; set; }
        public string Credit_Terms { get; set; }
        public double Discount_Scheme { get; set; }
        public string Agent { get; set; }
        public string Contacts_Order { get; set; }
        public string Contacts_Accounting { get; set; }
        public string Telephone_Number { get; set; }
        public string Fax_Number { get; set; }
        public string Email { get; set; }
        public bool Rounded_Payment { get; set; }
        public string Usual_Ordered_Item { get; set; }
        public double Witholding_Tax { get; set; }
        public bool Vat_Exemption { get; set; }
        public string Collection_Period { get; set; }
        public bool Combine_Items { get; set; }
        public double Overpayment_Counter { get; set; }

        public DateTime Created_Date { get; set; }
        
        public DateTime? Modified_Date { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<CompetitorDiscountSchemesEntity> CompetitorDiscountSchemes { get; set; }
        public virtual ICollection<SOSEntity> SalesOrderSlips { get; set; }
    }
}
