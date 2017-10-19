using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.Models
{
    public class UsuarioModels
    {
        public int IdUsuario { get; set; }
        public string NmUsuario { get; set; }
        public string MnSobrenome { get; set; }
        public string EmailUsuario { get; set; }
        public int IdadeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string TpUsuario { get; set; }
        public bool Existe { get; set; }
    }
}