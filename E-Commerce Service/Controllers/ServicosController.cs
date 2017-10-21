using E_Commerce_Service.GlobalServices;
using E_Commerce_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Service.Controllers
{
    public class ServicosController : Controller
    {
        #region [Serviços]

        public ActionResult Index()
        {
            return View();
        }

        #endregion [Serviços]

        #region [Serviços - Perfil 2: Oferece serviço]

        public ActionResult MyServices(string Nome = "")
        {
            List<FiltroMyServiceViewModel> MyServices = new List<FiltroMyServiceViewModel>();
            ServicosServices service = new ServicosServices();
            
            string email = Server.HtmlEncode(Request.Cookies["userEmail"].Value.ToString());
            MyServices = service.GetListServicosUsuario(Nome, email);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialGridMyServices", MyServices);
            }
            return View(MyServices);
        }

        #endregion [Serviços - Perfil 2: Oferece serviço]
    }
}