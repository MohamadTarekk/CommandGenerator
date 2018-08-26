﻿using System;
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
        // Global Variables
        public static int row;
        private bool editFlag = false;
        public static string oldUsername, oldPassword;
        public string path = "Data Source=D:\\Mahmoud\\Programs\\SQLite\\databases\\testdb.db3;Version=3";

        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            SQLiteConnection myconnection = new SQLiteConnection(path);
            myconnection.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconnection;
            cmd.CommandText = "Select * from usersTable";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(sdr);
                sdr.Close();
                myconnection.Close();
                usersGrid.DataSource = dt;
            }
            usersGrid.DefaultCellStyle.ForeColor = Color.Black;
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
            SQLiteDataAdapter adap = new SQLiteDataAdapter("Select * from usersTable", myconn);
            adap.Fill(show);
            try
            {
                usersGrid.DataSource = show;
            }
            catch(Exception ex)
            {
                usersGrid.Rows[row].Cells[2].Value = oldUsername;
                usersGrid.Rows[row].Cells[3].Value =Encrypt(oldPassword);
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
                catch (Exception ex) { }
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
            if (editFlag && !checkUsername(newUsername) && (newUsername != oldUsername || newPassword != oldPassword) &&
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
            AddingNetworkForm anf = new AddingNetworkForm();
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

        //////////--------------------Commands Tab--------------------\\\\\\\\\\

        CommandsTabController commandsTabController = new CommandsTabController();

        private void SheetsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdGrid.DataSource = commandsTabController.GetSheet(SheetsCB.SelectedIndex);
            cmdGrid.DefaultCellStyle.ForeColor = Color.Black;

        }

        private void BrowzeBtn_Click(object sender, EventArgs e)
        {
            cmdGrid.DataSource = null;
            commandsTabController.BrowzeButtonClicked(SheetsCB);
        }

    }
}
