using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Helper;
using proyecto.Areas.Admin.Filters;

namespace proyecto.Areas.Admin.Controllers
{

    public class LoginController : Controller
    {
        Usuario usuario = new Usuario();

        [NoLogin]
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Acceder(string Email, string Password)
        {
            var rm = usuario.Acceder(Email, Password);

            if (rm.response)
            {
                rm.href = Url.Content("~/admin/usuario");
            }

            return Json(rm);
        }

        public ActionResult Logout()
        {
            // Eliminar la sesion actual
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }
    }
}