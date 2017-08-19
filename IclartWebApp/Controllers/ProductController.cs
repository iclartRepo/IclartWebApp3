using IclartWebApp.BLL;
using IclartWebApp.Common.Entities;
using IclartWebApp.Common.Models;
using IclartWebApp.DAL;
using IclartWebApp.DAL.Interfaces;
using IclartWebApp.Models;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IclartWebApp.Controllers
{ 

    public class ProductController : Controller
    {
        private readonly IGenericRepository<ProductCategoryEntity> _productCategoryRepository;
        private readonly IGenericRepository<ProductEntity> _productRepository;
        private readonly IGenericRepository<CompetitorEntity> _competitorRepository;
        private readonly IProductBLL _productBLL;

        public ProductController(IGenericRepository<ProductCategoryEntity> productCategoryRepository, IGenericRepository<ProductEntity> productRepository, IProductBLL productBLL, IGenericRepository<CompetitorEntity> competitorRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _productBLL = productBLL;
            _competitorRepository = competitorRepository;
        }
        // GET: Product
        public ActionResult Index(int id)
        {
            ViewBag.ViewProductId = id;
            if (id == 1)
            {
                var products = GetProducts() as JsonResult;

                var productViewModel = new ProductViewModel
                {
                    Products = products.Data as MessageResult<ProductModel>
                };
                return View(productViewModel);
              
            }
            else
            {
                var productCategories = GetProductCategories() as JsonResult;

                var productViewModel = new ProductViewModel
                {
                    ProductCategories = productCategories.Data as MessageResult<ProductCategoryModel>
                };
                return View(productViewModel);
            }
          
        }

        public ActionResult AddProduct()
        {
          

            var productCategories = GetProductCategories() as JsonResult;
            var competitors = _competitorRepository.Get().OrderBy(i => i.Name).Select(i => new CompetitorModel { Id = i.Id, Name = i.Name }).ToList();

            var competitorMessage = new MessageResult<CompetitorModel>
            {
                isError = false,
                ResultList = competitors,
                Message = "Success",
                Result = null
            };

            var productViewModel = new ProductViewModel
            {
                ProductCategories = productCategories.Data as MessageResult<ProductCategoryModel>,
                Competitors = competitorMessage
            };

            return View(productViewModel);
        }

        public ActionResult UpdateProduct(int id)
        {
            ViewBag.ProductId = id;
           

            var product = GetProduct(id) as JsonResult;
            var productCategories = GetProductCategories() as JsonResult;
            var competitors = _competitorRepository.Get().OrderBy(i => i.Name).Select(i => new CompetitorModel { Id = i.Id, Name = i.Name }).ToList();

            var competitorMessage = new MessageResult<CompetitorModel>
            {
                isError = false,
                ResultList = competitors,
                Message = "Success",
                Result = null
            };

            var productViewModel = new ProductViewModel
            {
                ProductCategories = productCategories.Data as MessageResult<ProductCategoryModel>,
                Competitors = competitorMessage,
                SingleProduct = product.Data as MessageResult<ProductModel>
            };

            return View(productViewModel);
        }

        public ActionResult ViewProduct(int id)
        {
            var product = GetProduct(id) as JsonResult;

            var productViewModel = new ProductViewModel
            {
                SingleProduct = product.Data as MessageResult<ProductModel>
            };

            return View(productViewModel);
        }

        #region GET METHODS
        [HttpGet]
        public ActionResult GetProductCategories()
        {
            try
            {
                var categories = _productCategoryRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();

                TinyMapper.Bind<List<ProductCategoryEntity>, List<ProductCategoryModel>>();

                var categoriesModel = TinyMapper.Map<List<ProductCategoryModel>>(categories);

                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = false,
                    ResultList = categoriesModel,
                    Message = "Success",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                var message = new MessageResult<ClientModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact the administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            try
            {
                var productEntities = _productRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.ProductCategory.Name).ThenBy(i => i.Name).Select(x => new ProductModel { Id = x.Id, Name = x.Name, ProductCategory = new ProductCategoryModel { Name = x.ProductCategory.Name, Id = x.ProductCategory.Id } }).ToList();

                var message = new MessageResult<ProductModel>
                {
                    isError = false,
                    ResultList = productEntities,
                    Message = "Success",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact the administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult SearchProduct(string name)
        {
            try
            {
                var productEntities = _productRepository.Get(i => i.IsDeleted == false && i.Name.Contains(name)).OrderBy(i => i.Name).Select(x => new ProductModel { Id = x.Id, Name = x.Name, ProductCategory = new ProductCategoryModel { Name = x.ProductCategory.Name } }).ToList();


                var message = new MessageResult<ProductModel>
                {
                    isError = false,
                    ResultList = productEntities,
                    Message = "Success",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact the administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult FilterProducts(string name)
        {
            try
            {
                using (var context = new DBContext())
                {                

                    var productEntities = _productRepository.Get(i => i.IsDeleted == false && i.ProductCategory.Name == name).OrderBy(i => i.Name).Select(x => new ProductModel { Id = x.Id, Name = x.Name, ProductCategory = new ProductCategoryModel { Name = x.ProductCategory.Name } }).ToList();

                    var message = new MessageResult<ProductModel>
                    {
                        isError = false,
                        ResultList = productEntities,
                        Message = "Success",
                        Result = null
                    };
                    return Json(message, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact the administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            try
            {
              
                   

                    var productEntity = _productRepository.Get(i => i.Id == id).First();

                    TinyMapper.Bind<ProductEntity, ProductModel>();
                    var productModel = TinyMapper.Map<ProductModel>(productEntity);

                    var message = new MessageResult<ProductModel>
                    {
                        isError = false,
                        ResultList = null,
                        Message = "Success",
                        Result = productModel
                    };
                    return Json(message, JsonRequestBehavior.AllowGet);
                

            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact the administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPrice(int clientId, int productId)
        {
            try
            {
                

                var leastPrice = _productBLL.GetBestPrice(clientId, productId);

                return Json(leastPrice, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact the administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region POST METHODS
        [HttpPost]
        public ActionResult AddProductCategory(string name)
        {
            try
            {
                _productBLL.AddProductCategory(name);
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Product Category added successfully!",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult AddProduct(ProductFormModel newProduct)
        {
            try
            {
                _productBLL.AddProduct(newProduct);
                var message = new MessageResult<ProductFormModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Product added successfully!",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductFormModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region PUT METHODS
        [HttpPut]
        public ActionResult UpdateProductCategory(int id, string name)
        {
            try
            {
                _productBLL.UpdateProductCategory(id, name);
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Product Category updated successfully!",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPut]
        public ActionResult UpdateProduct(int id, ProductFormModel product)
        {
            try
            {
                _productBLL.UpdateProduct(id, product);
                var message = new MessageResult<ProductModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Product updated successfully!",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region DELETE METHODS
        [HttpDelete]
        public ActionResult DeleteProductCategory(int id)
        {
            try
            {
                _productBLL.DeleteProductCategory(id);
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Success",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpDelete]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productBLL.DeleteProduct(id);
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Success",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<ProductCategoryModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}