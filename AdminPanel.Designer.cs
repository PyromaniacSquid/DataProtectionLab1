﻿
namespace WinFormsApp1
{
    partial class AdminPanel
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
            this.CreateUserButton = new System.Windows.Forms.Button();
            this.UserListBox = new System.Windows.Forms.ComboBox();
            this.newUserBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BlockedState = new System.Windows.Forms.CheckBox();
            this.PWRestrictionsState = new System.Windows.Forms.CheckBox();
            this.DeleteUserButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateUserButton
            // 
            this.CreateUserButton.Location = new System.Drawing.Point(103, 152);
            this.CreateUserButton.Name = "CreateUserButton";
            this.CreateUserButton.Size = new System.Drawing.Size(208, 37);
            this.CreateUserButton.TabIndex = 0;
            this.CreateUserButton.Text = "Создать пользователя";
            this.CreateUserButton.UseVisualStyleBackColor = true;
            this.CreateUserButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserListBox
            // 
            this.UserListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserListBox.FormattingEnabled = true;
            this.UserListBox.Location = new System.Drawing.Point(211, 269);
            this.UserListBox.Name = "UserListBox";
            this.UserListBox.Size = new System.Drawing.Size(151, 28);
            this.UserListBox.TabIndex = 1;
            this.UserListBox.SelectedIndexChanged += new System.EventHandler(this.UserListBox_SelectedIndexChanged);
            // 
            // newUserBox
            // 
            this.newUserBox.Location = new System.Drawing.Point(211, 95);
            this.newUserBox.Name = "newUserBox";
            this.newUserBox.Size = new System.Drawing.Size(182, 27);
            this.newUserBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Имя нового пользователя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(64, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Создание нового пользователя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(78, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Управление пользователями";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Пользователь";
            // 
            // BlockedState
            // 
            this.BlockedState.AutoSize = true;
            this.BlockedState.Location = new System.Drawing.Point(36, 324);
            this.BlockedState.Name = "BlockedState";
            this.BlockedState.Size = new System.Drawing.Size(132, 24);
            this.BlockedState.TabIndex = 7;
            this.BlockedState.Text = "Заблокирован";
            this.BlockedState.UseVisualStyleBackColor = true;
            this.BlockedState.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // PWRestrictionsState
            // 
            this.PWRestrictionsState.AutoSize = true;
            this.PWRestrictionsState.Location = new System.Drawing.Point(211, 324);
            this.PWRestrictionsState.Name = "PWRestrictionsState";
            this.PWRestrictionsState.Size = new System.Drawing.Size(201, 24);
            this.PWRestrictionsState.TabIndex = 8;
            this.PWRestrictionsState.Text = "Ограничения на пароль";
            this.PWRestrictionsState.UseVisualStyleBackColor = true;
            this.PWRestrictionsState.CheckedChanged += new System.EventHandler(this.PWRestrictionsState_CheckedChanged);
            // 
            // DeleteUserButton
            // 
            this.DeleteUserButton.Location = new System.Drawing.Point(103, 383);
            this.DeleteUserButton.Name = "DeleteUserButton";
            this.DeleteUserButton.Size = new System.Drawing.Size(208, 37);
            this.DeleteUserButton.TabIndex = 9;
            this.DeleteUserButton.Text = "Удалить пользователя";
            this.DeleteUserButton.UseVisualStyleBackColor = true;
            this.DeleteUserButton.Click += new System.EventHandler(this.DeleteUserButton_Click);
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 446);
            this.Controls.Add(this.DeleteUserButton);
            this.Controls.Add(this.PWRestrictionsState);
            this.Controls.Add(this.BlockedState);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newUserBox);
            this.Controls.Add(this.UserListBox);
            this.Controls.Add(this.CreateUserButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminPanel";
            this.Text = "AdminPanel";
            this.Load += new System.EventHandler(this.AdminPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateUserButton;
        private System.Windows.Forms.ComboBox UserListBox;
        private System.Windows.Forms.TextBox newUserBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox BlockedState;
        private System.Windows.Forms.CheckBox PWRestrictionsState;
        private System.Windows.Forms.Button DeleteUserButton;
    }
}