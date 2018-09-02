using System;
using System.IO;
using System.Windows.Forms;

namespace CommandGenerator
{
    class FileParser
    {
        private static int Ncounter = 0;

        private static readonly string[] s = { "\\bin" };
        private static readonly string LogsDirectory =
                       Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Logs";
        private static readonly string LogsPath = Path.GetFullPath(LogsDirectory);

        private static readonly string OutputDirectory =
                       Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Result";
        private static readonly string OutputPath = Path.GetFullPath(OutputDirectory);



        private static string SavingPath = "";

        public static void LogException(Exception ex)
        {
            try
            {
                if (!Directory.Exists(LogsPath))
                    Directory.CreateDirectory(LogsPath);
                string Filep = "ErrorsLog.txt";
                string lpath = LogsPath + "\\" + Filep;
                StreamWriter w = File.AppendText(lpath);
                w.Dispose();
                string file = Path.Combine(LogsPath, Filep);
                File.AppendAllText(file, ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
            }
            catch (Exception exception)
            {
                //FileParser.LogException(ex.StackTrace);
                Console.WriteLine(exception.StackTrace);
            }
        }

        public static void SaveResult(string name, string IP, string commandText, string result)
        {
            try
            {
                string path = Path.GetFullPath(OutputPath);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += ("\\" + name);
                path = Path.GetFullPath(path);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string Filep = "command_" + Ncounter + ".txt";
                string file = Path.Combine(path, Filep);
                string text = name + Environment.NewLine + IP + Environment.NewLine + commandText + Environment.NewLine + result + Environment.NewLine + Environment.NewLine;
                File.WriteAllText(file, text);
                string alltxt = OutputPath + "\\AllCommands.txt";
                using (StreamWriter w = File.AppendText(alltxt))
                {
                    w.WriteLine(text);
                }
                Ncounter++;
            }
            catch(Exception ex)
            {
                FileParser.LogException(ex);
            }
        }

        public static void CreateDirectory()
        {
            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(OutputPath);
            string Filep = "AllCommands.txt";
            string lpath = OutputPath + "\\" + Filep;
            StreamWriter w = File.AppendText(lpath);
            w.Dispose();
            string file = Path.Combine(OutputPath, Filep);
        }

        public static void CreateZip()
        {
            using (var zip = new Ionic.Zip.ZipFile())
            {
                using (FileStream fs = File.Create(SavingPath))
                {
                    zip.AddDirectory(OutputPath);
                    zip.Save(fs);
                }
            }
            try
            {
                var dir = new DirectoryInfo(OutputPath);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
            }
            catch (IOException ex)
            {
                FileParser.LogException(ex);
            }
            /*using (var zip = new Ionic.Zip.ZipFile())
            {
                zip.AddDirectory(Environment.GetFolderPath(
              System.Environment.SpecialFolder.DesktopDirectory) + "\\code");
                FileStream x = File.Create("D:\\Visual Studio\\CommandGenerator\\MyFile.zip");
                zip.Save(x);
                x.Close();
                zip.Dispose();
            }
            */
        }

        public static bool SetSavingPath()
        {
            SaveFileDialog savefile = new SaveFileDialog
            {
                // set a default file name
                FileName = "Result.zip",
                // set filters - this can be done in properties as well
                Filter = "Zip Files|*.zip;*.rar"
            };

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                SavingPath = savefile.FileName;
                return true;
            }
            return false;
        }
    }
}
