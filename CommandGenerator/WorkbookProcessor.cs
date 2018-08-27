using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CommandGenerator
{
    class WorkbookProcessor
    {
        private DataSet result;
        private List<string> networkElements = new List<string>();
        private List<int> networkElementsStatus = new List<int>();
        private DataBaseReference dataBaseReference = new DataBaseReference();
        private SSHConnection SSH = new SSHConnection();
        
        public void ReadWorkbook(string path, int filterIndex)
        {
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader;
            if (filterIndex == 1)
                reader = ExcelReaderFactory.CreateBinaryReader(fs);
            else
                reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            //reader.IsFirstRowAsColumnNames = true;
            //result = reader.AsDataSet();
            result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            reader.Close();
        }

        public DataTableCollection GetSheets()
        {
            return result.Tables;
        }
        
        private void CountNetworkElements()
        {
            foreach (DataTable dt in result.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!networkElements.Contains(dr[0].ToString()))
                    {
                        networkElements.Add(dr[0].ToString());
                    }
                }
            }
        }

        public void ExcuteCommands()
        {
            foreach (DataTable dt in result.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[2].Equals("Available"))
                    {
                        SSH.Excute(dr[0].ToString(), dr[1].ToString());
                    }
                }
            }
        }

        private void GetStatus()
        {
            foreach (string s in networkElements)
                networkElementsStatus.Add(dataBaseReference.CheckConn1(s));
        }

        public void MarkStatus()
        {
            string status;
            CountNetworkElements();
            GetStatus();
            foreach(DataTable dt in result.Tables)
            {
                dt.Columns.Add("Status");
                foreach(DataRow dr in dt.Rows)
                {
                    if (networkElementsStatus[networkElements.IndexOf(dr[0].ToString())] == 1)
                        status = "Available";
                    else if (networkElementsStatus[networkElements.IndexOf(dr[0].ToString())] == 0)
                        status = "Connection Error";
                    else
                        status = "Doesn't Exist";
                    dr[2] = status;
                }
            }
        }

        /// <summary>
        /// The following functions constructs a list of names of the network elements listed
        /// in the excel sheet without duplicates.
        /// Then constructs a list indicating each element's status
        /// -1 => "Doesn't Exist" in database
        /// 1 => "Available"
        /// 0 => exists in database but has a "Connection Error"
        /// </summary>

    }
}
