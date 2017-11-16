using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using E_Commerce_Service.Models;
using E_Commerce_Service.GlobalServices;

namespace E_Commerce_Service.Controllers
{
    
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UsuarioService _userService = new UsuarioService();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region [login]

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Response.Cookies["userLogado"].Value = "-1";
            Response.Cookies["userName"].Value = "";
            Response.Cookies["userEmail"].Value = "";
            Response.Cookies["userPerfil"].Value = "";
            Response.Cookies["IdUsuario"].Value = "";
            

            if (returnUrl == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            UsuarioModels user = new UsuarioModels();
            UsuarioService service = new UsuarioService();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user = _userService.AltenticaUsuario(model.Email);

            if (user.Existe)
            {
                if (user.SenhaUsuario == model.Password)
                {
                    Response.Cookies["userLogado"].Value = "1";
                    Response.Cookies["userName"].Value = user.NmUsuario.ToString();
                    Response.Cookies["userEmail"].Value = user.EmailUsuario.ToString();
                    Response.Cookies["userPerfil"].Value = user.TpUsuario.ToString();
                    Response.Cookies["IdUsuario"].Value = user.IdUsuario.ToString();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Senha incorreta.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário não encontrado.");
                return View(model);
            }
        }

        public JsonResult LoginModel(string Email, string Senha)
        {
            UsuarioModels user = new UsuarioModels();
            UsuarioService service = new UsuarioService();

            user = _userService.AltenticaUsuario(Email);

            if (user.Existe)
            {
                if (user.SenhaUsuario == Senha)
                {
                    Response.Cookies["userLogado"].Value = "1";
                    Response.Cookies["userName"].Value = user.NmUsuario.ToString();
                    Response.Cookies["userEmail"].Value = user.EmailUsuario.ToString();
                    Response.Cookies["userPerfil"].Value = user.TpUsuario.ToString();
                    Response.Cookies["IdUsuario"].Value = user.IdUsuario.ToString();

                    return Json("OK");
                }
                else
                {
                    ModelState.AddModelError("", "Senha incorreta.");
                    return Json("S");
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário não encontrado.");
                return Json("E");
            }
        }
        #endregion [login]

        #region [Registrar]

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.TipoPefil = new SelectList
                (
                    new TipoPerfilModels().ListaPerfil(),
                    "IdPerfil",
                    "DsPerfil"
                );

            ViewBag.PerfilNaoSelecionado = "0";
            ViewBag.UsuarioExistente = "-1";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, string TipoPefil)
        {
            ViewBag.PerfilNaoSelecionado = "0";
            if (TipoPefil == "-1")
            {
                ViewBag.TipoPefil = new SelectList
                (
                    new TipoPerfilModels().ListaPerfil(),
                    "IdPerfil",
                    "DsPerfil"
                );
                ViewBag.PerfilNaoSelecionado = "-1";
                return View(model);
            }

            ViewBag.UsuarioExistente = "-1";

            if (ModelState.IsValid)
            {
                if (!_userService.ValidaExistenciaUsuario(model))
                {
                    if (_userService.InsUsuario(model, TipoPefil))
                    {
                        int id = _userService.GetUsuarioPorEmail(model.Email);

                        Response.Cookies["userLogado"].Value = "1";
                        Response.Cookies["userName"].Value = model.Nome.ToString();
                        Response.Cookies["userEmail"].Value = model.Email.ToString();
                        Response.Cookies["userPerfil"].Value = TipoPefil.ToString();
                        Response.Cookies["IdUsuario"].Value = id.ToString();

                        return RedirectToAction("Index", "Home");
                    }
                }

                ViewBag.TipoPefil = new SelectList
                (
                    new TipoPerfilModels().ListaPerfil(),
                    "IdPerfil",
                    "DsPerfil"
                );

                ViewBag.UsuarioExistente = "Email já cadastrado.";
                return View(model);
            }

            ViewBag.TipoPefil = new SelectList
                (
                    new TipoPerfilModels().ListaPerfil(),
                    "IdPerfil",
                    "DsPerfil"
                );

            return View(model);
        }

        #endregion [Registrar]

        #region [LogOff]

        public ActionResult LogOff()
        {
            Response.Cookies["userLogado"].Value = "-1";
            Response.Cookies["userName"].Value = "";
            Response.Cookies["userEmail"].Value = "";
            Response.Cookies["userPerfil"].Value = "";
            Response.Cookies["IdUsuario"].Value = "";

            return RedirectToAction("Index", "Home");
        }

        #endregion [LogOff]

        #region Helpers

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
           {
                ModelState.AddModelError("", error);
           }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}