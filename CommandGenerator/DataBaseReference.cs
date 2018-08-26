using System;
using System.Data.SQLite;

namespace CommandGenerator
{
    class DataBaseReference
    {
        SQLiteConnection myconn = new SQLiteConnection(@"Data Source = C:\Users\LENOVO\Desktop\Functions\Nokia.db"); //* Put Your Own Link
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

                if (ssh.Connection(Ip, Username, Password))
                    return 1;
                else
                    return 0;

            }
            else
                return -1; //* if Name is not Existed (First Function)


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
            if (ssh.Connection(Ip, Username, Password))
                return true;
            else
                return false;


        }

    }
}
