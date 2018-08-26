using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace CommandGenerator
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
         
        }
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Log_Click(object sender, EventArgs e)
        {
    
            bool x = Login();
            if (Login()==true)
            {
                User user = new User();
                user.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong Username Or Password");
            }
           

        }
        private bool Login()
        {


            String logname = txt_Username.Text;
            String Password = txt_Password.Text;
            string[] s = { "\\bin" };
            string database =
                           Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Nokia.db";
            string connectionString =
                "Data Source=" + Path.GetFullPath(database);
            SQLiteConnection myconn = new SQLiteConnection (connectionString);

            try
            {
                myconn.Open();
          
            Queries q = new Queries();
            DataTable show = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand(q.Login(logname, Password), myconn);

            int Result = Convert.ToInt32(cmd.ExecuteScalar());
            Console.WriteLine(Result);
            myconn.Close();
                if (Result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception )
            {
                MessageBox.Show("PROBLEM RETRIEVING DATA PLEASE TRY AGAIN");
            }


            return false;

        }

    }
}
