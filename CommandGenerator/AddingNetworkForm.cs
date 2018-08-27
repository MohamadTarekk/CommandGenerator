using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CommandGenerator
{
    public partial class AddingNetworkForm : Form
    {
        private bool usernameEntered;
        private bool IPEntered;
        private bool NetworkEntered;
        Queries q = new Queries();
        DataGridView NetworkGrid = new DataGridView();
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

        public AddingNetworkForm(DataGridView NetworkGrid)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.NetworkGrid = NetworkGrid;
        }

        public bool checkPnlAddNetwork()
        {
            if (tbUsername.Text != "" && tbPassword.Text != "" && tbIP.Text != "" && tbNetwork.Text != ""
                && usernameEntered && IPEntered && NetworkEntered && tbPassword.UseSystemPasswordChar)
                return true;
            return false;
        }

        public void clearPnlAddNetwork()
        {
            tbIP.Text = "IP";
            tbNetwork.Text = "Network Name";
            tbUsername.Text = "Username";
            tbPassword.Text = "Password";
            IPEntered = false;
            NetworkEntered = false;
            usernameEntered = false;
            tbPassword.UseSystemPasswordChar = false;
        }

        private void exitPnlAddUser_Click(object sender, EventArgs e)
        {
            clearPnlAddNetwork();
            this.Hide();
        }

        private void tbIP_Enter(object sender, EventArgs e)
        {
            pnlTbIP.BackColor = Color.FromArgb(26, 73, 148);
            if (tbIP.Text == "IP" && !IPEntered)
            {
                tbIP.Text = "";
                IPEntered = true;
                tbIP.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbIP_Leave(object sender, EventArgs e)
        {
            pnlTbIP.BackColor = Color.Transparent;
            if (tbIP.Text == "")
            {
                tbIP.Text = "IP";
                IPEntered = false;
                tbIP.ForeColor = Color.CornflowerBlue;
            }
        }

        private void tbNetwork_Enter(object sender, EventArgs e)
        {
            pnlTbNetwork.BackColor = Color.FromArgb(26, 73, 148);
            if (tbNetwork.Text == "Network Name" && !NetworkEntered)
            {
                tbNetwork.Text = "";
                NetworkEntered = true;
                tbNetwork.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbNetwork_Leave(object sender, EventArgs e)
        {
            pnlTbNetwork.BackColor = Color.Transparent;
            if (tbNetwork.Text == "")
            {
                tbNetwork.Text = "Network Name";
                NetworkEntered = false;
                tbNetwork.ForeColor = Color.CornflowerBlue;
            }
        }

        private void tbUsername_Enter(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.FromArgb(26, 73, 148);
            if (tbUsername.Text == "Username" && !usernameEntered)
            {
                tbUsername.Text = "";
                usernameEntered = true;
                tbUsername.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbUsername_Leave(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.Transparent;
            if (tbUsername.Text == "")
            {
                tbUsername.Text = "Username";
                usernameEntered = false;
                tbUsername.ForeColor = Color.CornflowerBlue;
            }
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            pnlTbPassword.BackColor = Color.FromArgb(26, 73, 148);
            if (tbPassword.Text == "Password" && !tbPassword.UseSystemPasswordChar)
            {
                tbPassword.Text = "";
                tbPassword.UseSystemPasswordChar = true;
                tbPassword.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbPassword_Leave(object sender, EventArgs e)
        {
            pnlTbPassword.BackColor = Color.Transparent;
            if (tbPassword.Text == "")
            {
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.CornflowerBlue;
            }
        }

        private void btnPerformAdd_Click(object sender, EventArgs e)
        {
            string IP = tbIP.Text;
            string network = tbNetwork.Text;
            string username = tbUsername.Text;
            string password = User.Encrypt(tbPassword.Text);
            if (checkPnlAddNetwork())
            {
                if (AddingNetworks(username, password, IP, network))
                {
                    MessageBox.Show("Network Added Successfully");
                    ListingNetworks();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error Occured", "Warning");

                }


            }
            else
                MessageBox.Show("Please provide all the required information.", "Warning");
        }
        private void ListingNetworks()
        {
            SQLiteConnection myconn = new SQLiteConnection(pathh);
            try
            {
                
                myconn.Open();
                

            }
            catch (Exception)
            {
                MessageBox.Show("Check Network");
            }
            Queries q = new Queries();
            DataTable show = new DataTable();
            SQLiteDataAdapter adap = new SQLiteDataAdapter(q.ShowNetworks(), myconn);
            adap.Fill(show);
            NetworkGrid.DataSource = show;
            myconn.Close();
        }

        private bool AddingNetworks(String Username, String Enc_Password, String IP, String Name)
        {

            SQLiteConnection myconn = new SQLiteConnection(pathh);
            try
            {
                myconn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Check Network");
            }
            Queries q = new Queries();
            DataTable show = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand(q.AddNetwork(Username, Enc_Password, IP, Name), myconn);
            int Result = cmd.ExecuteNonQuery();
            myconn.Close();
            if (Result > 0)

                return true;
            else
                return false;

        }













        private void AddingNetworkForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }

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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //*/
    }
}
