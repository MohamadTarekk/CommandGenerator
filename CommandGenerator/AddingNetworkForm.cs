using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandGenerator
{
    public partial class AddingNetworkForm : Form
    {
        public AddingNetworkForm()
        {
            InitializeComponent();
        }

        private void exitPnlAddUser_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void tbIP_Enter(object sender, EventArgs e)
        {
            pnlTbIP.BackColor = Color.FromArgb(26, 73, 148);
            if (tbIP.Text == "IP")
            {
                tbIP.Text = "";
                tbIP.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbIP_Leave(object sender, EventArgs e)
        {
            pnlTbIP.BackColor = Color.Transparent;
            if (tbIP.Text == "")
            {
                tbIP.Text = "IP";
                tbIP.ForeColor = Color.CornflowerBlue;
            }
        }

        private void tbNetwork_Enter(object sender, EventArgs e)
        {
            pnlTbNetwork.BackColor = Color.FromArgb(26, 73, 148);
            if (tbNetwork.Text == "Network Name")
            {
                tbNetwork.Text = "";
                tbNetwork.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbNetwork_Leave(object sender, EventArgs e)
        {
            pnlTbNetwork.BackColor = Color.Transparent;
            if (tbNetwork.Text == "")
            {
                tbNetwork.Text = "Network Name";
                tbNetwork.ForeColor = Color.CornflowerBlue;
            }
        }

        private void tbUsername_Enter(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.FromArgb(26, 73, 148);
            if (tbUsername.Text == "Username")
            {
                tbUsername.Text = "";
                tbUsername.ForeColor = Color.FromArgb(26, 73, 148);
            }
        }

        private void tbUsername_Leave(object sender, EventArgs e)
        {
            pnlTbUsername.BackColor = Color.Transparent;
            if (tbUsername.Text == "")
            {
                tbUsername.Text = "Username";
                tbUsername.ForeColor = Color.CornflowerBlue;
            }
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            pnlTbPassword.BackColor = Color.FromArgb(26, 73, 148);
            if (tbPassword.Text == "Password")
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

        }

        private void AddingNetworkForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }
    }
}
