using E_Commerce_Service.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.GlobalServices
{
    public class UsuarioService
    {
        public UsuarioService()
        {

        }

        public UsuarioModels GetUsuario(ISheet sheet, UsuarioModels user)
        {
            IRow row = sheet.GetRow(1);
            ICell cellId = null;
            ICell cellNome = null;
            ICell cellSobrenome = null;
            ICell cellIdade = null;
            ICell cellEmail = null;
            ICell cellSenha = null;
            ICell cellTipo = null;
            
            user.Existe = false;

            for(int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                if(row != null)
                {
                    row = sheet.GetRow(linha);

                    cellEmail = row.GetCell(4);

                    if (user.EmailUsuario.ToString().Trim().ToLower() == cellEmail.ToString().Trim().ToLower())
                    {
                        cellId = row.GetCell(0);
                        cellNome = row.GetCell(1);
                        cellSobrenome = row.GetCell(2);
                        cellIdade = row.GetCell(3);
                        cellSenha = row.GetCell(5);
                        cellTipo = row.GetCell(6);

                        user.IdUsuario = Int32.Parse(cellId.ToString());
                        user.NmUsuario = cellNome.ToString();
                        user.MnSobrenome = cellSobrenome.ToString();
                        user.IdadeUsuario = Int32.Parse(cellIdade.ToString());
                        user.EmailUsuario = cellEmail.ToString().Trim();
                        user.SenhaUsuario = cellSenha.ToString().Trim();
                        user.TpUsuario = cellTipo.ToString();
                        user.Existe = true;

                        break;
                    }
                }
            }
            return user;
        }

        public bool ValidaExistenciaUsuario(RegisterViewModel user)
        {
            HSSFWorkbook work = new HSSFWorkbook();
            BDService bd = new BDService();
            ISheet sheet = null;
            IRow row = null;
            ICell cellEmail = null;

            work = bd.BDInit();
            sheet = bd.BDGetFolha(work, "USUARIO");
            row = sheet.GetRow(1);

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                if (row != null)
                {
                    row = sheet.GetRow(linha);
                    cellEmail = row.GetCell(4);

                    if (user.Email.ToString().Trim().ToLower() == cellEmail.ToString().Trim().ToLower())
                    {
                        return true;
                    }
                }
            }

            bd.BDClose(work);

            return false;
        }

        public List<UsuarioModels> GetUsuarioPorNome(ISheet sheet, string nome)
        {
            List<UsuarioModels> usuario = new List<UsuarioModels>();
            IRow row = sheet.GetRow(1);
            ICell cellId = null;
            ICell cellNome = null;
            ICell cellSobrenome = null;
            ICell cellIdade = null;
            ICell cellEmail = null;
            ICell cellSenha = null;
            ICell cellTipo = null;

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                if (row != null)
                {
                    row = sheet.GetRow(linha);

                    cellNome = row.GetCell(1);

                    if (nome.ToString().Trim().ToLower() == cellNome.ToString().Trim().ToLower())
                    {
                        UsuarioModels _tempUsuario = new UsuarioModels();
                        cellId = row.GetCell(0);
                        cellSobrenome = row.GetCell(2);
                        cellIdade = row.GetCell(3);
                        cellEmail = row.GetCell(4);
                        cellSenha = row.GetCell(5);
                        cellTipo = row.GetCell(6);

                        _tempUsuario.IdUsuario = Int32.Parse(cellId.ToString());
                        _tempUsuario.NmUsuario = cellNome.ToString();
                        _tempUsuario.MnSobrenome = cellSobrenome.ToString();
                        _tempUsuario.IdadeUsuario = Int32.Parse(cellIdade.ToString());
                        _tempUsuario.EmailUsuario = cellEmail.ToString().Trim();
                        _tempUsuario.SenhaUsuario = cellSenha.ToString().Trim();
                        _tempUsuario.TpUsuario = cellTipo.ToString();
                        _tempUsuario.Existe = true;

                        usuario.Add(_tempUsuario);
                    }
                }
            }
            return usuario;
        }

        public List<FiltroViewModels> GetTodosUsuarios(ISheet sheet)
        {
            List<FiltroViewModels> usuarios = new List<FiltroViewModels>();

            IRow row = sheet.GetRow(1);
            ICell cellId = null;
            ICell cellNome = null;
            ICell cellIdade = null;
            ICell cellEmail = null;
            ICell cellTipo = null;
            ICell cellSobrenome = null;

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                if (row != null)
                {
                    row = sheet.GetRow(linha);

                    cellId = row.GetCell(0);
                    cellNome = row.GetCell(1);
                    cellSobrenome = row.GetCell(2);
                    cellIdade = row.GetCell(3);
                    cellEmail = row.GetCell(4);
                    cellTipo = row.GetCell(6);

                    if(cellId != null && !string.IsNullOrEmpty(cellId.ToString())){

                        FiltroViewModels _tempUser = new FiltroViewModels();

                        _tempUser.Id = Int32.Parse(cellId.ToString());
                        _tempUser.NmUsuario= cellNome.ToString() + " " + cellSobrenome.ToString();
                        _tempUser.EmailUsuario = cellEmail.ToString().Trim();
                        _tempUser.IdadeUsuario = Int32.Parse(cellIdade.ToString());
                        _tempUser.PerfilUsuario = cellTipo.ToString();

                        usuarios.Add(_tempUser);
                    }

                        
                }
            }

            return usuarios;
        }

        public bool InsUsuario(RegisterViewModel user, string TipoPefil)
        {
            HSSFWorkbook work = new HSSFWorkbook();
            BDService bd = new BDService();
            ISheet sheet = null;
            IRow row = null;
            ICell cellId = null;
            int linha = 0;
            int _id = -1;

            work = bd.BDInit();
            sheet = bd.BDGetFolha(work, "USUARIO");
            
            for (linha = 1; linha <= sheet.LastRowNum; linha++);

            row = sheet.GetRow(linha - 1);
            cellId = row.GetCell(0);

            Int32.TryParse(cellId.ToString(), out _id);

            if(_id == -1)
            {
                throw new Exception("Erro ao inserir usuário.");
            }
            _id++;

            row = sheet.CreateRow(linha);

            row.CreateCell(0).SetCellValue(_id.ToString());
            row.CreateCell(1).SetCellValue(user.Nome.ToString().Trim());
            row.CreateCell(2).SetCellValue(user.Sobrenome.ToString().Trim());
            row.CreateCell(3).SetCellValue(user.Idade.ToString().Trim());
            row.CreateCell(4).SetCellValue(user.Email.ToString().Trim());
            row.CreateCell(5).SetCellValue(user.Password.ToString().Trim());
            row.CreateCell(6).SetCellValue(TipoPefil.Trim());

            if (bd.BDSave(work))
            {
                bd.BDClose(work);
                return true;
            }
            bd.BDClose(work);
            return false;
        }

        public UsuarioModels AltenticaUsuario(string Email)
        {
            HSSFWorkbook plan = new HSSFWorkbook();
            BDService server = new BDService();
            UsuarioModels userModel = new UsuarioModels();
            ISheet sheet = null;
            
            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "USUARIO");

            userModel.EmailUsuario = Email;

            userModel = GetUsuario(sheet, userModel);

            server.BDClose(plan);

            return userModel;
        }

        public List<FiltroViewModels> GetListUsuario(string Nome, string Email)
        {
            HSSFWorkbook plan = new HSSFWorkbook();
            BDService server = new BDService();
            List<FiltroViewModels> lista = new List<FiltroViewModels>();
            List<UsuarioModels> userLista = new List<UsuarioModels>();
            UsuarioModels user = new UsuarioModels();

            ISheet sheet = null;

            plan = server.BDInit();
            sheet = server.BDGetFolha(plan, "USUARIO");

            user.NmUsuario = Nome;
            user.EmailUsuario = Email;
            
            if (Nome == "" && Email != "")
            {
                user = GetUsuario(sheet, user);
                if(user.Existe)
                {
                    FiltroViewModels _tempLista = new FiltroViewModels();

                    _tempLista.Id = user.IdUsuario;
                    _tempLista.NmUsuario = user.NmUsuario + " " + user.MnSobrenome;
                    _tempLista.EmailUsuario = user.EmailUsuario;
                    _tempLista.IdadeUsuario = user.IdadeUsuario;
                    _tempLista.PerfilUsuario = user.TpUsuario;

                    lista.Add(_tempLista);
                }
                return lista;

            }
            else if(Nome != "" && Email == "")
            {
                userLista = GetUsuarioPorNome(sheet, Nome);

                if(userLista.Count() > 0)
                {
                    foreach(UsuarioModels item in userLista)
                    {
                        FiltroViewModels _tempLista = new FiltroViewModels();

                        _tempLista.Id = item.IdUsuario;
                        _tempLista.NmUsuario = item.NmUsuario + " " + item.MnSobrenome;
                        _tempLista.EmailUsuario = item.EmailUsuario;
                        _tempLista.IdadeUsuario = item.IdadeUsuario;
                        _tempLista.PerfilUsuario = item.TpUsuario;

                        lista.Add(_tempLista);
                    }
                }
                return lista; 
            }
            else if(Nome != "" && Email != "")
            {
                user = GetUsuario(sheet, user);
                if(user.Existe)
                {
                    if(user.NmUsuario.ToLower() == Nome.ToLower())
                    {
                        FiltroViewModels _tempLista = new FiltroViewModels();

                        _tempLista.Id = user.IdUsuario;
                        _tempLista.NmUsuario = user.NmUsuario + " " + user.MnSobrenome;
                        _tempLista.EmailUsuario = user.EmailUsuario;
                        _tempLista.IdadeUsuario = user.IdadeUsuario;
                        _tempLista.PerfilUsuario = user.TpUsuario;

                        lista.Add(_tempLista);
                    }
                }
                
            }

            else
            {
                lista = GetTodosUsuarios(sheet);
            }
            return lista;
        }

        public int GetUsuarioPorEmail(string Email)
        {
            BDService bd = new BDService();
            HSSFWorkbook plan = null;
            ISheet sheet = null;
            
            plan = bd.BDInit();
            sheet = bd.BDGetFolha(plan, "USUARIO");

            IRow row = sheet.GetRow(1);
            ICell cellId = null;
            ICell cellEmail = null;
            int IdUsuario = 0;

            for (int linha = 1; linha <= sheet.LastRowNum; linha++)
            {
                if (row != null)
                {
                    row = sheet.GetRow(linha);

                    cellEmail = row.GetCell(4);

                    if (Email.Trim().ToLower() == cellEmail.ToString().Trim().ToLower())
                    {
                        cellId = row.GetCell(0);
                        IdUsuario = Int32.Parse(cellId.ToString());
                        break;
                    }
                }
            }
            bd.BDClose(plan);
            return IdUsuario;
        }
    }
}