using System;
using System.IO;


namespace CommandGenerator
{
    class FileParser
    {
        public static int counter = 0;
        public static int Ncounter = 0;
        public static void SaveResult(string name, string IP, string commandText, string result)
        {
            try
            {
                string strPath = Environment.GetFolderPath(
               System.Environment.SpecialFolder.DesktopDirectory);
                string path = Path.GetFullPath(strPath) + "\\networkelements";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += "\\" + name;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string Filep = "command_" + Ncounter + ".txt";
                string lpath = path + "\\" + Filep;
                using (StreamWriter w = File.AppendText(Filep)) ;
                string file = Path.Combine(path, Filep);
                string text = name + Environment.NewLine + IP + Environment.NewLine + commandText + Environment.NewLine + result;
                File.WriteAllText(file, text);
                string alltxt = path + "AllCommands.txt";
                using (StreamWriter w = File.AppendText(alltxt))
                {
                    w.WriteLine(text);
                }
                Ncounter++;
            }
            catch(Exception ex)
            {
                FileParser.LogException(ex.Message);
            }
        }

        public static void LogException(string error)
        {
            try { 
            string strPath = Environment.GetFolderPath(
               System.Environment.SpecialFolder.DesktopDirectory);
            string path = Path.GetFullPath(strPath) + "\\Logs";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string Filep = "Error_" + counter + ".txt";
            string lpath = path + "\\" + Filep;
            using (StreamWriter w = File.AppendText(Filep)) ;
            string file = Path.Combine(path, Filep);
            File.WriteAllText(file, error);
            counter++;
            }
            catch (Exception ex)
            {
                FileParser.LogException(ex.Message);
            }
        }
       
    }
}
