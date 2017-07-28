using IclartWebApp.Common.Entities;
using IclartWebApp.DAL;
using MigrationTool.MySQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MigrationTool
{
    public partial class Migration : Form
    {
        public Migration()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void migrate_Click(object sender, EventArgs e)
        {
            string server = ConfigurationManager.AppSettings["Server"];
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string dbname = ConfigurationManager.AppSettings["DBName"];

            var dbExecute = new DBExecute(server, username, password, dbname);
            var clients = dbExecute.RetrieveClientsAR();
            var products = dbExecute.retrieveProducts();
            using (var context = new DBContext())
            {
                /* Adding Competitors */

                //var competitorA = new CompetitorEntity
                //{
                //    Name = "Competitor A",
                //    CreatedDate = DateTime.Now
                //};
                //var competitorB = new CompetitorEntity
                //{
                //    Name = "Competitor B",
                //    CreatedDate = DateTime.Now
                //};
                //context.Competitors.Add(competitorA);
                //context.Competitors.Add(competitorB);

                /* Adding Clients */

                var clientList = new List<ClientEntity>();
                for (int i = 0; i < clients.Count; i++)
                {
                    var client = clients[i];

                    var dealer = false;

                    var rounded = false;
                    var vat = false;
                    var combine = false;

                    if (int.Parse(client[5].ToString()) == 1)
                    {
                        dealer = true;
                    }

                    if (int.Parse(client[19].ToString()) == 1)
                    {
                        rounded = true;
                    }
                    if (int.Parse(client[22].ToString()) == 1)
                    {
                        vat = true;
                    }
                    if (int.Parse(client[24].ToString()) == 1)
                    {
                        combine = true;
                    }
                    var newClient = new ClientEntity
                    {
                        Name = client[1].ToString(),
                        TIN = client[2].ToString(),
                        Office_Address = client[3].ToString(),
                        Credit_Limit = double.Parse(client[4].ToString()),
                        Dealer = dealer,
                        Accounts_Receivables = 0,
                        Credit_Terms = client[7].ToString(),
                        Discount_Scheme = double.Parse(client[8].ToString()),
                        Agent = client[12].ToString(),
                        Contacts_Order = client[13].ToString(),
                        Contacts_Accounting = client[14].ToString(),
                        Warehouse_Address = client[15].ToString(),
                        Telephone_Number = client[16].ToString(),
                        Fax_Number = client[17].ToString(),
                        Email = client[18].ToString(),
                        Rounded_Payment = rounded,
                        Usual_Ordered_Item = client[20].ToString(),
                        Witholding_Tax = double.Parse(client[21].ToString()),
                        Vat_Exemption = vat,
                        Collection_Period = client[23].ToString(),
                        Combine_Items = combine,
                        Overpayment_Counter = 0,
                        Created_Date = DateTime.Now
                    };
                    var competitorA = context.Competitors.First(y => y.Name == "Competitor A");
                    var competitorB = context.Competitors.First(y => y.Name == "Competitor B");
                    var dsSchemes = new List<CompetitorDiscountSchemesEntity>();
                    var competitorDS1 = new CompetitorDiscountSchemesEntity
                    {
                        DiscountScheme = double.Parse(client[9].ToString()),
                        CompetitorEntity = competitorA
                    };
                    var competitorDS2 = new CompetitorDiscountSchemesEntity
                    {
                        DiscountScheme = double.Parse(client[10].ToString()),
                        CompetitorEntity = competitorB
                    };
                    dsSchemes.Add(competitorDS1);
                    dsSchemes.Add(competitorDS2);

                    newClient.CompetitorDiscountSchemes = dsSchemes;
                    context.Clients.Add(newClient);
                }

                /* Add Products */

                //var category1 = new ProductCategoryEntity
                //{
                //    Name = "Paper Bag",
                //    CreatedDate = DateTime.Now,
                //    Products = new List<ProductEntity>()
                //};
                //var category2 = new ProductCategoryEntity
                //{
                //    Name = "POS Roll",
                //    CreatedDate = DateTime.Now,
                //    Products = new List<ProductEntity>()
                //};
                //var category3 = new ProductCategoryEntity
                //{
                //    Name = "Fax Paper",
                //    CreatedDate = DateTime.Now,
                //    Products = new List<ProductEntity>()
                //};
                //var category4 = new ProductCategoryEntity
                //{
                //    Name = "Continuous Form",
                //    CreatedDate = DateTime.Now,
                //    Products = new List<ProductEntity>()
                //};


                //for (int j = 0; j < products.Count; j++)
                //{
                //    var product = products[j];

                //    var newProduct = new ProductEntity
                //    {
                //        Name = product[2].ToString(),
                //        CompanyPrice = double.Parse(product[3].ToString()),
                //        CreatedDate = DateTime.Now
                //    };

                //    var competitorPrices = new List<CompetitorPricesEntity>();
                //    var competitorA = context.Competitors.First(y => y.Name == "Competitor A");
                //    var competitorB = context.Competitors.First(y => y.Name == "Competitor B");

                //    var priceA = new CompetitorPricesEntity
                //    {
                //        Price = double.Parse(product[4].ToString()),
                //        Competitor = competitorA
                //    };
                //    var priceB = new CompetitorPricesEntity
                //    {
                //        Price = double.Parse(product[5].ToString()),
                //        Competitor = competitorB
                //    };
                //    competitorPrices.Add(priceA);
                //    competitorPrices.Add(priceB);
                //    newProduct.CompetitorPrices = competitorPrices;

                //    if (product[1].ToString() == "Paper Bag")
                //    {
                //        category1.Products.Add(newProduct);
                //    }
                //    else if (product[1].ToString() == "POS Roll")
                //    {
                //        category2.Products.Add(newProduct);
                //    }
                //    else if (product[1].ToString() == "Fax Paper")
                //    {
                //        category3.Products.Add(newProduct);
                //    }
                //    else
                //    {
                //        category4.Products.Add(newProduct);
                //    }

                //}

                //context.ProductCategories.Add(category1);
                //context.ProductCategories.Add(category2);
                //context.ProductCategories.Add(category3);
                //context.ProductCategories.Add(category4);
                context.SaveChanges();
            }
            MessageBox.Show("Done");

        }

        private void settings_Click(object sender, EventArgs e)
        {
            Settings set = new MigrationTool.Settings();
            set.Show();
        }
    }
}
