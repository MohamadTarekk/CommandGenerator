using System.Data;
using System.Windows.Forms;

namespace CommandGenerator
{
    class CommandsTabController
    {
        private WorkbookProcessor workbookProcessor = new WorkbookProcessor();

        public void BrowzeButtonClicked(ComboBox sheetsCB)
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
            workbookProcessor.MarkStatus();
        }

        public DataTable GetSheet(int tableName)
        {
            return workbookProcessor.GetSheets()[tableName];
        }
    }
}
