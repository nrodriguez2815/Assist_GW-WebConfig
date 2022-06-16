using Dapper;
using Assist_WebConfig.Data;
using Assist_WebConfig.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;

namespace Assist_WebConfig.Controllers
{
    public class AccountController : Controller
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

                return View(DapperORM.ReturnList<AccountModel>("WebGetAccount", param));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");

            ViewBag.InstanceId = id;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AccountModel acc)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");

            if (!ModelState.IsValid)
                return RedirectToAction("/index/" + acc.InstanceId);
            
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@InstanceId", acc.InstanceId);
                param.Add("@AccountId", acc.AccountId);

                await DapperORM.ExecuteWithoutReturnAsync("WebAddAccount", param);

                return RedirectToAction("/index/" + acc.InstanceId);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        public async Task<ActionResult> Delete(int instanceId, int accountId)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@InstanceId", instanceId);
                param.Add("@AccountId", accountId);

                await DapperORM.ExecuteWithoutReturnAsync("WebDeleteAccount", param);

                return RedirectToAction("../Account/index/" + instanceId);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpPost]
        public JsonResult CheckAccount(int instanceId, int accountId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@InstanceId", instanceId);
            string query = "SELECT AccountId FROM AccountsDetails WHERE AccountId = " + accountId;

            var SearchDbARM = DapperORM.ReturnList<AccountModel>("WebGetAccount", param).ToList();
            var SearchDataLocal = SearchDbARM.FirstOrDefault(x => x.AccountId == accountId);
            var SearchDbClientF = DapperORM.ReturnList<int>("dbClientF", query).FirstOrDefault();

            if (SearchDbClientF == 0)
                return Json(1);

            if (SearchDataLocal != null)
                return Json(2);

            return Json(0);
        }
    }
}