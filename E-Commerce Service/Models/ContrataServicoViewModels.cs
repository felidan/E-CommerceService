using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.Models
{
    public class ContrataServicoViewModels
    {
        public ContrataServicoViewModels()
        {
            TipoServico = new ListCategoria();
            Usuario = new FiltroViewModels();
        }

        #region [Servico]

        public int IdServico { get; set; }

        public ListCategoria TipoServico { get; set; }

        public string NmServico { get; set; }

        public FiltroViewModels Usuario { get; set; }

        public decimal VlServico { get; set; }

        public decimal NotaServico { get; set; }

        public decimal Distancia { get; set; }
        
        #endregion [Servico]

    }
}