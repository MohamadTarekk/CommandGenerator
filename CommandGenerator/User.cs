using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CommandGenerator
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
            CommandsTabController c = new CommandsTabController();
            if (c.GetSheet(0) == null)
            {
                SheetsCB.Items.Add("null");
            }
        }

        // Global Variables
        public static int row;
        private bool editFlag = false;
        public static string oldUsername, oldPassword;
        public static string oldname,oldpass;

        Queries q = new Queries();
        public static string[] s = { "\\bin" };
        public static string database =
                       Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\Nokia.db";
        string path =
            "Data Source=" + Path.GetFullPath(database);


        //////////--------------------Users Tab--------------------\\\\\\\\\\

        private void User_Load(object sender, EventArgs e)
        {
            SQLiteConnection myconnection = new SQLiteConnection(path);
            myconnection.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconnection;
            cmd.CommandText = q.ShowUsers();
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(sdr);
                sdr.Close();
                myconnection.Close();
                usersGrid.DataSource = dt;
            }
            myconnection.Open();
            SQLiteCommand cmd1 = new SQLiteCommand();
            cmd1.Connection = myconnection;
            cmd1.CommandText = q.ShowNetworks();
            using (SQLiteDataReader sdr1 = cmd1.ExecuteReader())
            {
                DataTable dt1 = new DataTable();
                dt1.Load(sdr1);
                sdr1.Close();
                myconnection.Close();
                NetworkGrid.DataSource = dt1;

            }

            usersGrid.DefaultCellStyle.ForeColor = Color.Black;
            NetworkGrid.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void refreshUsers()
        {
            SQLiteConnection myconn = new SQLiteConnection(path);
            myconn.Open();
            DataTable show = new DataTable();
            SQLiteDataAdapter adap = new SQLiteDataAdapter(q.ShowUsers(), myconn);
            adap.Fill(show);
            try
            {
                usersGrid.DataSource = show;
            }
            catch(Exception ex)
            {
                usersGrid.Rows[row].Cells[2].Value = oldUsername;
                usersGrid.Rows[row].Cells[3].Value =Encrypt(oldPassword);
                Console.WriteLine(ex.Message);
            }
            myconn.Close();
        }

        private void usersGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                try
                {
                    string str = usersGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (MessageBox.Show("Are you sure you want to delete user '" + str + "' ?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SQLiteConnection myconnection = new SQLiteConnection(path);
                        myconnection.Open();
                        SQLiteCommand cmd = new SQLiteCommand("delete from usersTable where username = @username;", myconnection);
                        cmd.Parameters.Add(new SQLiteParameter("@username", str));
                        cmd.ExecuteNonQuery();
                        myconnection.Close();
                        refreshUsers();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            AddingUserForm auf = new AddingUserForm(usersGrid);
            auf.ShowDialog();
            auf.Dispose();
        }

        private void usersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.ColumnIndex != 1 && e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                row = e.RowIndex;
                oldUsername = usersGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                oldPassword = Decrypt(usersGrid.Rows[e.RowIndex].Cells[3].Value.ToString());
                usersGrid.Rows[e.RowIndex].Cells[3].Value = oldPassword;
                usersGrid.BeginEdit(true);
                editFlag = true;
            }
        }

        private bool checkUsername(string username)
        {
            SQLiteConnection myconnection = new SQLiteConnection(path);
            myconnection.Open();
            string query = "select count(*) from usersTable where username='" + username + "';";
            SQLiteCommand cmd = new SQLiteCommand(query, myconnection);
            cmd.Parameters.Add(new SQLiteParameter("@username", username));
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 0)
                return false;
            if(username != oldUsername)
                MessageBox.Show("This username is already in use.", "Can not edit.");
            return true;
        }

        private void usersGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string newUsername = usersGrid.Rows[row].Cells[2].Value.ToString();
            string newPassword = usersGrid.Rows[row].Cells[3].Value.ToString();
            // MessageBox.Show("new username " + newUsername + " oldUsername " + oldUsername);
            // MessageBox.Show("new password " + newPassword + " oldpassword " + oldPassword);
            if (editFlag && (!checkUsername(newUsername) || newUsername != oldUsername || newPassword != oldPassword) &&
                    (MessageBox.Show("Are you sure you want to save edits?",
                        "Confirm Edit", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                editFlag = false;
                newPassword = Encrypt(newPassword);
                SQLiteConnection myconnection = new SQLiteConnection(path);
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand("update usersTable set username = @username, password = @password where username = @oldUsername;", myconnection);
                cmd.Parameters.Add(new SQLiteParameter("@username", newUsername));
                cmd.Parameters.Add(new SQLiteParameter("@password", newPassword));
                cmd.Parameters.Add(new SQLiteParameter("@oldUsername", oldUsername));
                cmd.ExecuteNonQuery();
                myconnection.Close();
            }
            refreshUsers();
        }

        //* Encryption and Decryption Functions
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            AddingNetworkForm anf = new AddingNetworkForm(NetworkGrid);
            anf.ShowDialog();
            anf.Dispose();
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        //////////--------------------Networks Tab--------------------\\\\\\\\\\

        private void NetworkGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                try
                {
                    string str = NetworkGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (MessageBox.Show("Are you sure you want to delete user '" + str + "' ?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SQLiteConnection myconnection = new SQLiteConnection(path);
                        myconnection.Open();
                        SQLiteCommand cmd = new SQLiteCommand(q.DeleteNetwork(str), myconnection);
                        cmd.ExecuteNonQuery();
                        myconnection.Close();
                        ListingNetworks();
                    }
                }
                catch (Exception ex)
                {
                    FileParser.LogException(ex.Message);
                }
            }
        }
        private void ListingNetworks()
        {
            SQLiteConnection myconn = new SQLiteConnection(path);
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

        private void NetworkGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.ColumnIndex != 1 && e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                row = e.RowIndex;
                String oldIp = NetworkGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                String oldusername = NetworkGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                oldpass = Decrypt(NetworkGrid.Rows[e.RowIndex].Cells[4].Value.ToString());
                 oldname = NetworkGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
               NetworkGrid.Rows[e.RowIndex].Cells[4].Value = oldpass;
                NetworkGrid.BeginEdit(true);
            }
        }

        private void NetworkGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string Ip = NetworkGrid.Rows[row].Cells[2].Value.ToString();
            string Username = NetworkGrid.Rows[row].Cells[3].Value.ToString();
            string Password = NetworkGrid.Rows[row].Cells[4].Value.ToString();
            string Name = NetworkGrid.Rows[row].Cells[5].Value.ToString();

            if (MessageBox.Show("Are you sure you want to save edits?", "Confirm Edit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Password = Encrypt(Password);
                SQLiteConnection myconnection = new SQLiteConnection(path);
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand("update Network set Ip = @Ip , username=@username, password= @password , Name = @Name where Name=@oldname  ;", myconnection);
                cmd.Parameters.Add(new SQLiteParameter("@username", Username));
                cmd.Parameters.Add(new SQLiteParameter("@password", Password));
                cmd.Parameters.Add(new SQLiteParameter("@Ip", Ip));
                cmd.Parameters.Add(new SQLiteParameter("@Name", Name));
                cmd.Parameters.Add(new SQLiteParameter("@oldname",oldname));
                cmd.ExecuteNonQuery();
                myconnection.Close();
            }
            ListingNetworks();
        }

        //////////--------------------Commands Tab--------------------\\\\\\\\\\

        CommandsTabController commandsTabController = new CommandsTabController();

        private void SheetsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCmdGrid();
        }

        public void RefreshCmdGrid()
        {
            CmdGrid.DataSource = commandsTabController.GetSheet(SheetsCB.SelectedIndex);
            CmdGrid.DefaultCellStyle.ForeColor = Color.Black;
            CmdGrid.DefaultCellStyle.BackColor = Color.AliceBlue;
            CmdGrid.BackgroundColor = Color.AliceBlue;
            SetGridColors(3, "Status", "Available", "Doesn't Exist");
            SetGridColors(4, "Excution", "true", "false");
        }

        public void SetGridColors(int ColumnIndex, string ColumnName, string GreenState, string RedState)
        {
            if (CmdGrid.Columns.Contains(ColumnName))
            {
                for (int itr = 0; itr < CmdGrid.Rows.Count; itr++)
                {
                    if (CmdGrid.Rows[itr].Cells[ColumnIndex].ToString().Equals(GreenState))
                        CmdGrid.Rows[itr].Cells[ColumnIndex].Style.BackColor = Color.LawnGreen;
                    else if (CmdGrid.Rows[itr].Cells[ColumnIndex].ToString().Equals(RedState))
                        CmdGrid.Rows[itr].Cells[ColumnIndex].Style.BackColor = Color.Red;
                    else
                        CmdGrid.Rows[itr].Cells[ColumnIndex].Style.BackColor = Color.Yellow;
                }
            }
        }

        private void CmdGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                commandsTabController.RefreshConnection(CmdGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
                RefreshCmdGrid();
            }
        }

        private void BrowzeBtn_Click(object sender, EventArgs e)
        {
            commandsTabController.BrowzeButtonClicked(SheetsCB);
            if (commandsTabController.GetSheet(0) != null)
                ExcuteBtn.Enabled = true;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            commandsTabController.ClearGrid();
            CmdGrid.DataSource = null;
            SheetsCB.Text = null;
            SheetsCB.Items.Clear();
            ExcuteBtn.Enabled = false;
        }

        private void ExcuteBtn_Click(object sender, EventArgs e)
        {
            commandsTabController.ExcuteButtonPressed();
            RefreshCmdGrid();
        }
    }
}
