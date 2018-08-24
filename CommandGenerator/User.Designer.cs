namespace CommandGenerator
{
    partial class User
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Commands = new System.Windows.Forms.TabPage();
            this.pnlCommands = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ManageUsers = new System.Windows.Forms.TabPage();
            this.pnlManageUsers = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.usersGB = new System.Windows.Forms.GroupBox();
            this.btnAdd = new CommandGenerator.CircularButton();
            this.usersGrid = new System.Windows.Forms.DataGridView();
            this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.addGB = new System.Windows.Forms.GroupBox();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btnCancelAdd = new System.Windows.Forms.Button();
            this.btnPerformAdd = new System.Windows.Forms.Button();
            this.tbConfirmPassword = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ManageNetwork = new System.Windows.Forms.TabPage();
            this.pnlManageNetwork = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.Commands.SuspendLayout();
            this.pnlCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.ManageUsers.SuspendLayout();
            this.pnlManageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.usersGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersGrid)).BeginInit();
            this.addGB.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            this.ManageNetwork.SuspendLayout();
            this.pnlManageNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Commands);
            this.tabControl1.Controls.Add(this.ManageUsers);
            this.tabControl1.Controls.Add(this.ManageNetwork);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1006, 721);
            this.tabControl1.TabIndex = 2;
            // 
            // Commands
            // 
            this.Commands.Controls.Add(this.pnlCommands);
            this.Commands.Location = new System.Drawing.Point(4, 25);
            this.Commands.Name = "Commands";
            this.Commands.Padding = new System.Windows.Forms.Padding(3);
            this.Commands.Size = new System.Drawing.Size(998, 692);
            this.Commands.TabIndex = 0;
            this.Commands.Text = "Commands";
            this.Commands.UseVisualStyleBackColor = true;
            // 
            // pnlCommands
            // 
            this.pnlCommands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCommands.BackgroundImage")));
            this.pnlCommands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCommands.Controls.Add(this.pictureBox2);
            this.pnlCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCommands.Location = new System.Drawing.Point(3, 3);
            this.pnlCommands.Name = "pnlCommands";
            this.pnlCommands.Size = new System.Drawing.Size(992, 686);
            this.pnlCommands.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(27, 584);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 70);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // ManageUsers
            // 
            this.ManageUsers.Controls.Add(this.pnlManageUsers);
            this.ManageUsers.Location = new System.Drawing.Point(4, 25);
            this.ManageUsers.Name = "ManageUsers";
            this.ManageUsers.Padding = new System.Windows.Forms.Padding(3);
            this.ManageUsers.Size = new System.Drawing.Size(998, 692);
            this.ManageUsers.TabIndex = 1;
            this.ManageUsers.Text = "Manage Users";
            this.ManageUsers.UseVisualStyleBackColor = true;
            // 
            // pnlManageUsers
            // 
            this.pnlManageUsers.BackColor = System.Drawing.Color.Transparent;
            this.pnlManageUsers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlManageUsers.BackgroundImage")));
            this.pnlManageUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlManageUsers.Controls.Add(this.pictureBox1);
            this.pnlManageUsers.Controls.Add(this.usersGB);
            this.pnlManageUsers.Controls.Add(this.addGB);
            this.pnlManageUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManageUsers.Location = new System.Drawing.Point(3, 3);
            this.pnlManageUsers.Name = "pnlManageUsers";
            this.pnlManageUsers.Size = new System.Drawing.Size(992, 686);
            this.pnlManageUsers.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 584);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // usersGB
            // 
            this.usersGB.Controls.Add(this.btnAdd);
            this.usersGB.Controls.Add(this.usersGrid);
            this.usersGB.ForeColor = System.Drawing.Color.White;
            this.usersGB.Location = new System.Drawing.Point(326, 3);
            this.usersGB.Name = "usersGB";
            this.usersGB.Size = new System.Drawing.Size(661, 678);
            this.usersGB.TabIndex = 5;
            this.usersGB.TabStop = false;
            this.usersGB.Text = "Users";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(592, 613);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(52, 48);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // usersGrid
            // 
            this.usersGrid.AllowUserToAddRows = false;
            this.usersGrid.AllowUserToDeleteRows = false;
            this.usersGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usersGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.usersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Remove});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.usersGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.usersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.usersGrid.Location = new System.Drawing.Point(3, 18);
            this.usersGrid.Name = "usersGrid";
            this.usersGrid.RowTemplate.Height = 24;
            this.usersGrid.Size = new System.Drawing.Size(655, 657);
            this.usersGrid.TabIndex = 0;
            this.usersGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.usersGrid_CellContentClick);
            this.usersGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.usersGrid_CellDoubleClick);
            this.usersGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.usersGrid_CellEndEdit);
            // 
            // Remove
            // 
            this.Remove.HeaderText = "Remove";
            this.Remove.Name = "Remove";
            this.Remove.Text = "";
            // 
            // addGB
            // 
            this.addGB.Controls.Add(this.pnlAdd);
            this.addGB.ForeColor = System.Drawing.Color.White;
            this.addGB.Location = new System.Drawing.Point(326, 3);
            this.addGB.Name = "addGB";
            this.addGB.Size = new System.Drawing.Size(661, 678);
            this.addGB.TabIndex = 7;
            this.addGB.TabStop = false;
            this.addGB.Text = "Add User";
            this.addGB.Visible = false;
            // 
            // pnlAdd
            // 
            this.pnlAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(73)))), ((int)(((byte)(148)))));
            this.pnlAdd.Controls.Add(this.btnCancelAdd);
            this.pnlAdd.Controls.Add(this.btnPerformAdd);
            this.pnlAdd.Controls.Add(this.tbConfirmPassword);
            this.pnlAdd.Controls.Add(this.tbPassword);
            this.pnlAdd.Controls.Add(this.label10);
            this.pnlAdd.Controls.Add(this.tbUsername);
            this.pnlAdd.Controls.Add(this.label9);
            this.pnlAdd.Controls.Add(this.label8);
            this.pnlAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAdd.Location = new System.Drawing.Point(3, 18);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(655, 657);
            this.pnlAdd.TabIndex = 6;
            // 
            // btnCancelAdd
            // 
            this.btnCancelAdd.BackColor = System.Drawing.Color.Firebrick;
            this.btnCancelAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelAdd.ForeColor = System.Drawing.Color.White;
            this.btnCancelAdd.Location = new System.Drawing.Point(296, 343);
            this.btnCancelAdd.Name = "btnCancelAdd";
            this.btnCancelAdd.Size = new System.Drawing.Size(124, 34);
            this.btnCancelAdd.TabIndex = 5;
            this.btnCancelAdd.Text = "Cancel";
            this.btnCancelAdd.UseVisualStyleBackColor = false;
            this.btnCancelAdd.Click += new System.EventHandler(this.btnCancelAdd_Click);
            // 
            // btnPerformAdd
            // 
            this.btnPerformAdd.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnPerformAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerformAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerformAdd.ForeColor = System.Drawing.Color.White;
            this.btnPerformAdd.Location = new System.Drawing.Point(428, 343);
            this.btnPerformAdd.Name = "btnPerformAdd";
            this.btnPerformAdd.Size = new System.Drawing.Size(124, 34);
            this.btnPerformAdd.TabIndex = 4;
            this.btnPerformAdd.Text = "Add";
            this.btnPerformAdd.UseVisualStyleBackColor = false;
            this.btnPerformAdd.Click += new System.EventHandler(this.btnPerformAdd_Click_1);
            // 
            // tbConfirmPassword
            // 
            this.tbConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConfirmPassword.Location = new System.Drawing.Point(296, 302);
            this.tbConfirmPassword.Name = "tbConfirmPassword";
            this.tbConfirmPassword.Size = new System.Drawing.Size(256, 28);
            this.tbConfirmPassword.TabIndex = 3;
            this.tbConfirmPassword.UseSystemPasswordChar = true;
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(296, 262);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(256, 28);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(95, 305);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 24);
            this.label10.TabIndex = 3;
            this.label10.Text = "Confirm Password";
            // 
            // tbUsername
            // 
            this.tbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUsername.Location = new System.Drawing.Point(296, 223);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(256, 28);
            this.tbUsername.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(137, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 24);
            this.label9.TabIndex = 4;
            this.label9.Text = "Password";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(133, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 24);
            this.label8.TabIndex = 5;
            this.label8.Text = "Username";
            // 
            // ManageNetwork
            // 
            this.ManageNetwork.Controls.Add(this.pnlManageNetwork);
            this.ManageNetwork.Location = new System.Drawing.Point(4, 25);
            this.ManageNetwork.Name = "ManageNetwork";
            this.ManageNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.ManageNetwork.Size = new System.Drawing.Size(998, 692);
            this.ManageNetwork.TabIndex = 2;
            this.ManageNetwork.Text = "Manage Network";
            this.ManageNetwork.UseVisualStyleBackColor = true;
            // 
            // pnlManageNetwork
            // 
            this.pnlManageNetwork.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlManageNetwork.BackgroundImage")));
            this.pnlManageNetwork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlManageNetwork.Controls.Add(this.pictureBox3);
            this.pnlManageNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManageNetwork.Location = new System.Drawing.Point(3, 3);
            this.pnlManageNetwork.Name = "pnlManageNetwork";
            this.pnlManageNetwork.Size = new System.Drawing.Size(992, 686);
            this.pnlManageNetwork.TabIndex = 0;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(27, 584);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(30);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(200, 70);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "User";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Command Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.User_FormClosing);
            this.Load += new System.EventHandler(this.User_Load);
            this.tabControl1.ResumeLayout(false);
            this.Commands.ResumeLayout(false);
            this.pnlCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ManageUsers.ResumeLayout(false);
            this.pnlManageUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.usersGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usersGrid)).EndInit();
            this.addGB.ResumeLayout(false);
            this.pnlAdd.ResumeLayout(false);
            this.pnlAdd.PerformLayout();
            this.ManageNetwork.ResumeLayout(false);
            this.pnlManageNetwork.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Commands;
        private System.Windows.Forms.TabPage ManageUsers;
        private System.Windows.Forms.Panel pnlManageUsers;
        private System.Windows.Forms.GroupBox usersGB;
        private System.Windows.Forms.DataGridView usersGrid;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CircularButton btnAdd;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Button btnPerformAdd;
        private System.Windows.Forms.TextBox tbConfirmPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox addGB;
        private System.Windows.Forms.Button btnCancelAdd;
        private System.Windows.Forms.DataGridViewButtonColumn Remove;
        private System.Windows.Forms.Panel pnlCommands;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TabPage ManageNetwork;
        private System.Windows.Forms.Panel pnlManageNetwork;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

