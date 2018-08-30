using ExcelDataReader;
using System;
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
                    UseHeaderRow = false
                }
            });
            reader.Close();

            foreach(DataTable dt in result.Tables)
            {
                dt.Columns[0].ColumnName = "Network Element";
                dt.Columns[1].ColumnName = "Command";
            }
        }

        public DataTableCollection GetSheets()
        {
            try
            {
                return result.Tables;
            }
            catch(Exception ex)
            {
                FileParser.LogException(ex.Message);
                return null;
            }
        }
        
        private void CountNetworkElements()
        {
            // linq
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

        private void MarkStatus()
        {
            foreach (string s in networkElements)
                networkElementsStatus.Add(dataBaseReference.CheckConn1(s));
        }

        public void InitializeStatus()
        {
            string status;
            CountNetworkElements();
            MarkStatus();
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
            /// -1 => "Doesn't Exist" in database
            /// 1 => "Available"
            /// 0 => exists in database but has a "Connection Error"
        }

        public void RefreshConnection(string name)
        {
            try
            {
                int position = networkElements.IndexOf(name);
                networkElementsStatus[position] = dataBaseReference.CheckConn1(name);
            }
            catch(Exception ex)
            {
                FileParser.LogException(ex.Message);
            }
        }

        public void ExcuteCommands()
        {
            foreach (DataTable dt in result.Tables)
            {
                dt.Columns.Add("Excution");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[2].Equals("Available"))
                    {
                        bool excutionResult = SSH.Excute(dr[0].ToString(), dr[1].ToString());
                        dr[3] = excutionResult.ToString();
                    }
                    else
                    {
                        dr[3] = "---";
                    }
                }
            }
        }

        public void Reset()
        {
            result = null;
            networkElements = null;
            networkElementsStatus = null;
        }
    }
}