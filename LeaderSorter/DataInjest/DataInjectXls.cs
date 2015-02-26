using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Excel;
using tests.LeaderSorter;

namespace GeneticAlgorithm.LeaderSorter.DataInjest
{
    class DataInjectXls : ILeaderDataSource
    {

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            private set
            {
                if (File.Exists(value))
                    filePath = value;
                else
                {
                    filePath = null;
                    Console.WriteLine("File {0} does not exist.", value);
                }
            }
        }

        public DataInjectXls(string filename)
        {
            if (Path.GetExtension(filename) != ".xlsx" && Path.GetExtension(filename) != ".xls")
                throw new Exception("Invalid File Type");
            FilePath = filename;
        }

        public void FillMeWithData(ref LeaderSorting iga)
        {

            using (var excelReader = OpenFile())
            {
                var data = excelReader.AsDataSet();
                foreach (DataTable table in data.Tables)
                {
                    //Parse out Column Names
                    var leaderParser = new LeaderParser();
                    leaderParser.InjectColumnNames(table.Rows[0]);
                    for (var i = 1; i < table.Rows.Count; i++)
                    {
                        var leader = leaderParser.ParseLeader(table.Rows[i].ItemArray, table.TableName);
                        if (leader != null)
                            iga.Leaderpool.Add(leader);
                    }
                }

            }
            iga.PrepWorkspace();

        }

        private IExcelDataReader OpenFile()
        {
            if (FilePath == null)
                throw new NullReferenceException("FilePath is invalid");

            var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            if (Path.GetExtension(FilePath) == ".xlsx")
                return ExcelReaderFactory.CreateOpenXmlReader(stream);
            if (Path.GetExtension(FilePath) == ".xls")
                return ExcelReaderFactory.CreateBinaryReader(stream);
            throw new Exception("Invalid File Type");
        }
    }
}
