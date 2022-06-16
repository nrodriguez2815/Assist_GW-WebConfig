using Dapper;
using Assist_WebConfig.Data;
using Assist_WebConfig.Helpers;
using Assist_WebConfig.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Assist_WebConfig.ViewModels;

namespace Assist_WebConfig.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            if (Session["username"] == null || !Session["userType"].ToString().Equals("Admin"))
                return RedirectToAction("../Login.aspx");

            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", (int)0);

            try
            {
                return View(DapperORM.ReturnList<UserModel>("WebGetUsers", param));
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
            if (Session["username"] == null || !Session["userType"].ToString().Equals("Admin"))
                return RedirectToAction("../Login.aspx");

            try
            {
                ViewBag.UserType = DapperORM.ReturnList<GenericModel>("WebGetUserType");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }            
        }

        [HttpPost]
        public async Task<ActionResult> Add(UserModel user)
        {
            if (Session["username"] == null || !Session["userType"].ToString().Equals("Admin"))
                return RedirectToAction("../Login.aspx");

            DynamicParameters param = new DynamicParameters();
            string passEncrypted = Encrypt.GetSHA256(user.Password.Trim());

            param.Add("@UserId", (int)0);
            param.Add("@Email", user.Email);
            param.Add("@Password", passEncrypted);
            param.Add("@UserTypeId", user.UserTypeId);
            param.Add("@Token_Recovery", null);

            try
            {
                await DapperORM.ExecuteWithoutReturnAsync("WebAddOrEditUser", param);

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
            if (Session["username"] == null || !Session["userType"].ToString().Equals("Admin"))
                return RedirectToAction("../Login.aspx");

            try
            {
                var userTypes = DapperORM.ReturnList<GenericModel>("WebGetUserType").ToList();
                DynamicParameters param = new DynamicParameters();
                var userTag = new UserTypeTag();

                param.Add("@UserId", id);
                var user = DapperORM.ReturnList<UserModel>("WebGetUsers", param).FirstOrDefault();

                if(userTypes != null && user != null)
                {
                    userTag.UserId = user.UserId;
                    userTag.Email = user.Email;
                    userTag.UserType = userTypes;
                    userTag.Selected = user.UserTypeId;
                }

                return View(userTag);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserTypeTag user)
        {
            if (Session["username"] == null || !Session["userType"].ToString().Equals("Admin"))
                return RedirectToAction("../Login.aspx");

            DynamicParameters param = new DynamicParameters();

            param.Add("@UserId", user.UserId);
            param.Add("@Email", user.Email);
            param.Add("@Password", null);
            param.Add("@UserTypeId", user.Selected);
            param.Add("@Token_Recovery", null);

            try
            {
                await DapperORM.ExecuteWithoutReturnAsync("WebAddOrEditUser", param);

                if ((int)Session["userId"] == user.UserId)
                {
                    Session.Abandon();
                    return RedirectToAction("../Login.aspx");
                }

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
            if (Session["username"] == null || !Session["userType"].ToString().Equals("Admin"))
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", id);
                await DapperORM.ExecuteWithoutReturnAsync("WebDeleteUser", param);

                if (Session["userId"].ToString().Equals(id.ToString()))
                {
                    Session.Abandon();

                    return RedirectToAction("../Login.aspx");
                }
                return RedirectToAction("/index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpGet]
        public ActionResult StartRecovery()
        {
            RecoveryModel model = new RecoveryModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> StartRecovery(RecoveryModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string token = Encrypt.GetSHA256(Guid.NewGuid().ToString());

                DynamicParameters param = new DynamicParameters();
                param.Add("@email", model.Email);
                param.Add("@pass", "Recovery");

                var oUser = DapperORM.ReturnList<UserModel>("WebGetAuthUser", param).FirstOrDefault();

                param = new DynamicParameters();
                param.Add("@email", model.Email);
                param.Add("@token", token);

                if (oUser != null)
                {
                    await DapperORM.ExecuteWithoutReturnAsync("WebAddRecoveryToken", param);

                    Sender.SendEmail(oUser.Email, token);
                }

                return RedirectToAction("../Login.aspx");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Recovery(string token)
        {
            var model = new RecoveryPasswordModel();
            model.Token = token;

            if (model.Token == null || model.Token.Trim().Equals(""))
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@token", token);

                var oUser = DapperORM.ReturnList<UserModel>("WebGetUserByToken", param).FirstOrDefault();

                if (oUser == null)
                {
                    ViewBag.Error = "Expired token";
                    return RedirectToAction("../Login.aspx");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Recovery(RecoveryPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                
                try
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@token", model.Token);

                    var oUser = DapperORM.ReturnList<UserModel>("WebGetUserByToken", param).FirstOrDefault();

                    if (oUser != null)
                    {
                        param = new DynamicParameters();

                        param.Add("@UserId", oUser.UserId);
                        param.Add("@Email", oUser.Email);
                        param.Add("@Password", Encrypt.GetSHA256(model.Password.Trim()));
                        param.Add("@UserTypeId", oUser.UserTypeId);
                        param.Add("@Token_Recovery", oUser.Token_Recovery);

                        await DapperORM.ExecuteWithoutReturnAsync("WebAddOrEditUser", param);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                    return View("~/Views/Error/SomethingWentWrong.cshtml");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error has ocurred: " + ex.ToString();
                return RedirectToAction("../Login.aspx");
            }

            ViewBag.Message = "Password changed successfully";
            return RedirectToAction("../Login.aspx");
        }
    }
}