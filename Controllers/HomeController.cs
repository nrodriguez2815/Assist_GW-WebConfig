using System.Web.Mvc;

namespace Assist_WebConfig.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("../Login.aspx");
        }
        public ActionResult Index()
        {
            if (Session["username"] == null)
                return RedirectToAction("../Login.aspx");

            return View();
        }
    }
}