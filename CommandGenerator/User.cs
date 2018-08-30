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
        }

        // Global Variables
        public static int row;
        private bool editUserFlag;
        private bool editNetworkFlag;
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
            SQLiteCommand cmd = new SQLiteCommand
            {
                Connection = myconnection,
                CommandText = q.ShowUsers()
            };
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(sdr);
                sdr.Close();
                myconnection.Close();
                usersGrid.DataSource = dt;
            }
            myconnection.Open();
            SQLiteCommand cmd1 = new SQLiteCommand
            {
                Connection = myconnection,
                CommandText = q.ShowNetworks()
            };
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

        private void RefreshUsers()
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

        private void UsersGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                        RefreshUsers();
                    } 
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private void UsersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.ColumnIndex != 1 && e.ColumnIndex != -1 && e.RowIndex != -1 && !editUserFlag)
            {
                row = e.RowIndex;
                oldUsername = usersGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                oldPassword = Decrypt(usersGrid.Rows[e.RowIndex].Cells[3].Value.ToString());
                usersGrid.Rows[e.RowIndex].Cells[3].Value = oldPassword;
                usersGrid.BeginEdit(true);
                editUserFlag = true;
            }
        }

        private bool CheckUsername(string username)
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

        private void UsersGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string newUsername = usersGrid.Rows[row].Cells[2].Value.ToString();
            string newPassword = usersGrid.Rows[row].Cells[3].Value.ToString();
            if (editUserFlag && (!CheckUsername(newUsername) || newUsername != oldUsername || newPassword != oldPassword) &&
                    (MessageBox.Show("Are you sure you want to save edits?",
                        "Confirm Edit", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
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
            editUserFlag = false;
            RefreshUsers();
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
                    if (MessageBox.Show("Are you sure you want to delete network element '" + str + "' ?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            try
            {
                NetworkGrid.DataSource = show;
            }
            catch (Exception ex)
            {
                NetworkGrid.Rows[row].Cells[3].Value = oldname;
                NetworkGrid.Rows[row].Cells[4].Value = Encrypt(oldpass);
                Console.WriteLine(ex.Message);
            }
            myconn.Close();
        }

        private void NetworkGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.ColumnIndex != 1 && e.ColumnIndex != -1 && e.RowIndex != -1 && !editNetworkFlag)
            {
                row = e.RowIndex;
                oldpass = Decrypt(NetworkGrid.Rows[e.RowIndex].Cells[4].Value.ToString());
                oldname = NetworkGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                NetworkGrid.Rows[e.RowIndex].Cells[4].Value = oldpass;
                NetworkGrid.BeginEdit(true);
                editNetworkFlag = true;
            }
        }

        private void NetworkGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string Ip = NetworkGrid.Rows[row].Cells[2].Value.ToString();
            string Username = NetworkGrid.Rows[row].Cells[3].Value.ToString();
            string Password = NetworkGrid.Rows[row].Cells[4].Value.ToString();
            string Name = NetworkGrid.Rows[row].Cells[5].Value.ToString();

            if (!editNetworkFlag && MessageBox.Show("Are you sure you want to save edits?", "Confirm Edit", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            editNetworkFlag = false;
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

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            AddingUserForm auf = new AddingUserForm(usersGrid);
            auf.ShowDialog();
            auf.Dispose();
        }

        private void BtnAddNetwork_Click(object sender, EventArgs e)
        {
            AddingNetworkForm anf = new AddingNetworkForm(NetworkGrid);
            anf.ShowDialog();
            anf.Dispose();
        }

        private void CellPaint(string imageName, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = 16;
                var h = 16;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                string[] s = { "\\bin" };
                string removeImage =
                       Application.StartupPath.Split(s, StringSplitOptions.None)[0] + "\\Data\\" + imageName + ".png";
                string path =
                    "Data Source=" + Path.GetFullPath(database);

                Image myImage = Image.FromFile(removeImage);
                e.Graphics.DrawImage(myImage, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void UsersGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            CellPaint("removeIcon", e);
        }

        private void CmdGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            CellPaint("refreshIcon", e);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AddingNetworkForm anf = new AddingNetworkForm(NetworkGrid);
            anf.ShowDialog();
            anf.Dispose();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            //FileParser.CreateZip();
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
