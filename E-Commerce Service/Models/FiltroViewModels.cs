using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.Models
{
    public class FiltroViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string NmUsuario { get; set; }

        [Display(Name = "Email")]
        public string EmailUsuario { get; set; }

        [Display(Name = "Idade")]
        public int IdadeUsuario { get; set; }

        [Display(Name = "Perfil")]
        public string PerfilUsuario { get; set; }
    }
}