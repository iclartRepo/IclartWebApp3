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
    public class SOSController : Controller
    {
        private readonly ISosBLL _sosBLL;
        private readonly IGenericRepository<SOSEntity> _sosRepository;
        private readonly IGenericRepository<ClientEntity> _clientRepository;
        private readonly IGenericRepository<ProductEntity> _productRepository;
        private readonly IGenericRepository<ProductCategoryEntity> _productCategoryRepository;

        public SOSController(ISosBLL sosBLL,
            IGenericRepository<SOSEntity> sosRepository,
            IGenericRepository<ClientEntity> clientRepository,
            IGenericRepository<ProductEntity> productRepository,
            IGenericRepository<ProductCategoryEntity> productCategoryRepository)
        {
            _sosBLL = sosBLL;
            _sosRepository = sosRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }
        // GET: SOS
        public ActionResult Index()
        {
            var clients = _clientRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.Name).Select(x => new ClientModel { Id = x.Id, Name = x.Name, Telephone_Number = x.Telephone_Number, Email = x.Email, Office_Address = x.Office_Address, Combine_Items = x.Combine_Items }).ToList();

            var clientsMessage = new MessageResult<ClientModel>
            {
                isError = false,
                ResultList = clients,
                Message = "Success",
                Result = null
            };
            var sosList = GetSOSList(false) as JsonResult;

            var sosModel = new SOSViewModel
            {
                SOSList = sosList.Data as MessageResult<SOSModel>,
                ClientList = clientsMessage
            };
            return View(sosModel);
        }

        public ActionResult AddSOS()
        {
            var clients = _clientRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.Name).Select(x => new ClientModel { Id = x.Id, Name = x.Name, Telephone_Number = x.Telephone_Number, Email = x.Email, Office_Address = x.Office_Address, Combine_Items = x.Combine_Items }).ToList();

            var clientsMessage = new MessageResult<ClientModel>
            {
                isError = false,
                ResultList = clients,
                Message = "Success",
                Result = null
            };

            var productEntities = _productRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.ProductCategory.Name).ThenBy(i => i.Name).Select(x => new ProductModel { Id = x.Id, Name = x.Name, ProductCategory = new ProductCategoryModel { Name = x.ProductCategory.Name, Id = x.ProductCategory.Id } }).ToList();

            var productsMessage = new MessageResult<ProductModel>
            {
                isError = false,
                ResultList = productEntities,
                Message = "Success",
                Result = null
            };

            var categories = _productCategoryRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();

            TinyMapper.Bind<List<ProductCategoryEntity>, List<ProductCategoryModel>>();

            var categoriesModel = TinyMapper.Map<List<ProductCategoryModel>>(categories);

            var categoriesMessage = new MessageResult<ProductCategoryModel>
            {
                isError = false,
                ResultList = categoriesModel,
                Message = "Success",
                Result = null
            };

            var sosModel = new SOSViewModel
            {
                ClientList = clientsMessage,
                Products = productsMessage,
                ProductCategories = categoriesMessage
            };

            return View(sosModel);
        }

        public ActionResult ViewSOS(int id)
        {
            var sosDetail = GetSOSDetail(id) as JsonResult;
            var sos = sosDetail.Data as MessageResult<SOSModel>;

            var sosViewModel = new SOSViewModel
                {
                    SingleSOS = sos.Result
                };

                return View(sosViewModel);          
           
        }

        public ActionResult UpdateSOS(int id)
        {


            var clients = _clientRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.Name).Select(x => new ClientModel { Id = x.Id, Name = x.Name, Telephone_Number = x.Telephone_Number, Email = x.Email, Office_Address = x.Office_Address, Combine_Items = x.Combine_Items }).ToList();

            var clientsMessage = new MessageResult<ClientModel>
            {
                isError = false,
                ResultList = clients,
                Message = "Success",
                Result = null
            };
            var productEntities = _productRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.ProductCategory.Name).ThenBy(i => i.Name).Select(x => new ProductModel { Id = x.Id, Name = x.Name, ProductCategory = new ProductCategoryModel { Name = x.ProductCategory.Name, Id = x.ProductCategory.Id } }).ToList();

            var productsMessage = new MessageResult<ProductModel>
            {
                isError = false,
                ResultList = productEntities,
                Message = "Success",
                Result = null
            };
            var categories = _productCategoryRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();

            TinyMapper.Bind<List<ProductCategoryEntity>, List<ProductCategoryModel>>();

            var categoriesModel = TinyMapper.Map<List<ProductCategoryModel>>(categories);

            var categoriesMessage = new MessageResult<ProductCategoryModel>
            {
                isError = false,
                ResultList = categoriesModel,
                Message = "Success",
                Result = null
            };
            var sosDetail = GetSOSDetail(id) as JsonResult;
            var sos = sosDetail.Data as MessageResult<SOSModel>;

            var sosModel = new SOSViewModel
            {
                ClientList = clientsMessage,
                Products = productsMessage,
                ProductCategories = categoriesMessage,
                SingleSOS = sos.Result
            };
            return View(sosModel);
        }

        #region GET METHODS 
        [HttpGet]
        public ActionResult GetSOSList(bool status)
        {
            try
            {
              

                 
                    var sosList = _sosRepository.Get(i => i.Status == status).OrderByDescending(i => i.Id)
                        .Select(i => new SOSModel { Id = i.Id, Status = i.Status, Client = new ClientModel { Name = i.ClientEntity.Name }, Sos_Date = i.Sos_Date, TotalAmount = i.TotalAmount, Orders = i.Orders.Where(x => x.Discarded == true).ToList().Count == 0 ? new List<SOSProductModel>() : null, CustomOrders = i.CustomOrders.Where(x => x.Discarded == true).ToList().Count == 0 ? new List<SOSCustomModel>() : null }).ToList();

                    var message = new MessageResult<SOSModel>
                    {
                        isError = false,
                        ResultList = sosList,
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
        public ActionResult GetSOSListByClient(string name)
        {
            try
            {
             

                    var sosList = new List<SOSModel>();

                    if (String.IsNullOrEmpty(name))
                    {
                         sosList = _sosRepository.Get(i => i.Status == false).OrderByDescending(i => i.Id)
                      .Select(i => new SOSModel { Id = i.Id, Status = i.Status, Client = new ClientModel { Name = i.ClientEntity.Name }, Sos_Date = i.Sos_Date, TotalAmount = i.TotalAmount, Orders = i.Orders.Where(x => x.Discarded == true).ToList().Count == 0 ? new List<SOSProductModel>() : null, CustomOrders = i.CustomOrders.Where(x => x.Discarded == true).ToList().Count == 0 ? new List<SOSCustomModel>() : null }).ToList();
                    }
                    else
                    {
                         sosList = _sosRepository.Get(i => i.ClientEntity.Name == name).OrderByDescending(i => i.Id)
                      .Select(i => new SOSModel { Id = i.Id, Status = i.Status, Client = new ClientModel { Name = i.ClientEntity.Name }, Sos_Date = i.Sos_Date, TotalAmount = i.TotalAmount, Orders = i.Orders.Where(x => x.Discarded == true).ToList().Count == 0 ? new List<SOSProductModel>() : null, CustomOrders = i.CustomOrders.Where(x => x.Discarded == true).ToList().Count == 0 ? new List<SOSCustomModel>() : null }).ToList();
                    }
                  

                    var message = new MessageResult<SOSModel>
                    {
                        isError = false,
                        ResultList = sosList,
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
        public ActionResult GetSOSDetail(int id)
        {
            try
            {
               
                    var sos = _sosRepository.Get(i => i.Id == id)
                        .Select(x => new SOSModel
                        {
                            Id = x.Id,
                            Pickup = x.Pickup,
                            Sos_Date = x.Sos_Date,
                            Client = new ClientModel { Id = x.ClientEntity.Id,Name = x.ClientEntity.Name, Office_Address = x.ClientEntity.Office_Address },
                            Remarks = x.Remarks,
                            Orders = x.Orders
                                    .Select(y => new SOSProductModel
                                    {
                                        Quantity = y.Quantity,
                                        QuantityDelivered = y.QuantityDelivered,
                                        Price = y.Price,
                                        Product = new ProductModel { Id = y.ProductId, Name = y.Product.Name },
                                        Unit = y.Unit,
                                        Id = y.Id,
                                        Discarded = y.Discarded

                                    }).ToList(),
                            CustomOrders = x.CustomOrders
                                    .Select(y => new SOSCustomModel
                                    {
                                        Quantity = y.Quantity,
                                        QuantityDelivered = y.QuantityDelivered,
                                        ItemDescription = y.ItemDescription,
                                        Unit = y.Unit,
                                        Price = y.Price,
                                        Id = y.Id,
                                        Discarded = y.Discarded,
                                        Category = y.Category
                                    }).ToList()
                        }).FirstOrDefault();

                    var message = new MessageResult<SOSModel>
                    {
                        isError = false,
                        ResultList = null,
                        Message = "Success",
                        Result = sos
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
        public ActionResult GetListCustomProducts(int clientId)
        {
            try
            {
               
                    var customProducts = _sosRepository.Get(i => i.ClientId == clientId).SelectMany(i => i.CustomOrders.ToList()).ToList();

                    var customProductsModel = customProducts.Where(x => x.Discarded == false).Select(x => new SOSCustomModel { Category = x.Category, ItemDescription = x.ItemDescription, Price = x.Price, Unit = x.Unit }).ToList();
                    

                    var message = new MessageResult<SOSCustomModel>
                    {
                        isError = false,
                        ResultList = customProductsModel,
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
        #endregion

        #region POST METHODS
        [HttpPost]
        public ActionResult AddSOS(SOSFormModel model)
        {
            try
            {
                _sosBLL.AddSos(model);

                var message = new MessageResult<SOSFormModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "SOS Added Succesfully!",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<SOSFormModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact the administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DiscardOrders(int sosId, List<int> orderIds, List<int> customOrderIds)
        {
            try
            {
                _sosBLL.DiscardOrders(sosId, orderIds, customOrderIds);

                var message = new MessageResult<string>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Orders discarded Succesfully!",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<SOSFormModel>
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

        #region PUT METHODS
        [HttpPut]
        public ActionResult UpdateSOS(SOSFormModel model)
        {
            try
            {
                _sosBLL.UpdateSOS(model);

                var message = new MessageResult<SOSFormModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "SOS Updated Succesfully!",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = new MessageResult<SOSFormModel>
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
    }
}