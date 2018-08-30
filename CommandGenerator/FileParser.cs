using System;
using System.IO;
using System.Windows.Forms;

namespace CommandGenerator
{
    class FileParser
    {
        private static int counter = 0;
        private static int Ncounter = 0;

        private static string[] s = { "\\bin" };
        private static string LogsDirectory =
                       Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Logs";
        private static string LogsPath = Path.GetFullPath(LogsDirectory);

        private static string OutputDirectory =
                       Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Result";
        private static string OutputPath = Path.GetFullPath(OutputDirectory);



        private static string SavingPath = "";

        public static void LogException(string error)
        {
            try
            {
                if (!Directory.Exists(LogsPath))
                    Directory.CreateDirectory(LogsPath);
                string Filep = "Error_" + counter + ".txt";
                string lpath = LogsPath + "\\" + Filep;
                StreamWriter w = File.AppendText(Filep);
                w.Dispose();
                string file = Path.Combine(LogsPath, Filep);
                File.WriteAllText(file, error);
                counter++;
            }
            catch (Exception ex)
            {
                //FileParser.LogException(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void SaveResult(string name, string IP, string commandText, string result)
        {
            try
            {
                string path = Path.GetFullPath(OutputPath);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += "\\" + name;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string Filep = "command_" + Ncounter + ".txt";
                string lpath = path + "\\" + Filep;
                StreamWriter ws = File.AppendText(Filep);
                ws.Dispose();
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
                FileParser.LogException(ex.StackTrace);
            }
        }

        public static void CreateDirectory()
        {
            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(OutputPath);
            string Filep = "AllCommands.txt";
            string lpath = LogsPath + "\\" + Filep;
            StreamWriter w = File.AppendText(Filep);
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
                MessageBox.Show(ex.StackTrace);
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
