using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.Models
{
    public class ServicosGeraisViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string NmServico { get; set; }

        [Display(Name = "Preço")]
        public decimal PrecoServico { get; set; }

        [Display(Name = "Distância")]
        public float DistanciaServico { get; set; }

        [Display(Name = "Avaliação")]
        public decimal NotaServico { get; set; }

        [Display(Name = "Rating")]
        public decimal RatingServico { get; set; }

        [Display(Name = "Categoria")]
        public string DsCategoria { get; set; }

        public string Categoria { get; set; }
        
    }
}