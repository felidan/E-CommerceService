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

        public ActionResult Index(bool ckPreco = false, bool ckDistancia = false, bool ckAvaliacao = false, string Categorias = "-1")
        {
            List<ServicosGeraisViewModel> servicos = new List<ServicosGeraisViewModel>();
            ServicosServices _service = new ServicosServices();
            RatingService rating = new RatingService();

            ViewBag.Categorias = new SelectList( _service.GetListCategorias(), "IdCategoria", "DsCategoria");

            if(Categorias == "-1")
            {
                servicos = _service.GetListServicos();
            }
            else
            {
                servicos = _service.GetListServicos().Where(x => x.Categoria == Categorias).ToList();
            }

            servicos = rating.CalculaRating(servicos, ckPreco, ckDistancia, ckAvaliacao).OrderByDescending(x => x.RatingServico).ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartionGridServicosGerais", servicos);
            }
            return View(servicos);
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

        public ActionResult NovoServico()
        {
            NovoServicoViewModels model = new NovoServicoViewModels();
            ServicosServices _service = new ServicosServices();
            ViewBag.Categorias = new SelectList(_service.GetListCategorias(), "IdCategoria", "DsCategoria");
            ViewBag.Erro = "-1";
            return View(model);
        }

        public ActionResult CadastrarNovoServico(NovoServicoViewModels model, string Categoria = "-1")
        {
            ServicosServices servico = new ServicosServices();
            string id = Server.HtmlEncode(Request.Cookies["IdUsuario"].Value.ToString());

            if( servico.InsServico(model, id, Categoria) ){
                return RedirectToAction("MyServices", "Servicos");
            }
            else
            {
                ViewBag.Erro = "Erro ao cadastrar serviço.";
                return RedirectToAction("NovoServico");
            }
        }

        public ActionResult ContrataServico(int idServico)
        {
            
            return View("ContrataServico");
        }
    }
}