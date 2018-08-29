using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace CommandGenerator
{
    class DataBaseReference
    {
        public static string[] s = { "\\bin" };
        public static string database =
                       Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Nokia.db";
        public static string pathh =
            "Data Source=" + Path.GetFullPath(database);
        SQLiteConnection myconn = new SQLiteConnection(pathh); //* Put Your Own Link
        Queries q = new Queries();

        public bool CheckExistence(String Name)
        {
            myconn.Close();
            myconn.Open();
            SQLiteCommand cmd = new SQLiteCommand(q.SearchNetwork(Name), myconn);
            int Result = Convert.ToInt32(cmd.ExecuteScalar());
            if (Result == 0)
                return false;
            else
                return true;
        }

        public int CheckConn1(String Name)
        {
            SSHConnection ssh = new SSHConnection();
            if (CheckExistence(Name) == true)
            {
                SQLiteCommand cmd = new SQLiteCommand(q.GetIp(Name), myconn);
                String Ip = Convert.ToString(cmd.ExecuteScalar());
                SQLiteCommand cmd2 = new SQLiteCommand(q.GetUsername(Name), myconn);
                String Username = Convert.ToString(cmd.ExecuteScalar());
                SQLiteCommand cmd1 = new SQLiteCommand(q.GetPassword(Name), myconn);
                String Password = Convert.ToString(cmd.ExecuteScalar());

                if (ssh.TestConnection(Ip, Username, Password))
                    return 1;
                else
                    return 0;

            }
            else
                return -1; //* if Name is not Existed (First Function)


        }

        public string[] AccessNetworkElement(string Name)
        {
            SQLiteCommand cmd = new SQLiteCommand(q.GetIp(Name), myconn);
            String Ip = Convert.ToString(cmd.ExecuteScalar());
            SQLiteCommand cmd2 = new SQLiteCommand(q.GetUsername(Name), myconn);
            String Username = Convert.ToString(cmd.ExecuteScalar());
            SQLiteCommand cmd1 = new SQLiteCommand(q.GetPassword(Name), myconn);
            String Password = Convert.ToString(cmd.ExecuteScalar());

            string[] data = new string[3];
            data[0] = Ip;
            data[1] = Username;
            data[2] = Password;

            return data;
        }

        public bool CheckConn0(String Name)
        {
            SSHConnection ssh = new SSHConnection();
            SQLiteCommand cmd = new SQLiteCommand(q.GetIp(Name), myconn);
            String Ip = Convert.ToString(cmd.ExecuteScalar());
            SQLiteCommand cmd2 = new SQLiteCommand(q.GetUsername(Name), myconn);
            String Username = Convert.ToString(cmd.ExecuteScalar());
            SQLiteCommand cmd1 = new SQLiteCommand(q.GetPassword(Name), myconn);
            String Password = Convert.ToString(cmd.ExecuteScalar());
            if (ssh.TestConnection(Ip, Username, Password))
                return true;
            else
                return false;


        }

    }
}
