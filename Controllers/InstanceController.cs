using Dapper;
using Assist_WebConfig.Data;
using Assist_WebConfig.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using Assist_WebConfig.ViewModels;

namespace Assist_WebConfig.Controllers
{
    public class InstanceController : Controller
    {
        public ActionResult Index()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", 0);

                return View(DapperORM.ReturnList<InstanceModel>("WebGetInstance", param));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");

            try
            {
                ViewBag.ApiAuthId = DapperORM.ReturnList<GenericModel>("WebGetApiAuth");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(InstanceModel instance)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");

            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@InstanceId", instance.InstanceId);
                param.Add("@IPAddress", instance.IPAddressIn);
                param.Add("@PortIn", instance.PortIn);
                param.Add("@ApiAuthId", instance.ApiAuthId);

                await DapperORM.ExecuteWithoutReturnAsync("WebAddOrUpdateInstance", param);

                return RedirectToAction("/index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");            

            try
            {
                var apiType = DapperORM.ReturnList<GenericModel>("WebGetApiAuth").ToList();
                var instanceTag = new InstanceApiTag();                    
                DynamicParameters param = new DynamicParameters();
                
                param.Add("@Id", id);
                var instance = DapperORM.ReturnList<InstanceModel>("WebGetInstance", param).FirstOrDefault();

                if(instance != null && apiType != null)
                {
                    instanceTag.InstanceId = instance.InstanceId;
                    instanceTag.IPAddressIn = instance.IPAddressIn;
                    instanceTag.PortIn = instance.PortIn;
                    instanceTag.ApiType = apiType;
                    instanceTag.Selected = instance.ApiAuthId;
                    
                    return View(instanceTag);
                }

                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(InstanceApiTag instance)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@InstanceId", instance.InstanceId);
                param.Add("@IPAddress", instance.IPAddressIn);
                param.Add("@PortIn", instance.PortIn);
                param.Add("@ApiAuthId", instance.Selected);

                await DapperORM.ExecuteWithoutReturnAsync("WebAddOrUpdateInstance", param);

                return RedirectToAction("/index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                await DapperORM.ExecuteWithoutReturnAsync("WebDeleteInstance", param);
                return RedirectToAction("/index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }
    }
}