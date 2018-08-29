using System;
using System.IO;

namespace CommandGenerator
{
    class FileParser
    {
        public static int counter = 0;
        public static void SaveResult(string name, string IP, string commandText, string result)
        {

        }

        public static void LogException(string error)
        {
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
    }
}
