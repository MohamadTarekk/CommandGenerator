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
                    if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
                    {
                        workbookProcessor.ReadWorkbook(ofd.FileName, ofd.FilterIndex);
                        sheetsCB.Items.Clear();
                        foreach (DataTable dt in workbookProcessor.GetSheets())
                        {
                            sheetsCB.Items.Add(dt.TableName);
                        }
                        workbookProcessor.InitializeStatus();
                    }
                }
            }
            catch(Exception ex)
            {
                FileParser.LogException(ex);
            }
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

        public bool HasSheets()
        {
            if (workbookProcessor.HasSheets())
            {
                return true;
            }
            return false;
        }

        public DataTable GetSheet(int tableIndex)
        {
            if(tableIndex < 0)
            {
                return null;
            }
            if (!workbookProcessor.HasSheets())
            {
                return null;
            }
            return workbookProcessor.GetSheets()[tableIndex];
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
