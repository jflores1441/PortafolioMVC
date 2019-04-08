using Model;
using proyecto.Areas.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;

namespace proyecto.Areas.Admin.Controllers
{
    [Autenticado]
    public class ExperienciaController : Controller
    {
        private Experiencia experiencia = new Experiencia();

        // GET: Admin/Experiencia
        public ActionResult Index(int tipo)
        {
            ViewBag.tipo = tipo;
            ViewBag.Title = tipo == 1 ? "Trabajos realizados" : "Estudios previos";
            return View();
        }

        public JsonResult Listar(AnexGRID grid, int tipo)
        {
            return Json(experiencia.Listar(grid, tipo));
        }

        /// <summary>
        /// Controlador que permite registrar una nueva experiencia o actualizarla
        /// </summary>      
        /// <param name="tipo"></param>
        /// <param name="id">Parametro opcional</param>/// 
        /// <returns></returns>
        public ActionResult Crud(byte tipo = 0, int id = 0)
        {
            if (id == 0)
            {
                if (tipo == 0) return Redirect("~/admin/experiencia");
                experiencia.Tipo = tipo;
                experiencia.Usuario_id = SessionHelper.GetUser();
            }
            else
            {
                experiencia = experiencia.Obtener(id);
            }
            return View(experiencia);
        }

        public JsonResult Guardar(Experiencia model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/experiencia/?tipo=" + model.Tipo);
                }
            }

            return Json(rm);
        }

        public JsonResult Eliminar(int id)
        {
            var rm = new ResponseModel();

            rm = experiencia.Eliminar(id);

            if (rm.response)
            {
                rm.href = Url.Content("self");


            }
            return Json(rm);
        }
    }
}