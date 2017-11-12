using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Service.Models
{
    public class NovoServicoViewModels
    {
        public NovoServicoViewModels()
        {

        }

        [Required]
        [Display(Name = "Nome do serviço")]
        public string NomeServico { get; set; }

        [Required]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Informe o valor do seriço")]
        public double Preco { get; set; }

        [Required]
        [Display(Name = "Distância")]
        public double Distancia { get; set; }
        
    }
}