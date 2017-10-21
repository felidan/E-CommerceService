using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.Models
{
    public class FiltroMyServiceViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        public string CategoriaServico { get; set; }

        [Display(Name = "Descrição")]
        public string DsServico { get; set; }

        [Display(Name = "Plano de propaganda")]
        public string FlPropaganda { get; set; }

        [Display(Name = "Preço")]
        public decimal VlServico { get; set; }
        
        [Display(Name = "Nota do serviço")]
        public decimal NotaServico { get; set; }
    }
}