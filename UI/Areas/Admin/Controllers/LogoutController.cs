using DTO;
using System;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Logout()
        {
            UserStatic.UserID = 0;
            UserStatic.isAdmin = false;
            UserStatic.NameSurname = null;
            UserStatic.ImagePath = null;

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            return RedirectToAction("Index", "Login");
        }
    }
}
