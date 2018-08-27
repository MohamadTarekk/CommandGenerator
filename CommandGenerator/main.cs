using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CommandGenerator
{
    public partial class CommandGenerator : Form
    {
        private bool usernameEntered;

        public CommandGenerator()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Log_Click(object sender, EventArgs e)
        {
            bool x = Login();
            if (Login() == true)
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
            String Password = User.Encrypt(txt_Password.Text);
            string[] s = { "\\bin" };
            string database =
                           Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Nokia.db";
            string connectionString =
                "Data Source=" + Path.GetFullPath(database);
            SQLiteConnection myconn = new SQLiteConnection(connectionString);

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
            catch (Exception)
            {
                MessageBox.Show("PROBLEM RETRIEVING DATA PLEASE TRY AGAIN");
            }


            return false;

        }

        private void exitPnlAddNetwork_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /* Making the panel's corner round */
        public static string[] s = { "\\bin" };
        public static string database =
        Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Nokia.db";
        public static string pathh =
            "Data Source=" + Path.GetFullPath(database);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        //*/

        /* making the form moveable */
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void pnlMoveable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //*/

        private void CommandGenerator_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }

        private void txt_Username_Enter(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.FromArgb(26, 73, 148);
            if (txt_Username.Text == "Username" && !usernameEntered)
            {
                txt_Username.Text = "";
                usernameEntered = true;
                txt_Username.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void txt_Username_Leave(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.Transparent;
            if (txt_Username.Text == "")
            {
                txt_Username.Text = "Username";
                usernameEntered = false;
                txt_Username.ForeColor = Color.CornflowerBlue;
            }
        }

        private void txt_Password_Enter(object sender, EventArgs e)
        {
            pnlTbPassword.BackColor = Color.FromArgb(26, 73, 148);
            if (txt_Password.Text == "Password" && !txt_Password.UseSystemPasswordChar)
            {
                txt_Password.Text = "";
                txt_Password.UseSystemPasswordChar = true;
                txt_Password.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void txt_Password_Leave(object sender, EventArgs e)
        {
            pnlTbPassword.BackColor = Color.Transparent;
            if (txt_Password.Text == "")
            {
                txt_Password.UseSystemPasswordChar = false;
                txt_Password.Text = "Password";
                txt_Password.ForeColor = Color.CornflowerBlue;
            }
        }

        private void exitPnlAddNetwork_MouseHover(object sender, EventArgs e)
        {
            exitPnlAddNetwork.ForeColor = Color.Silver;
        }

        private void exitPnlAddNetwork_MouseLeave(object sender, EventArgs e)
        {
            exitPnlAddNetwork.ForeColor = Color.DimGray;
        }

        internal class CircularButton : global::CommandGenerator.CircularButton
        {

        }
    }
}
