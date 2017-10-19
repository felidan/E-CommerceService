using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace E_Commerce_Service.GlobalServices
{
     class BDService
    {
        public BDService()
        {
            
        }

        public HSSFWorkbook BDInit()
        {
            HSSFWorkbook plan = new HSSFWorkbook();

            using (FileStream file = new FileStream
                (
                    @"C:\ECommerceService\RootSistem.xls",
                    FileMode.Open,
                    FileAccess.ReadWrite
                )){
                    plan = new HSSFWorkbook(file);
                }
            return plan;
        }

        public bool BDClose(HSSFWorkbook _plan)
        {
            _plan.Close();
            return true;
        }

        public ISheet BDGetFolha(HSSFWorkbook _plan, string nomeFolha)
        {
            ISheet sheet = _plan.GetSheet(nomeFolha);

            return sheet;
        }

        public bool BDSave(HSSFWorkbook work)
        {
            FileStream file = null;
            using (file = new FileStream
                (
                    @"C:\ECommerceService\RootSistem.xls",
                    FileMode.Open
                ))
            {
                work.Write(file);
            }
            return true;
        }

    }
}