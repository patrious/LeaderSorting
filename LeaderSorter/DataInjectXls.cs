﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;


namespace tests.LeaderSorter
{
    class DataInjectXls : ILeaderDataSource
    {
        public string FilePath { get; private set; }
        private LeaderSorting leaderSorting;

        public DataInjectXls(string filename)
        {
            if (Path.GetExtension(filename) != ".xlsx" && Path.GetExtension(filename) != ".xls")
                throw new Exception("Invalid File Type");
            FilePath = filename;
        }

        public void FillMeWithData(LeaderSorting iga)
        {
            leaderSorting = iga;
            using (var excelReader = OpenFile())
            {
                var data = excelReader.AsDataSet();
                foreach (DataTable table in data.Tables)
                {
                    //Parse out Column Names
                    var leaderParser = new LeaderParser();
                    leaderParser.InjectColumnNames(table.Rows[0]);
                    var leaderInformationKey = ParseKeys();
                    for (var i = 1; i < table.Rows.Count; i++)
                    {
                        leaderParser.ParseLeader(table.Rows[i].ItemArray);
                    }
                }

            }


        }

        

        private static Dictionary<string, int> ParseKeys(DataRow dataRow)
        {
          

        }

        private IExcelDataReader OpenFile()
        {
            FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            if (Path.GetExtension(FilePath) == ".xlsx")
                return ExcelReaderFactory.CreateOpenXmlReader(stream);
            if (Path.GetExtension(FilePath) == ".xls")
                return ExcelReaderFactory.CreateBinaryReader(stream);
            throw new Exception("Invalid File Type");
        }
    }
}
