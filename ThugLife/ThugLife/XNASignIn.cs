using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThugLife
{
    public partial class XNASignIn : Form
    {
        private bool ResultSet = false;
        public Player newPlayer = new Player();

        public XNASignIn()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            ResultSet = false;
            base.OnShown(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!ResultSet)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }

            base.OnClosed(e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ResultSet = true;

            ThugLifeDBEntities5 db = new ThugLifeDBEntities5();

            var vaic = from player in db.Player
                       where player.Username == this.inputUsername.Text
                       select player;
            if (vaic.Any())
            {
                newPlayer = vaic.Single();
            }
            else newPlayer.Username = "not";

            // check user here
            // an example that's very simple:
            if (newPlayer.Username == "not")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
            else if (this.inputUsername.Text == newPlayer.Username.Trim() && this.inputPassword.Text == newPlayer.Password.Trim())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResultSet = true;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private string username = string.Empty;
        public string Username { get { return username; } set { username = value; } }

        private string password = string.Empty;
        public string Password { get { return password; } set { password = value; } }

        private void InitializeComponent()
        {
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.inputPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inputUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnOK);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 106);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(5);
            this.panelButtons.Size = new System.Drawing.Size(384, 36);
            this.panelButtons.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(229, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(304, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.inputPassword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.inputUsername);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(384, 106);
            this.panel1.TabIndex = 5;
            // 
            // inputPassword
            // 
            this.inputPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputPassword.Location = new System.Drawing.Point(10, 76);
            this.inputPassword.Name = "inputPassword";
            this.inputPassword.PasswordChar = '*';
            this.inputPassword.Size = new System.Drawing.Size(364, 20);
            this.inputPassword.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label2.Size = new System.Drawing.Size(53, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password";
            // 
            // inputUsername
            // 
            this.inputUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputUsername.Location = new System.Drawing.Point(10, 33);
            this.inputUsername.Name = "inputUsername";
            this.inputUsername.Size = new System.Drawing.Size(364, 20);
            this.inputUsername.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label1.Size = new System.Drawing.Size(55, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // XNASignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 142);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XNASignIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThugLife";
            this.panelButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox inputPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputUsername;
        private System.Windows.Forms.Label label1;
    }
}