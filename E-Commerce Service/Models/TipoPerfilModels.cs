using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.Models
{
    public class TipoPerfilModels
    {
        public int IdPerfil { get; set; }

        public string DsPerfil { get; set; }

        public List<TipoPerfilModels> ListaPerfil()
        {
            return new List<TipoPerfilModels>
            {
                new TipoPerfilModels { IdPerfil = -1, DsPerfil = "Selecione"},
                //new TipoPerfilModels { IdPerfil = 0, DsPerfil = "ADM"},
                new TipoPerfilModels { IdPerfil = 1, DsPerfil = "Quero contratar um serviço"},
                new TipoPerfilModels { IdPerfil = 2, DsPerfil = "Quero divulgar meu serviço"}
            };
        }
    }
}