using proyecto.Areas.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Helper;

namespace proyecto.Areas.Admin.Controllers
{
    [Autenticado]
    public class UsuarioController : Controller
    {
        private Usuario usuario = new Usuario();
        private TablaDato dato = new TablaDato();

        public ActionResult Index()
        {
            ViewBag.Paises = dato.Listar("pais");

            return View(usuario.ObtenerUsuario(SessionHelper.GetUser()));
        }

        //El parametro Foto debe coincidir con el nombre que tiene en la vista del input file
        //Usaremos el HttpPostedFileBase para procesar la imagen desde el Modelo, el Modelo es lo suficientemente capaz para procesarlo
        public JsonResult Guardar(Usuario model, HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            ModelState.Remove("Password"); //Remover la validación del campo password, para que no sea validado a nivel controlador

            if (ModelState.IsValid)
            {
                rm = model.Guardar(Foto);
            }

            return Json(rm);
        }
    }
}