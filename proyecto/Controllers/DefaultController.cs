﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Helper;
using proyecto.App_Start;

namespace proyecto.Controllers
{
    public class DefaultController : Controller
    {

        private Usuario usuario = new Usuario();

        public ActionResult Index()
        {           
            return View(usuario.ObtenerUsuario(FrontOfficeStartUp.UsuarioVisualizando(),true));
        }

    }
}