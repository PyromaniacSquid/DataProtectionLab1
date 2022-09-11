
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
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameStaticBox = new System.Windows.Forms.TextBox();
            this.NewPasswordInput = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.TextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(27, 84);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(142, 20);
            this.UsernameLabel.TabIndex = 0;
            this.UsernameLabel.Text = "Имя пользователя:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(54, 137);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(115, 20);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "Новый пароль:";
            // 
            // UsernameStaticBox
            // 
            this.UsernameStaticBox.Location = new System.Drawing.Point(195, 84);
            this.UsernameStaticBox.Name = "UsernameStaticBox";
            this.UsernameStaticBox.ReadOnly = true;
            this.UsernameStaticBox.Size = new System.Drawing.Size(125, 27);
            this.UsernameStaticBox.TabIndex = 2;
            // 
            // NewPasswordInput
            // 
            this.NewPasswordInput.Location = new System.Drawing.Point(195, 137);
            this.NewPasswordInput.Name = "NewPasswordInput";
            this.NewPasswordInput.Size = new System.Drawing.Size(125, 27);
            this.NewPasswordInput.TabIndex = 3;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(75, 194);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(94, 29);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(207, 194);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(94, 29);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // TextLabel
            // 
            this.TextLabel.Location = new System.Drawing.Point(27, 9);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(293, 58);
            this.TextLabel.TabIndex = 6;
            this.TextLabel.Text = "Пожалуйста, установите новый пароль для указанного пользователя";
            this.TextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 247);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewPasswordInput);
            this.Controls.Add(this.UsernameStaticBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Name = "ChangePasswordForm";
            this.Text = "Установка пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox UsernameStaticBox;
        private System.Windows.Forms.TextBox NewPasswordInput;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label TextLabel;
    }
}