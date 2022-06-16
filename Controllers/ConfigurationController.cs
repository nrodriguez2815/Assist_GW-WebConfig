using Dapper;
using Assist_WebConfig.Data;
using Assist_WebConfig.Models;
using Assist_WebConfig.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;

namespace Assist_WebConfig.Controllers
{
    public class ConfigurationController : Controller
    {
        public ActionResult Index(int id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                ViewBag.InstanceId = id;

                DynamicParameters param = new DynamicParameters();
                param.Add("@InstanceId", id);

                return View(DapperORM.ReturnList<ConfigurationModel>("WebGetConfiguration", param));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                ViewBag.InstanceId = id;
                ViewBag.LogTypeId = DapperORM.ReturnList<GenericModel>("WebGetLogType");                

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(ConfigurationModel config, int id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");

            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@InstanceId", id);
                param.Add("@VehiclesListCheckTimer", config.VehiclesListCheckTimer);
                param.Add("@XmlsSenderTimer", config.XmlsSenderTimer);
                param.Add("@LogTypeId", config.LogTypeId);
                param.Add("@ActivityLog", config.LogTypeId != 4);
                param.Add("@LogPath", config.LogPath);
                param.Add("@Retries", config.Retries);
                param.Add("@InstanceName", config.InstanceName);

                await DapperORM.ExecuteWithoutReturnAsync("WebAddOrUpdateConfiguration", param);

                return RedirectToAction("/index/" + id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var config = new ConfigurationLogTag();
                ViewBag.InstanceId = id;
                DynamicParameters param = new DynamicParameters();
                param.Add("@InstanceId", id);
                var editConfig = DapperORM.ReturnList<ConfigurationModel>("WebGetConfiguration", param).FirstOrDefault<ConfigurationModel>();
                var logType = DapperORM.ReturnList<GenericModel>("WebGetLogType").ToList();

                if (editConfig != null && logType != null)
                {
                    config.InstanceId = id;
                    config.VehiclesListCheckTimer = editConfig.VehiclesListCheckTimer;
                    config.XmlsSenderTimer = editConfig.XmlsSenderTimer;
                    config.LogType = logType;
                    config.Selected = editConfig.LogTypeId;
                    config.ActivityLog = editConfig.LogTypeId != 4;
                    config.LogPath = editConfig.LogPath;
                    config.Retries = editConfig.Retries;
                    config.InstanceName = editConfig.InstanceName;
                }

                return View(config);
            }
            catch(Exception ex) 
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ConfigurationLogTag config, int id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");

            if (config.Selected == 1 || config.Selected == 4)
                config.LogPath = null;

            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@InstanceId", id);
                param.Add("@VehiclesListCheckTimer", config.VehiclesListCheckTimer);
                param.Add("@XmlsSenderTimer", config.XmlsSenderTimer);
                param.Add("@LogTypeId", config.Selected);
                param.Add("@ActivityLog", config.Selected != 4);
                param.Add("@LogPath", config.LogPath);
                param.Add("@Retries", config.Retries);
                param.Add("@InstanceName", config.InstanceName);

                await DapperORM.ExecuteWithoutReturnAsync("WebAddOrUpdateConfiguration", param);

                return RedirectToAction("/index/" + id);
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
                param.Add("@InstanceId", id);

                await DapperORM.ExecuteWithoutReturnAsync("WebDeleteConfiguration", param);
                return RedirectToAction("/index/" + id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }
    }
}