using System;
using System.Data;
using System.Windows.Forms;

namespace CommandGenerator
{
    class CommandsTabController
    {
        private WorkbookProcessor workbookProcessor = new WorkbookProcessor();

        public void BrowzeButtonClicked(ComboBox sheetsCB)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook 97-2003|*.xls|Excel Workbook|*.xlsx", ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        workbookProcessor.ReadWorkbook(ofd.FileName, ofd.FilterIndex);
                        sheetsCB.Items.Clear();
                        foreach (DataTable dt in workbookProcessor.GetSheets())
                            sheetsCB.Items.Add(dt.TableName);
                    }
                }
                workbookProcessor.InitializeStatus();
            }
            catch(Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        public void ExcuteButtonPressed()
        {
            if (FileParser.SetSavingPath())
            {
                FileParser.CreateDirectory();
                workbookProcessor.ExcuteCommands();
                FileParser.CreateZip();
            }
        }

        public DataTable GetSheet(int tableName)
        {
            try
            {
                return workbookProcessor.GetSheets()[tableName];
            }
            catch(Exception ex)
            {
                FileParser.LogException(ex.StackTrace);
                return null;
            }
        }

        public void RefreshConnection(string name)
        {
            workbookProcessor.RefreshConnection(name);
        }

        public void ClearGrid()
        {
            workbookProcessor.Reset();
        }
    }
}
