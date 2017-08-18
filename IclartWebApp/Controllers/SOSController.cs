using IclartWebApp.BLL;
using IclartWebApp.Common.Entities;
using IclartWebApp.Common.Models;
using IclartWebApp.DAL;
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
        // GET: SOS
        public ActionResult Index()
        {
            var sosList = GetSOSList() as JsonResult;

            var sosModel = new SOSViewModel
            {
                SOSList = sosList.Data as MessageResult<SOSModel>
            };
            return View(sosModel);
        }

        public ActionResult AddSOS()
        {
            var clientController = new ClientController();
            var productController = new ProductController();

            var clientList = clientController.GetClientList() as JsonResult;
            var products = productController.GetProducts() as JsonResult;
            var productCategories = productController.GetProductCategories() as JsonResult;

            var sosModel = new SOSViewModel
            {
                ClientList = clientList.Data as MessageResult<ClientModel>,
                Products = products.Data as MessageResult<ProductModel>,
                ProductCategories = productCategories.Data as MessageResult<ProductCategoryModel>
            };

            return View(sosModel);
        }

        public ActionResult ViewSOS(int id)
        {
            using (var context = new DBContext())
            {
                var sosRepository = new GenericRepository<SOSEntity>(context);

                var sos = sosRepository.Get(i => i.Id == id)
                    .Select(x => new SOSModel
                    {
                        Id = x.Id,
                        Pickup = x.Pickup,
                        Sos_Date = x.Sos_Date,
                        Client = new ClientModel { Name = x.ClientEntity.Name, Office_Address = x.ClientEntity.Office_Address},
                        Remarks = x.Remarks,
                        Orders = x.Orders.Where(y => y.Discarded == false)
                                .Select(y => new SOSProductModel {
                                     Quantity = y.Quantity,
                                     QuantityDelivered = y.QuantityDelivered,
                                     Price = y.Price,
                                     Product = new ProductModel { Name = y.Product.Name },
                                     Unit = y.Unit,
                                     Id = y.Id
                                    
                                }).ToList(),
                        CustomOrders = x.CustomOrders.Where(y => y.Discarded == false)
                                .Select(y => new SOSCustomModel
                                {
                                    Quantity = y.Quantity,
                                    QuantityDelivered = y.QuantityDelivered,
                                    ItemDescription = y.ItemDescription,
                                    Unit = y.Unit,
                                    Price = y.Price,
                                    Id = y.Id
                                }).ToList()
                    }).FirstOrDefault();

                var sosViewModel = new SOSViewModel
                {
                    SingleSOS = sos
                };

                return View(sosViewModel);
            }
           
        }

        #region GET METHODS 
        [HttpGet]
        public ActionResult GetSOSList()
        {
            try
            {
                using (var context = new DBContext())
                {
                    var sosRepository = new GenericRepository<SOSEntity>(context);

                 
                    var sosList = sosRepository.Get().OrderBy(i => i.Status).ThenByDescending(i => i.Sos_Date).Select(i => new SOSModel { Id = i.Id, Status = i.Status, Client = new ClientModel { Name = i.ClientEntity.Name }, Sos_Date = i.Sos_Date, TotalAmount = i.TotalAmount }).ToList();

                    var message = new MessageResult<SOSModel>
                    {
                        isError = false,
                        ResultList = sosList,
                        Message = "Success",
                        Result = null
                    };
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
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
                using (var context = new DBContext())
                {
                    var sosRepository = new GenericRepository<SOSEntity>(context);

                    var customProducts = sosRepository.Get(i => i.ClientId == clientId).SelectMany(i => i.CustomOrders.ToList()).ToList();

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
                var sosBLL = new SosBLL();
                sosBLL.AddSos(model);

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
        #endregion
    }
}