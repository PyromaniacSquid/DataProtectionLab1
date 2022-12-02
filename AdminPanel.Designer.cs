
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
            this.StagesTextBox = new System.Windows.Forms.TextBox();
            this.ChecksTextBox = new System.Windows.Forms.TextBox();
            this.LimitTextBox = new System.Windows.Forms.TextBox();
            this.stagesLabel = new System.Windows.Forms.Label();
            this.checksLabel = new System.Windows.Forms.Label();
            this.limitLabel = new System.Windows.Forms.Label();
            this.keyboardAuthCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CreateUserButton
            // 
            this.CreateUserButton.Location = new System.Drawing.Point(90, 114);
            this.CreateUserButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreateUserButton.Name = "CreateUserButton";
            this.CreateUserButton.Size = new System.Drawing.Size(182, 28);
            this.CreateUserButton.TabIndex = 0;
            this.CreateUserButton.Text = "Создать пользователя";
            this.CreateUserButton.UseVisualStyleBackColor = true;
            this.CreateUserButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserListBox
            // 
            this.UserListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserListBox.FormattingEnabled = true;
            this.UserListBox.Location = new System.Drawing.Point(185, 202);
            this.UserListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserListBox.Name = "UserListBox";
            this.UserListBox.Size = new System.Drawing.Size(133, 23);
            this.UserListBox.TabIndex = 1;
            this.UserListBox.SelectedIndexChanged += new System.EventHandler(this.UserListBox_SelectedIndexChanged);
            // 
            // newUserBox
            // 
            this.newUserBox.Location = new System.Drawing.Point(185, 71);
            this.newUserBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newUserBox.Name = "newUserBox";
            this.newUserBox.Size = new System.Drawing.Size(160, 23);
            this.newUserBox.TabIndex = 2;
            this.newUserBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newUserBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Имя нового пользователя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(56, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Создание нового пользователя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(68, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Управление пользователями";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Пользователь";
            // 
            // BlockedState
            // 
            this.BlockedState.AutoSize = true;
            this.BlockedState.Location = new System.Drawing.Point(32, 243);
            this.BlockedState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BlockedState.Name = "BlockedState";
            this.BlockedState.Size = new System.Drawing.Size(106, 19);
            this.BlockedState.TabIndex = 7;
            this.BlockedState.Text = "Заблокирован";
            this.BlockedState.UseVisualStyleBackColor = true;
            this.BlockedState.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // PWRestrictionsState
            // 
            this.PWRestrictionsState.AutoSize = true;
            this.PWRestrictionsState.Location = new System.Drawing.Point(185, 243);
            this.PWRestrictionsState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PWRestrictionsState.Name = "PWRestrictionsState";
            this.PWRestrictionsState.Size = new System.Drawing.Size(159, 19);
            this.PWRestrictionsState.TabIndex = 8;
            this.PWRestrictionsState.Text = "Ограничения на пароль";
            this.PWRestrictionsState.UseVisualStyleBackColor = true;
            this.PWRestrictionsState.CheckedChanged += new System.EventHandler(this.PWRestrictionsState_CheckedChanged);
            // 
            // DeleteUserButton
            // 
            this.DeleteUserButton.Location = new System.Drawing.Point(90, 434);
            this.DeleteUserButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteUserButton.Name = "DeleteUserButton";
            this.DeleteUserButton.Size = new System.Drawing.Size(183, 32);
            this.DeleteUserButton.TabIndex = 9;
            this.DeleteUserButton.Text = "Удалить пользователя";
            this.DeleteUserButton.UseVisualStyleBackColor = true;
            this.DeleteUserButton.Click += new System.EventHandler(this.DeleteUserButton_Click);
            // 
            // StagesTextBox
            // 
            this.StagesTextBox.Enabled = false;
            this.StagesTextBox.Location = new System.Drawing.Point(254, 314);
            this.StagesTextBox.Name = "StagesTextBox";
            this.StagesTextBox.Size = new System.Drawing.Size(90, 23);
            this.StagesTextBox.TabIndex = 10;
            this.StagesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StagesTextBox_KeyPress);
            this.StagesTextBox.Leave += new System.EventHandler(this.StagesTextBox_TextChanged);
            // 
            // ChecksTextBox
            // 
            this.ChecksTextBox.Enabled = false;
            this.ChecksTextBox.Location = new System.Drawing.Point(254, 358);
            this.ChecksTextBox.Name = "ChecksTextBox";
            this.ChecksTextBox.Size = new System.Drawing.Size(91, 23);
            this.ChecksTextBox.TabIndex = 11;
            this.ChecksTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChecksTextBox_KeyPress);
            this.ChecksTextBox.Leave += new System.EventHandler(this.ChecksTextBox_TextChanged);
            // 
            // LimitTextBox
            // 
            this.LimitTextBox.Enabled = false;
            this.LimitTextBox.Location = new System.Drawing.Point(254, 397);
            this.LimitTextBox.Name = "LimitTextBox";
            this.LimitTextBox.Size = new System.Drawing.Size(91, 23);
            this.LimitTextBox.TabIndex = 12;
            this.LimitTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LimitTextBox_KeyPress);
            this.LimitTextBox.Leave += new System.EventHandler(this.LimitTextBox_TextChanged);
            // 
            // stagesLabel
            // 
            this.stagesLabel.AutoSize = true;
            this.stagesLabel.Location = new System.Drawing.Point(32, 314);
            this.stagesLabel.Name = "stagesLabel";
            this.stagesLabel.Size = new System.Drawing.Size(123, 15);
            this.stagesLabel.TabIndex = 13;
            this.stagesLabel.Text = "Этапов тестирования";
            // 
            // checksLabel
            // 
            this.checksLabel.AutoSize = true;
            this.checksLabel.Location = new System.Drawing.Point(32, 358);
            this.checksLabel.Name = "checksLabel";
            this.checksLabel.Size = new System.Drawing.Size(120, 15);
            this.checksLabel.TabIndex = 14;
            this.checksLabel.Text = "Проверок при входе";
            // 
            // limitLabel
            // 
            this.limitLabel.AutoSize = true;
            this.limitLabel.Location = new System.Drawing.Point(32, 397);
            this.limitLabel.Name = "limitLabel";
            this.limitLabel.Size = new System.Drawing.Size(190, 15);
            this.limitLabel.TabIndex = 15;
            this.limitLabel.Text = "Допустимый порог непохожести";
            // 
            // keyboardAuthCheck
            // 
            this.keyboardAuthCheck.AutoSize = true;
            this.keyboardAuthCheck.Location = new System.Drawing.Point(32, 278);
            this.keyboardAuthCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.keyboardAuthCheck.Name = "keyboardAuthCheck";
            this.keyboardAuthCheck.Size = new System.Drawing.Size(214, 19);
            this.keyboardAuthCheck.TabIndex = 16;
            this.keyboardAuthCheck.Text = "Проверка клавиатурного почерка";
            this.keyboardAuthCheck.UseVisualStyleBackColor = true;
            this.keyboardAuthCheck.CheckedChanged += new System.EventHandler(this.keyboardAuthCheck_CheckedChanged);
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 477);
            this.Controls.Add(this.keyboardAuthCheck);
            this.Controls.Add(this.limitLabel);
            this.Controls.Add(this.checksLabel);
            this.Controls.Add(this.stagesLabel);
            this.Controls.Add(this.LimitTextBox);
            this.Controls.Add(this.ChecksTextBox);
            this.Controls.Add(this.StagesTextBox);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminPanel";
            this.Text = "Управление пользователями";
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
        private System.Windows.Forms.TextBox StagesTextBox;
        private System.Windows.Forms.TextBox ChecksTextBox;
        private System.Windows.Forms.TextBox LimitTextBox;
        private System.Windows.Forms.Label stagesLabel;
        private System.Windows.Forms.Label checksLabel;
        private System.Windows.Forms.Label limitLabel;
        private System.Windows.Forms.CheckBox keyboardAuthCheck;
    }
}