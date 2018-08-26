using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CommandGenerator
{
    public partial class AddingUserForm : Form
    {
        DataGridView usersGrid = new DataGridView();
        private bool usernameEntered;

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

        public AddingUserForm(DataGridView usersGrid)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.usersGrid = usersGrid;
        }

        public bool checkPnlAddUser()
        {
            if (tbUsername.Text != "" && tbPassword.Text != "" && tbConfirmPassword.Text != ""
                && tbPassword.Text == tbConfirmPassword.Text && usernameEntered
                && tbPassword.UseSystemPasswordChar && tbConfirmPassword.UseSystemPasswordChar)
                return true;
            return false;
        }

        public void clearPnlAddUser()
        {
            tbUsername.Text = "Username";
            tbPassword.Text = "Password";
            tbConfirmPassword.Text = "Confirm Password";
            usernameEntered = false;
            tbPassword.UseSystemPasswordChar = false;
            tbConfirmPassword.UseSystemPasswordChar = false;
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

        private void refreshUsers()
        {
            SQLiteConnection myconn = new SQLiteConnection("Data Source = D:\\Mahmoud\\Programs\\SQLite\\databases\\testdb.db3;Version=3");
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
            }
            myconn.Close();
        }

        private void btnPerformAdd_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = User.Encrypt(tbPassword.Text);
            if (checkPnlAddUser())
            {
                SQLiteConnection myconnection = new SQLiteConnection("Data Source = D:\\Mahmoud\\Programs\\SQLite\\databases\\testdb.db3;Version=3");
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
                    refreshUsers();
                    clearPnlAddUser();
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

        private void tbConfirmPassword_Enter(object sender, EventArgs e)
        {
            pnlTbConfirmPassword.BackColor = Color.FromArgb(26, 73, 148);
            if (tbConfirmPassword.Text == "Confirm Password" && !tbConfirmPassword.UseSystemPasswordChar)
            {
                tbConfirmPassword.Text = "";
                tbConfirmPassword.UseSystemPasswordChar = true;
                tbConfirmPassword.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbConfirmPassword_Leave(object sender, EventArgs e)
        {
            pnlTbConfirmPassword.BackColor = Color.Transparent;
            if (tbConfirmPassword.Text == "")
            {
                tbConfirmPassword.UseSystemPasswordChar = false;
                tbConfirmPassword.Text = "Confirm Password";
                tbConfirmPassword.ForeColor = Color.CornflowerBlue;
            }
        }

        private void exitPnlAddUser_Click(object sender, EventArgs e)
        {
            clearPnlAddUser();
            this.Hide();
        }

        private void exitPnlAddUser_MouseHover(object sender, EventArgs e)
        {
            exitPnlAddUser.ForeColor = Color.Silver;
        }

        private void exitPnlAddUser_MouseLeave(object sender, EventArgs e)
        {
            exitPnlAddUser.ForeColor = Color.DimGray;
        }

        private void AddingUserForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }
    }
}
