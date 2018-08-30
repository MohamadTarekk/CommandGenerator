using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CommandGenerator
{
    public partial class AddingUserForm : Form
    {
        DataGridView usersGrid = new DataGridView();
        private bool usernameEntered;
        
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

        public AddingUserForm(DataGridView usersGrid)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.usersGrid = usersGrid;
        }

        public bool CheckPnlAddUser()
        {
            if (tbUsername.Text != "" && tbPassword.Text != "" && tbConfirmPassword.Text != ""
                && tbPassword.Text == tbConfirmPassword.Text && usernameEntered
                && tbPassword.UseSystemPasswordChar && tbConfirmPassword.UseSystemPasswordChar)
                return true;
            return false;
        }

        public void ClearPnlAddUser()
        {
            tbUsername.Text = "Username";
            tbPassword.Text = "Password";
            tbConfirmPassword.Text = "Confirm Password";
            usernameEntered = false;
            tbPassword.UseSystemPasswordChar = false;
            tbConfirmPassword.UseSystemPasswordChar = false;
        }

        private void TbUsername_Enter(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.FromArgb(26, 73, 148);
            if (tbUsername.Text == "Username" && !usernameEntered)
            {
                tbUsername.Text = "";
                usernameEntered = true;
                tbUsername.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void TbUsername_Leave(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.Transparent;
            if (tbUsername.Text == "")
            {
                tbUsername.Text = "Username";
                usernameEntered = false;
                tbUsername.ForeColor = Color.CornflowerBlue;
            }
        }

        private void RefreshUsers()
        {
            SQLiteConnection myconn = new SQLiteConnection(pathh);
            myconn.Open();
            DataTable show = new DataTable();
            SQLiteDataAdapter adap = new SQLiteDataAdapter("Select * from usersTable", myconn);
            adap.Fill(show);
            try
            {
                usersGrid.DataSource = show;
            }
            catch (Exception ex)
            {
                usersGrid.Rows[User.row].Cells[2].Value = User.oldUsername;
                usersGrid.Rows[User.row].Cells[3].Value = User.Encrypt(User.oldPassword);
                Console.WriteLine(ex.StackTrace);
            }
            myconn.Close();
        }

        private void BtnPerformAdd_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = User.Encrypt(tbPassword.Text);
            if (CheckPnlAddUser())
            {
               
                SQLiteConnection myconnection = new SQLiteConnection(pathh);
                myconnection.Open();
                string query = "select count(*) from usersTable where username='" + username + "';";
                SQLiteCommand cmd = new SQLiteCommand(query, myconnection);
                cmd.Parameters.Add(new SQLiteParameter("@username", username));
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    cmd.CommandText = "insert into usersTable(username, password) values(@username, @password)";
                    cmd.Parameters.Add(new SQLiteParameter("@username", username));
                    cmd.Parameters.Add(new SQLiteParameter("@password", password));
                    cmd.ExecuteNonQuery();
                    myconnection.Close();
                    RefreshUsers();
                    ClearPnlAddUser();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("This username is already in use.", "Can not add");
                }
            }
            else
            {
                if(!(tbPassword.UseSystemPasswordChar && tbConfirmPassword.UseSystemPasswordChar))
                    MessageBox.Show("Please provide all the required information.", "Warning");
                else if (tbPassword.Text != tbConfirmPassword.Text)
                    MessageBox.Show("Passwords doesn't match!", "Error");
                else
                    MessageBox.Show("Please provide all the required information.", "Warning");
            }
        }

        private void TbPassword_Enter(object sender, EventArgs e)
        {
            pnlTbPassword.BackColor = Color.FromArgb(26, 73, 148);
            if (tbPassword.Text == "Password" && !tbPassword.UseSystemPasswordChar)
            {
                tbPassword.Text = "";
                tbPassword.UseSystemPasswordChar = true;
                tbPassword.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void TbPassword_Leave(object sender, EventArgs e)
        {
            pnlTbPassword.BackColor = Color.Transparent;
            if (tbPassword.Text == "")
            {
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.CornflowerBlue;
            }
        }

        private void TbConfirmPassword_Enter(object sender, EventArgs e)
        {
            pnlTbConfirmPassword.BackColor = Color.FromArgb(26, 73, 148);
            if (tbConfirmPassword.Text == "Confirm Password" && !tbConfirmPassword.UseSystemPasswordChar)
            {
                tbConfirmPassword.Text = "";
                tbConfirmPassword.UseSystemPasswordChar = true;
                tbConfirmPassword.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void TbConfirmPassword_Leave(object sender, EventArgs e)
        {
            pnlTbConfirmPassword.BackColor = Color.Transparent;
            if (tbConfirmPassword.Text == "")
            {
                tbConfirmPassword.UseSystemPasswordChar = false;
                tbConfirmPassword.Text = "Confirm Password";
                tbConfirmPassword.ForeColor = Color.CornflowerBlue;
            }
        }

        private void ExitPnlAddUser_Click(object sender, EventArgs e)
        {
            ClearPnlAddUser();
            this.Hide();
        }

        private void ExitPnlAddUser_MouseHover(object sender, EventArgs e)
        {
            exitPnlAddUser.ForeColor = Color.Silver;
        }

        private void ExitPnlAddUser_MouseLeave(object sender, EventArgs e)
        {
            exitPnlAddUser.ForeColor = Color.DimGray;
        }

        private void AddingUserForm_Shown(object sender, EventArgs e)
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
        private void Panel7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //*/
        private void TbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnPerformAdd.PerformClick();
            }
        }
    }
}
