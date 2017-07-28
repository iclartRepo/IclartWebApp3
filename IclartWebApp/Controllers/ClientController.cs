using IclartWebApp.BLL;
using IclartWebApp.Common.Entities;
using IclartWebApp.Common.Models;
using IclartWebApp.DAL;
using IclartWebApp.Models;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IclartWebApp.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            var clients = GetClientList() as JsonResult;
            
            var clientModel = new ClientViewModel
            {
                ClientList = clients.Data as MessageResult<ClientModel>
            };
            return View(clientModel);
        }

        public ActionResult AddClient()
        {
            return View();
        }
        public ActionResult ViewClient(int id)
        {
            var client = GetClientInfo(id) as JsonResult;

            var clientModel = new ClientViewModel
            {
                ClientList = null,
                ClientInfo = client.Data as MessageResult<ClientModel>
            };

            return View(clientModel);
        }
        public ActionResult UpdateClient(int id)
        {
            var client = GetClientInfo(id) as JsonResult;

            var clientModel = new ClientViewModel
            {
                ClientList = null,
                ClientInfo = client.Data as MessageResult<ClientModel>
            };
            return View(clientModel);
        }
        /// <summary>
        /// Get List of All Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetClientList()
        {
            try
            {
                using (var context = new DBContext())
                {
                    var clientRepository = new GenericRepository<ClientEntity>(context);
                    var clients = clientRepository.Get(i => i.IsDeleted == false).OrderBy(i => i.Name).Select(x => new ClientModel { Id = x.Id, Name = x.Name, Telephone_Number = x.Telephone_Number, Email = x.Email, Office_Address = x.Office_Address, Combine_Items = x.Combine_Items }).ToList();

                    var message = new MessageResult<ClientModel>
                    {
                        isError = false,
                        ResultList = clients,
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
        /// <summary>
        /// Search for Clients
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchClient(string clientName)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var clientRepository = new GenericRepository<ClientEntity>(context);


                    var clients = clientRepository.Get(i => i.IsDeleted == false && i.Name.Contains(clientName)).Select(x => new ClientModel { Id = x.Id, Name = x.Name, Telephone_Number = x.Telephone_Number, Email = x.Email }).ToList();



                    var message = new MessageResult<ClientModel>
                    {
                        isError = false,
                        ResultList = clients,
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
        /// <summary>
        /// Get single client from database based on Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetClientInfo(int id)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var clientRepository = new GenericRepository<ClientEntity>(context);

                    var client = clientRepository.Get(y => y.Id == id).FirstOrDefault();

                    TinyMapper.Bind<ClientEntity, ClientModel>();
                    var clientModel = TinyMapper.Map<ClientModel>(client);

                    var message = new MessageResult<ClientModel>
                    {
                        isError = false,
                        ResultList = null,
                        Message = "Success",
                        Result = clientModel
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
        /// <summary>
        /// Add Client to Database
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddClient(ClientFormModel client)
        {
            try
            {
                var clientBll = new ClientBLL();
                clientBll.AddClient(client);
                var message = new MessageResult<ClientModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Client added successfully!",
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
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Update Client in DB
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult UpdateClient(ClientFormModel client)
        {
            try
            {
                var clientBll = new ClientBLL();
                clientBll.UpdateClient(client);
                var message = new MessageResult<ClientModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Client updated successfully!",
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
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Soft Delete a client using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                var clientBll = new ClientBLL();
                clientBll.DeleteClient(id);
                var message = new MessageResult<ClientModel>
                {
                    isError = false,
                    ResultList = null,
                    Message = "Client updated successfully!",
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
                    Message = ex.Message,
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}