using Dapper;
using Assist_WebConfig.Data;
using Assist_WebConfig.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;

namespace Assist_WebConfig.Controllers
{
    public class LogController : Controller
    {
        public ActionResult Activity(string search, int? i)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@FeedbackTypeId", (int)1);

                if (search == null || search.Length < 1)
                    return View(DapperORM.ReturnList<LogModel>("WebGetLog", param).ToPagedList(i ?? 1, 20));

                List<LogModel> logs = DapperORM.ReturnList<LogModel>("WebGetLog", param).ToList();
                var result = logs.Where(x => x.InstanceName.ToUpper().Equals(search.ToUpper())).ToList();

                return View(result.ToPagedList(i ?? 1, 20));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }
        public ActionResult Xmls(string search, int? i)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@FeedbackTypeId", (int)3);

                if (search == null || search.Length < 1)
                    return View(DapperORM.ReturnList<LogXmlModel>("WebGetLog", param).ToPagedList(i ?? 1, 20));

                List<LogXmlModel> xmls = DapperORM.ReturnList<LogXmlModel>("WebGetLog", param).ToList();
                var result = xmls.Where(x => x.LicenseNumber.ToUpper().Equals(search.ToUpper())).ToList();

                return View(result.ToPagedList(i ?? 1, 20));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }
        public ActionResult Errors(string search, int? i)
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");
            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@FeedbackTypeId", (int)2);

                if (search == null || search.Length < 1)
                    return View(DapperORM.ReturnList<LogModel>("WebGetLog", param).ToPagedList(i ?? 1, 20));

                List<LogModel> errors = DapperORM.ReturnList<LogModel>("WebGetLog", param).ToList();
                var result = errors.Where(x => x.InstanceName.ToUpper().Equals(search.ToUpper())).ToList();

                return View(result.ToPagedList(i ?? 1, 20));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error has ocurred: " + ex.Message;
                return View("~/Views/Error/SomethingWentWrong.cshtml");
            }
        }       
    }
}