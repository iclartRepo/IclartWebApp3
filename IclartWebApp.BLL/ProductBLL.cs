using AutoMapper;
using IclartWebApp.Common.Entities;
using IclartWebApp.Common.Models;
using IclartWebApp.DAL;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.BLL
{
    public class ProductBLL
    {
        private GenericRepository<ProductCategoryEntity> _categoryRepository;
        private GenericRepository<ProductEntity> _productRepository;
        private GenericRepository<CompetitorEntity> _competitorRepository;
        private GenericRepository<CompetitorPricesEntity> _competitorPricesRepository;
        private GenericRepository<ClientEntity> _clientRepository;
        private DBContext context;

        public ProductBLL()
        {
            context = new DBContext();
            _categoryRepository = new GenericRepository<ProductCategoryEntity>(context);
            _productRepository = new GenericRepository<ProductEntity>(context);
            _competitorPricesRepository = new GenericRepository<CompetitorPricesEntity>(context);
            _competitorRepository = new GenericRepository<CompetitorEntity>(context);
            _clientRepository = new GenericRepository<ClientEntity>(context);
        }
        #region Product Category
        public void AddProductCategory(string name)
        {
            if (ValidateIfCategoryExists(name, 0))
            {
                var newCategory = new ProductCategoryEntity
                {
                    Name = name,
                    CreatedDate = DateTime.Now
                };
                _categoryRepository.Insert(newCategory);
            }
            else
            {
                throw new Exception("Product Category already exists!");
            }
        }
        public void UpdateProductCategory(int id, string name)
        {
            var categoryToUpdate = _categoryRepository.Get(i => i.Id == id).FirstOrDefault();
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = name;
                categoryToUpdate.ModifiedDate = DateTime.Now;
                _categoryRepository.Update(categoryToUpdate);
            }
        }
        public void DeleteProductCategory(int id)
        {
            var category = _categoryRepository.Get(i => i.Id == id).FirstOrDefault();
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedDate = DateTime.Now;
                _categoryRepository.SoftDelete(category);
            }
            else
            {
                throw new Exception("Product Category does not exist!");
            }
        }
        #endregion

        #region Product
        public void AddProduct(ProductFormModel newProduct)
        {
            var exists = ValidateIfProductExists(newProduct.Product);
            if (exists)
            {
                TinyMapper.Bind<ProductModel, ProductEntity>();
                var productEntity = TinyMapper.Map<ProductEntity>(newProduct.Product);
                productEntity.CreatedDate = DateTime.Now;
                productEntity.CompetitorPrices = new List<CompetitorPricesEntity>();

                var productCategory = _categoryRepository.Get(i => i.Id == newProduct.ProductCategory.Id).First();
                productEntity.ProductCategory = productCategory;

                if (newProduct.CompetitorPrices != null)
                {
                    TinyMapper.Bind<CompetitorPricesModel, CompetitorPricesEntity>();
                    for (int i = 0; i < newProduct.CompetitorPrices.Count; i++)
                    {
                        var competitorPrice = newProduct.CompetitorPrices[i];
                        var competitorPriceEntity = TinyMapper.Map<CompetitorPricesEntity>(competitorPrice);
                        var competitor = _competitorRepository.Get(y => y.Id == competitorPriceEntity.CompetitorId).First();
                        competitorPriceEntity.Competitor = competitor;
                        productEntity.CompetitorPrices.Add(competitorPriceEntity);
                    }
                }

                _productRepository.Insert(productEntity);
            }
            else
            {
                throw new Exception("Product already exists!");
            }
        }
        public void UpdateProduct(int id, ProductFormModel product)
        {
            var exists = ValidateIfProductExists(product.Product);
            if (exists)
            {
                var productEntity = _productRepository.Get(i => i.Id == product.Product.Id).First();
                var competitorPrices = productEntity.CompetitorPrices.ToList();
                for (int i = 0; i < competitorPrices.Count; i++)
                {
                    var competitorPrice = competitorPrices[i];
                    productEntity.CompetitorPrices.Remove(competitorPrice);
                    _competitorPricesRepository.HardDelete(competitorPrice);
                }

                Mapper.Initialize(cfg => cfg.CreateMap<ProductModel, ProductEntity>().ForMember(x => x.CreatedDate, opt => opt.Ignore()).ForMember(x => x.CompetitorPrices, opt => opt.Ignore()).ForMember(x => x.ProductCategory, opt => opt.Ignore()));
                Mapper.Map(product.Product, productEntity);
                productEntity.ModifiedDate = DateTime.Now;

                var productCategory = _categoryRepository.Get(i => i.Id == product.ProductCategory.Id).First();
                productEntity.ProductCategory = productCategory;

                TinyMapper.Bind<CompetitorPricesModel, CompetitorPricesEntity>();
                if (product.CompetitorPrices != null)
                {
                    for (int i = 0; i < product.CompetitorPrices.Count; i++)
                    {
                        var competitorPrice = product.CompetitorPrices[i];
                        var competitorPriceEntity = TinyMapper.Map<CompetitorPricesEntity>(competitorPrice);
                        var competitor = _competitorRepository.Get(y => y.Id == competitorPrice.CompetitorId).First();
                        competitorPriceEntity.Competitor = competitor;
                        productEntity.CompetitorPrices.Add(competitorPriceEntity);
                    }
                }

                _productRepository.Update(productEntity);
            }
            else
            {
                throw new Exception("Product already exists!");
            }
        }
        public void DeleteProduct(int id)
        {
            var productEntity = _productRepository.Get(i => i.Id == id && i.IsDeleted == false).FirstOrDefault();
            if (productEntity != null)
            {
                productEntity.IsDeleted = true;
                productEntity.ModifiedDate = DateTime.Now;
                _productRepository.SoftDelete(productEntity);
            }
        }
        public double GetBestPrice(int clientId, int productId)
        {
            var productEntity = _productRepository.Get(i => i.Id == productId).First();
            var client = _clientRepository.Get(y => y.Id == clientId).FirstOrDefault();

            var leastPrice = productEntity.CompanyPrice * ((100 - client.Discount_Scheme) / 100);

            var otherPrices = (from p in productEntity.CompetitorPrices
                               join discount in client.CompetitorDiscountSchemes
                               on p.CompetitorId equals discount.CompetitorId
                               select (p.Price * ((100 - discount.DiscountScheme) / 100))).ToList();
            otherPrices.Add(leastPrice);

            return Math.Round(otherPrices.Min(), 2);

        }
        #endregion

        #region Validations
        private bool ValidateIfCategoryExists(string name, int id)
        {
            var categories = _categoryRepository.Get();
            var categoryCheck = (from c in categories
                                 where c.Name.Trim(' ').ToLower() == name.Trim(' ').ToLower() && c.IsDeleted == false
                                 select c).ToList();
            if (categoryCheck.Count == 0)
            {
                return true;
            }
            else if (id != 0 && id == categoryCheck[0].Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidateIfProductExists(ProductModel product)
        {
            var products = _productRepository.Get();
            var productCheck = (from c in products
                                where c.Name.Trim(' ').ToLower() == product.Name.Trim(' ').ToLower() && c.IsDeleted == false
                                select c).ToList();
            if (productCheck.Count == 0)
            {
                return true;
            }
            else if (product.Id != 0 && product.Id == productCheck[0].Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
