using IclartWebApp.BLL;
using IclartWebApp.Common.Entities;
using IclartWebApp.Common.Models;
using IclartWebApp.DAL;
using IclartWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IclartWebApp.Controllers
{
    public class CompetitorController : Controller
    {
        // GET: Competitor
        public ActionResult Index()
        {
            var competitorList = GetCompetitors() as JsonResult;

            var competitorViewModel = new CompetitorViewModel
            {
                CompetitorList = competitorList.Data as MessageResult<CompetitorModel>
            };
            return View(competitorViewModel);
        }

        [HttpGet]
        public ActionResult GetCompetitors()
        {
            try
            {
                using (var context = new DBContext())
                {
                    var competitorRepository = new GenericRepository<CompetitorEntity>(context);

                    var competitors = competitorRepository.Get().OrderBy(i => i.Name).Select(i => new CompetitorModel { Id = i.Id, Name = i.Name }).ToList();

                    var message = new MessageResult<CompetitorModel>
                    {
                        isError = false,
                        ResultList = competitors,
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
        [HttpPost]
        public ActionResult AddCompetitor(string name)
        {
            try
            {
                var competitor = new CompetitorModel
                {
                    Name = name,
                    CreatedDate = DateTime.Now
                };
                var competitorBLL = new CompetitorBLL();
                competitorBLL.AddCompetitor(competitor);
                var message = new MessageResult<CompetitorModel>
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
        [HttpPut]
        public ActionResult UpdateCompetitor(int id, string name)
        {
            try
            {
                var competitor = new CompetitorModel
                {
                    Id = id,
                    Name = name
                };
                var competitorBLL = new CompetitorBLL();
                competitorBLL.UpdateCompetitor(competitor);
                var message = new MessageResult<CompetitorModel>
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
        [HttpDelete]
        public ActionResult DeleteCompetitor(int id)
        {
            try
            {

                var competitorBLL = new CompetitorBLL();
                competitorBLL.DeleteCompetitor(id);
                var message = new MessageResult<CompetitorModel>
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
                var message = new MessageResult<ClientModel>
                {
                    isError = true,
                    ResultList = null,
                    Message = "Some error occured. Please contact your Administrator.",
                    Result = null
                };
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}