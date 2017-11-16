using E_Commerce_Service.GlobalServices;
using E_Commerce_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Service.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Quem somos?";
            
            return View();
        }

        public JsonResult LoadRanking()
        {
            ServicosServices servico = new ServicosServices();
            var retorno = servico.CarregaRanking();
            return Json(retorno);
        }
    }
}