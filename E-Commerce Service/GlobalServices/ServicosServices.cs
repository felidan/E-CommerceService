using E_Commerce_Service.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.GlobalServices
{
    public class ServicosServices
    {
        public ServicosServices()
        {

        }

        public List<FiltroMyServiceViewModel> GetListServicosUsuario(string NmServico, string EmailUsuario)
        {
            #region [Declarações]

            HSSFWorkbook plan = new HSSFWorkbook();
            BDService server = new BDService();
            List<FiltroMyServiceViewModel> lista = new List<FiltroMyServiceViewModel>();
            
            ISheet sheet = null;
            IRow row = null;
            ICell cellIdUsuario = null;
            ICell cellNmServico = null;
            ICell cellIdServico = null;
            ICell cellDsCaegoria = null;
            ICell cellDsServico = null;
            ICell cellFlPropaganda = null;
            ICell cellVlServico = null;
            ICell cellNota = null;
            int IdUsuario = 0;
            UsuarioService user = new UsuarioService();
            UsuarioModels userModel = new UsuarioModels();
            int _id = 0;
            string _nome = "";

            #endregion [Declarações]

            #region [GetUsuario]

            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "USUARIO");

            userModel.EmailUsuario = EmailUsuario; 
            userModel = user.GetUsuario(sheet, userModel);

            if (userModel.Existe)
            {
                IdUsuario = userModel.IdUsuario;
            }

            server.BDClose(plan);

            #endregion [GetUsuario]

            plan = null;
            sheet = null;
            
            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "SERVICOS");

            row = sheet.GetRow(1);

            cellNmServico = row.GetCell(2);
            cellIdUsuario = row.GetCell(3);

            for(int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                row = sheet.GetRow(linha);
                
                if(row != null)
                {
                    cellNmServico = row.GetCell(2);
                    cellIdUsuario = row.GetCell(3);

                    _id = Int32.Parse(cellIdUsuario.ToString());
                    _nome = cellNmServico.ToString();

                    if (NmServico.ToString().Trim() == "")
                    {
                        if (_id == IdUsuario && NmServico == "")
                        {
                            FiltroMyServiceViewModel _listaAux = new FiltroMyServiceViewModel();
                            cellIdServico = row.GetCell(0);
                            cellDsCaegoria = row.GetCell(1);
                            cellDsServico = row.GetCell(2);
                            cellFlPropaganda = row.GetCell(5);
                            cellVlServico = row.GetCell(9);
                            cellNota = row.GetCell(10);

                            _listaAux.Id = Int32.Parse(cellIdServico.ToString());
                            _listaAux.CategoriaServico = cellDsCaegoria.ToString();
                            _listaAux.DsServico = cellDsServico.ToString().ToUpper();

                            if (cellFlPropaganda.ToString() == "N")
                            {
                                _listaAux.FlPropaganda = "Não";
                            }
                            else
                            {
                                _listaAux.FlPropaganda = "Sim";
                            }

                            _listaAux.VlServico = decimal.Parse(cellVlServico.ToString());
                            _listaAux.NotaServico = decimal.Parse(cellNota.ToString());
                            lista.Add(_listaAux);
                        }
                    }
                    else
                    {
                        if (_id == IdUsuario && _nome.ToLower() == NmServico.ToLower())
                        {
                            FiltroMyServiceViewModel _listaAux = new FiltroMyServiceViewModel();

                            cellIdServico = row.GetCell(0);
                            cellDsCaegoria = row.GetCell(1);
                            cellDsServico = row.GetCell(2);
                            cellFlPropaganda = row.GetCell(5);
                            cellVlServico = row.GetCell(9);
                            cellNota = row.GetCell(10);

                            _listaAux.Id = Int32.Parse(cellIdServico.ToString());
                            _listaAux.CategoriaServico = cellDsCaegoria.ToString();
                            _listaAux.DsServico = cellDsServico.ToString().ToUpper();

                            if(cellFlPropaganda.ToString() == "N")
                            {
                                _listaAux.FlPropaganda = "Não";
                            }
                            else
                            {
                                _listaAux.FlPropaganda = "Sim";
                            }

                            _listaAux.VlServico = decimal.Parse(cellVlServico.ToString());
                            _listaAux.NotaServico = decimal.Parse(cellNota.ToString());
                            lista.Add(_listaAux);
                        }
                    }
                    
                }
            }
            server.BDClose(plan);

            return lista;
        }

        public List<ListCategoria> GetListCategorias()
        {
            HSSFWorkbook plan = new HSSFWorkbook();
            ISheet sheet = null;
            IRow row = null;
            BDService server = new BDService();
            List<ListCategoria> lista = new List<ListCategoria>();
            ListCategoria _listCateg = new ListCategoria();
            ICell Id = null;
            ICell DsCategoria = null;

            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "TP_SERVICO");

            row = sheet.GetRow(1);

            _listCateg.IdCategoria = -1;
            _listCateg.DsCategoria = "Selecione";

            lista.Add(_listCateg);

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                row = sheet.GetRow(linha);

                if (row != null)
                {
                    _listCateg = new ListCategoria();

                    Id = row.GetCell(0);
                    DsCategoria = row.GetCell(1);

                    _listCateg.IdCategoria = Int32.Parse(Id.ToString());
                    _listCateg.DsCategoria = DsCategoria.ToString();

                    lista.Add(_listCateg);
                }
                
            }

            server.BDClose(plan);

            return lista;
        }

        private string GetListCategoriasPorId(int IdServico)
        {
            HSSFWorkbook plan = new HSSFWorkbook();
            ISheet sheet = null;
            IRow row = null;
            BDService server = new BDService();
            List<ListCategoria> lista = new List<ListCategoria>();
            ListCategoria _listCateg = new ListCategoria();
            ICell Id = null;
            ICell DsCategoria = null;

            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "TP_SERVICO");

            row = sheet.GetRow(1);


            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                row = sheet.GetRow(linha);

                if (row != null)
                {
                    Id = row.GetCell(0);
                    DsCategoria = row.GetCell(1);

                    if(Id.ToString() == IdServico.ToString())
                    {
                        server.BDClose(plan);
                        return DsCategoria.ToString();
                    }
                }
            }

            server.BDClose(plan);

            return null;
        }

        public List<ServicosGeraisViewModel> GetListServicos()
        {
            List<ServicosGeraisViewModel> lista = new List<ServicosGeraisViewModel>();
            HSSFWorkbook plan = new HSSFWorkbook();
            ISheet sheet = null;
            IRow row = null;
            ICell IdServico = null;
            ICell NmServico = null;
            ICell VlServico = null;
            ICell DistanciaServico = null;
            ICell NotaServico = null;
            ICell Categoria = null;

            BDService server = new BDService();
            ServicosGeraisViewModel _list = new ServicosGeraisViewModel();

            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "SERVICOS");

            row = sheet.GetRow(1);
            
            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                row = sheet.GetRow(linha);

                if (row != null)
                {
                    _list = new ServicosGeraisViewModel();

                    IdServico = row.GetCell(0);
                    NmServico = row.GetCell(2);
                    VlServico = row.GetCell(9);
                    DistanciaServico = row.GetCell(11);
                    NotaServico = row.GetCell(10);
                    Categoria = row.GetCell(1);

                    _list.Id = Int32.Parse(IdServico.ToString());
                    _list.NmServico = NmServico.ToString();
                    _list.PrecoServico = Decimal.Parse(VlServico.ToString());
                    _list.DistanciaServico = Decimal.Parse(DistanciaServico.ToString());
                    _list.NotaServico = Decimal.Parse(NotaServico.ToString());
                    _list.Categoria = Categoria.ToString();
                    var tpServico = GetListCategoriasPorId(Int32.Parse(Categoria.ToString()));
                    if (tpServico != null)
                    {
                        _list.DsCategoria = tpServico;
                    }
                    lista.Add(_list);

                }
            }
            server.BDClose(plan);

            return lista;
        }
        
        public bool InsServico(NovoServicoViewModels model, string IdUsuario, string categoria)
        {
            HSSFWorkbook work = new HSSFWorkbook();
            BDService bd = new BDService();
            ISheet sheet = null;
            IRow row = null;
            ICell cellId = null;
            int linha = 0;
            int _id = -1;

            work = bd.BDInit();
            sheet = bd.BDGetFolha(work, "SERVICOS");

            for (linha = 1; linha <= sheet.LastRowNum; linha++) ;

            row = sheet.GetRow(linha - 1);
            cellId = row.GetCell(0);

            Int32.TryParse(cellId.ToString(), out _id);

            if (_id == -1)
            {
                throw new Exception("Erro ao inserir serviço.");
            }
            _id++;

            row = sheet.CreateRow(linha);

            row.CreateCell(0).SetCellValue(_id.ToString());
            row.CreateCell(1).SetCellValue(categoria.Trim());
            row.CreateCell(2).SetCellValue(model.NomeServico.Trim());
            row.CreateCell(3).SetCellValue(IdUsuario.ToString().Trim());
            row.CreateCell(4).SetCellValue(0);
            row.CreateCell(5).SetCellValue("N");
            row.CreateCell(8).SetCellValue(0);
            row.CreateCell(9).SetCellValue(model.Preco);
            row.CreateCell(10).SetCellValue(5);
            row.CreateCell(11).SetCellValue(model.Distancia);

            if (bd.BDSave(work))
            {
                bd.BDClose(work);
                return true;
            }
            bd.BDClose(work);
            return false;
        }

        public List<ServicosGeraisViewModel> CarregaRanking()
        {
            List<ServicosGeraisViewModel> ranking = new List<ServicosGeraisViewModel>();
            ServicosGeraisViewModel _list = new ServicosGeraisViewModel();

            HSSFWorkbook plan = new HSSFWorkbook();
            ISheet sheet = null;
            IRow row = null;
            BDService server = new BDService();
            
            ICell Id = null;
            ICell DsServico = null;

            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "SERVICOS");

            row = sheet.GetRow(1);

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                row = sheet.GetRow(linha);

                if (row != null)
                {
                    _list = new ServicosGeraisViewModel();

                    Id = row.GetCell(4);
                    DsServico = row.GetCell(2);

                    _list.Id = Int32.Parse(Id.ToString());
                    _list.NmServico = DsServico.ToString();

                    ranking.Add(_list);
                }

            }

            List<ServicosGeraisViewModel> aux = new List<ServicosGeraisViewModel>();
            aux = ranking.OrderByDescending(x => x.Id).ToList();

            ranking.Clear();
            ranking = new List<ServicosGeraisViewModel>();

            ranking.Add(aux[0]);
            ranking.Add(aux[1]);
            ranking.Add(aux[2]);

            server.BDClose(plan);
            
            return ranking;
        }

        public List<ContrataServicoViewModels> GetTodosServicos()
        {
            #region [Declarações]

            HSSFWorkbook plan = new HSSFWorkbook();
            BDService server = new BDService();
            List<ContrataServicoViewModels> lista = new List<ContrataServicoViewModels>();

            ISheet sheet = null;
            IRow row = null;
            ICell cellIdServico = null;
            ICell cellIdTpServico = null;
            ICell cellDsServico = null;
            ICell cellIdUsuario = null;
            ICell cellVlServico = null;
            ICell cellNota = null;
            ICell cellDistancia = null;
            
            #endregion [Declarações]
            
            plan = null;
            sheet = null;

            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "SERVICOS");

            row = sheet.GetRow(1);

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                row = sheet.GetRow(linha);

                if (row != null)
                {
                    cellIdServico = row.GetCell(0);
                    cellIdTpServico = row.GetCell(1);
                    cellDsServico = row.GetCell(2);
                    cellIdUsuario = row.GetCell(3);
                    cellVlServico = row.GetCell(9);
                    cellNota = row.GetCell(10);
                    cellDistancia = row.GetCell(11);

                    ContrataServicoViewModels _listaAux = new ContrataServicoViewModels();

                    _listaAux.IdServico = Int32.Parse(cellIdServico.ToString());
                    _listaAux.TipoServico.IdCategoria = Int32.Parse(cellIdTpServico.ToString());
                    _listaAux.NmServico = cellDsServico.ToString();
                    _listaAux.Usuario.Id = Int32.Parse(cellIdUsuario.ToString());
                    _listaAux.VlServico = Decimal.Parse(cellVlServico.ToString());
                    _listaAux.NotaServico = Decimal.Parse(cellNota.ToString());
                    _listaAux.Distancia = Decimal.Parse(cellDistancia.ToString());

                    lista.Add(_listaAux);
                }
            }
            server.BDClose(plan);

            return lista;
        }

        public ContrataServicoViewModels GetServicoPorId(int IdServico)
        {
            ContrataServicoViewModels list = new ContrataServicoViewModels();

            var retorno = GetTodosServicos().Where(x => x.IdServico == IdServico).ToList();

            if(retorno != null)
            {
                foreach (ContrataServicoViewModels x in retorno)
                {
                    list = x;
                }

                var categ = GetListCategorias().Where(x => x.IdCategoria == list.TipoServico.IdCategoria).ToList();

                if(categ != null)
                {
                    foreach(ListCategoria x in categ)
                    {
                        list.TipoServico = x;
                    }
                }

                UsuarioService user = new UsuarioService();

                HSSFWorkbook plan = new HSSFWorkbook();
                BDService server = new BDService();

                ISheet sheet = null;

                plan = server.BDInit();
                sheet = server.BDGetFolha(plan, "USUARIO");

                var usuario = user.GetTodosUsuarios(sheet).Where(x => x.Id == list.Usuario.Id).ToList();

                server.BDClose(plan);

                if(usuario != null)
                {
                    foreach(FiltroViewModels x in usuario)
                    {
                        list.Usuario = x;
                    }
                }
            }
            return list;
        }

        public bool ContaServico(int IdServico)
        {
            HSSFWorkbook work = new HSSFWorkbook();
            BDService bd = new BDService();
            ISheet sheet = null;
            IRow row = null;
            ICell cellId = null;
            ICell cellQt = null;
            int _id = -1;
            int _qtPesquisa = -1;

            work = bd.BDInit();
            sheet = bd.BDGetFolha(work, "SERVICOS");
            
            row = sheet.GetRow(1);

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                row = sheet.GetRow(linha);

                if (row != null)
                {
                    cellId = row.GetCell(0);

                    _id = Int32.Parse(cellId.ToString());
                    
                    if(_id == IdServico)
                    {
                        cellQt = row.GetCell(4);
                        _qtPesquisa = Int32.Parse(cellQt.ToString());

                        _qtPesquisa++;

                        row.CreateCell(4).SetCellValue(_qtPesquisa);
                    }
                }

            }
            
            if (bd.BDSave(work))
            {
                bd.BDClose(work);
                return true;
            }

            bd.BDClose(work);
            return false;
        }
    }
}