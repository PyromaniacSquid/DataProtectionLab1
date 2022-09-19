
namespace WinFormsApp1
{
    partial class ChangePasswordForm
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
            this.ConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.ConfirmPasswordInput = new System.Windows.Forms.TextBox();
            this.NewPasswordInput = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.TextLabel = new System.Windows.Forms.Label();
            this.OldPasswordLabel = new System.Windows.Forms.Label();
            this.OldPasswordBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConfirmPasswordLabel
            // 
            this.ConfirmPasswordLabel.AutoSize = true;
            this.ConfirmPasswordLabel.Location = new System.Drawing.Point(26, 134);
            this.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel";
            this.ConfirmPasswordLabel.Size = new System.Drawing.Size(123, 15);
            this.ConfirmPasswordLabel.TabIndex = 0;
            this.ConfirmPasswordLabel.Text = "Подтвердите пароль:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(57, 99);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(91, 15);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "Новый пароль:";
            // 
            // ConfirmPasswordInput
            // 
            this.ConfirmPasswordInput.Location = new System.Drawing.Point(170, 131);
            this.ConfirmPasswordInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConfirmPasswordInput.Name = "ConfirmPasswordInput";
            this.ConfirmPasswordInput.PasswordChar = '*';
            this.ConfirmPasswordInput.Size = new System.Drawing.Size(110, 23);
            this.ConfirmPasswordInput.TabIndex = 4;
            // 
            // NewPasswordInput
            // 
            this.NewPasswordInput.Location = new System.Drawing.Point(170, 96);
            this.NewPasswordInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NewPasswordInput.Name = "NewPasswordInput";
            this.NewPasswordInput.PasswordChar = '*';
            this.NewPasswordInput.Size = new System.Drawing.Size(110, 23);
            this.NewPasswordInput.TabIndex = 3;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(66, 174);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(82, 22);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(181, 174);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(82, 22);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TextLabel
            // 
            this.TextLabel.Location = new System.Drawing.Point(24, 7);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(256, 44);
            this.TextLabel.TabIndex = 6;
            this.TextLabel.Text = "Пожалуйста, установите новый пароль для указанного пользователя";
            this.TextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OldPasswordLabel
            // 
            this.OldPasswordLabel.AutoSize = true;
            this.OldPasswordLabel.Location = new System.Drawing.Point(57, 62);
            this.OldPasswordLabel.Name = "OldPasswordLabel";
            this.OldPasswordLabel.Size = new System.Drawing.Size(92, 15);
            this.OldPasswordLabel.TabIndex = 7;
            this.OldPasswordLabel.Text = "Старый пароль";
            // 
            // OldPasswordBox
            // 
            this.OldPasswordBox.Location = new System.Drawing.Point(170, 59);
            this.OldPasswordBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OldPasswordBox.Name = "OldPasswordBox";
            this.OldPasswordBox.PasswordChar = '*';
            this.OldPasswordBox.Size = new System.Drawing.Size(110, 23);
            this.OldPasswordBox.TabIndex = 2;
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 216);
            this.Controls.Add(this.OldPasswordBox);
            this.Controls.Add(this.OldPasswordLabel);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewPasswordInput);
            this.Controls.Add(this.ConfirmPasswordInput);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.ConfirmPasswordLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ChangePasswordForm";
            this.Text = "Установка пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ConfirmPasswordLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox ConfirmPasswordInput;
        private System.Windows.Forms.TextBox NewPasswordInput;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label TextLabel;
        private System.Windows.Forms.Label OldPasswordLabel;
        private System.Windows.Forms.TextBox OldPasswordBox;
    }
}