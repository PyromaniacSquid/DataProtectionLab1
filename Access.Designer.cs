namespace WinFormsApp1
{
    partial class Access
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
            this.PasswordInput = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.PasswordConfirmLabel = new System.Windows.Forms.Label();
            this.PasswordConfirmInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PasswordInput
            // 
            this.PasswordInput.Location = new System.Drawing.Point(272, 84);
            this.PasswordInput.Name = "PasswordInput";
            this.PasswordInput.PasswordChar = '*';
            this.PasswordInput.Size = new System.Drawing.Size(197, 23);
            this.PasswordInput.TabIndex = 0;
            this.PasswordInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordInput_KeyDown);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PasswordLabel.Location = new System.Drawing.Point(43, 85);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(120, 19);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "Парольная фраза";
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WelcomeLabel.Location = new System.Drawing.Point(109, 25);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(327, 45);
            this.WelcomeLabel.TabIndex = 2;
            this.WelcomeLabel.Text = "Установите парольную фразу для шифрования пользовательских данных";
            this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OKButton.Location = new System.Drawing.Point(170, 187);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(179, 49);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "Установить фразу";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // PasswordConfirmLabel
            // 
            this.PasswordConfirmLabel.AutoSize = true;
            this.PasswordConfirmLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PasswordConfirmLabel.Location = new System.Drawing.Point(43, 136);
            this.PasswordConfirmLabel.Name = "PasswordConfirmLabel";
            this.PasswordConfirmLabel.Size = new System.Drawing.Size(194, 19);
            this.PasswordConfirmLabel.TabIndex = 4;
            this.PasswordConfirmLabel.Text = "Повторите парольную фразу";
            // 
            // PasswordConfirmInput
            // 
            this.PasswordConfirmInput.Location = new System.Drawing.Point(272, 132);
            this.PasswordConfirmInput.Name = "PasswordConfirmInput";
            this.PasswordConfirmInput.PasswordChar = '*';
            this.PasswordConfirmInput.Size = new System.Drawing.Size(197, 23);
            this.PasswordConfirmInput.TabIndex = 1;
            this.PasswordConfirmInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordConfirmInput_KeyDown);
            // 
            // Access
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 267);
            this.Controls.Add(this.PasswordConfirmInput);
            this.Controls.Add(this.PasswordConfirmLabel);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.WelcomeLabel);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.PasswordInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Access";
            this.Text = "Access";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Access_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasswordInput;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label PasswordConfirmLabel;
        private System.Windows.Forms.TextBox PasswordConfirmInput;
    }
}