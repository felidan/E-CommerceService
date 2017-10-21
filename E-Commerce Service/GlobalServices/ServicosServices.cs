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
    }
}