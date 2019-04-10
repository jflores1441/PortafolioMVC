using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Helper;
using proyecto.App_Start;
using proyecto.ViewModels;
using System.Net.Mail;
using Rotativa.MVC;

namespace proyecto.Controllers
{
    public class DefaultController : Controller
    {

        private Usuario usuario = new Usuario();

        public ActionResult Index()
        {
            return View(usuario.ObtenerUsuario(FrontOfficeStartUp.UsuarioVisualizando(), true));
        }

        public JsonResult EnviarCorreo(ContactoViewModel model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                try
                {
                    var _usuario = usuario.ObtenerUsuario(FrontOfficeStartUp.UsuarioVisualizando());
                    var mail = new MailMessage();
                    mail.From = new MailAddress(model.Correo);//Ojo! solo está aceptando correos de outlook 
                    mail.To.Add(_usuario.Email);
                    mail.Subject = "Correo desde Pagina Web";
                    mail.IsBodyHtml = true;
                    mail.Body = model.Mensaje;

                    var SmtpServer = new SmtpClient("smtp.live.com")
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("j.f.h.2014@outlook.com", "Contra2014"),
                        EnableSsl = true
                    }; //or smpt.gmail.com

                    SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    rm.SetResponse(false,ex.Message );
                    return Json(rm);
                    throw;
                }

                rm.SetResponse(true);
                rm.function = "cerrarContacto();";
            }

            return Json(rm);
        }

        public ActionResult ExportaAPDF()
        {
            return new ActionAsPdf("PDF");
        }

        public ActionResult PDF()
        {
            return View(usuario.ObtenerUsuario(FrontOfficeStartUp.UsuarioVisualizando(),true));

        }

    }
}