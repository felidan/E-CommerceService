using E_Commerce_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.GlobalServices
{
    public class RatingService
    {
        private List<ServicosGeraisViewModel> _ListRating;

        public RatingService()
        {
            _ListRating = new List<ServicosGeraisViewModel>();
        }

        public List<ServicosGeraisViewModel> CalculaRating(List<ServicosGeraisViewModel> list, 
                                                            bool ckPreco, 
                                                            bool ckDistancia, 
                                                            bool ckAvaliacao)
        {
            decimal _pesoPreco = 1;
            decimal _pesoDistancia = 1;
            decimal _pesoAvaliacao = 1;
            
            decimal maxDistancia = 0;
            decimal maxPreco = 0;
            decimal maxNota = 0;

            int qtPeso = QuantidadePeso(ckPreco, ckDistancia, ckAvaliacao);

            if (qtPeso == 1)
            {
                if (ckAvaliacao)
                    _pesoAvaliacao = 1.5M;
                else
                    _pesoAvaliacao = 0.75M;
                if (ckDistancia)
                    _pesoDistancia = 1.5M;
                else
                    _pesoDistancia = 0.75M;
                if (ckPreco)
                    _pesoPreco = 1.5M;
                else
                    _pesoPreco = 0.75M;
            }
            else if(qtPeso == 2)
            {
                if (ckAvaliacao)
                    _pesoAvaliacao = 1.1M;
                else
                    _pesoAvaliacao = 0.8M;
                if (ckDistancia)
                    _pesoDistancia = 1.1M;
                else
                    _pesoDistancia = 0.8M;
                if (ckPreco)
                    _pesoPreco = 1.1M;
                else
                    _pesoPreco = 0.8M;
            }


            foreach (ServicosGeraisViewModel item in list)
            {
                if (item.DistanciaServico > maxDistancia)
                {
                    maxDistancia = item.DistanciaServico;
                }
                if (item.PrecoServico > maxPreco)
                {
                    maxPreco = item.PrecoServico;
                }
                if (item.NotaServico > maxNota)
                {
                    maxNota = item.NotaServico;
                }
            }

            foreach (ServicosGeraisViewModel servico in list)
            {
                var _service = new ServicosGeraisViewModel();
                _service = servico;
                _service.RatingServico = CalculaMetrica(servico, maxDistancia, _pesoDistancia, maxPreco, _pesoPreco, maxNota, _pesoAvaliacao);
                _ListRating.Add(_service);
            }
            return _ListRating;
        }
        

        private decimal CalculaMetrica(ServicosGeraisViewModel item, 
                                                             decimal maxDistancia,
                                                             decimal pesoDist, 
                                                             decimal maxPreco, 
                                                             decimal pesoPreco,
                                                             decimal maxNota,
                                                             decimal pesoNota)
        {
            decimal Rating = 0;
            decimal distancia = 0;
            decimal preco = 0;
            decimal nota = 0;

            distancia = item.DistanciaServico * 5 / maxDistancia;
            preco = item.PrecoServico * 5 / maxPreco;
            nota = item.NotaServico * 5 / maxNota;

            distancia = 5 - distancia;
            preco = 5 - preco;

            Rating = ((pesoDist * distancia) + (pesoNota * nota) + (pesoPreco * preco)) / 3;

            return Rating;
        }

        private int QuantidadePeso(bool ckPreco, bool ckDistancia, bool ckAvaliacao)
        {
            int count = 0;

            if (ckPreco)
                count++;
            if (ckDistancia)
                count++;
            if (ckAvaliacao)
                count++;
            
            return count;
        }
    }
}