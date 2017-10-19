using E_Commerce_Service.GlobalServices;
using E_Commerce_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Service.Controllers
{
    public class AdministradorController : Controller
    {

        // GET: Administrador
        public ActionResult Index(string Nome = "", string Email = "")
        {
            List<FiltroViewModels> usuarios = new List<FiltroViewModels>();
            UsuarioService _userService = new UsuarioService();

            usuarios = _userService.GetListUsuario(Nome, Email);
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialGridResultado", usuarios); // passar a lista
            }
            return View(usuarios); // passar a lista
        }
    }
}