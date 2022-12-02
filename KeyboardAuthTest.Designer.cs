namespace WinFormsApp1
{
    partial class KeyboardAuthTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardAuthTest));
            this.StageLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SampleText = new System.Windows.Forms.RichTextBox();
            this.UserInput = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.FinishTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StageLabel
            // 
            this.StageLabel.AutoSize = true;
            this.StageLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StageLabel.Location = new System.Drawing.Point(362, 20);
            this.StageLabel.Name = "StageLabel";
            this.StageLabel.Size = new System.Drawing.Size(89, 25);
            this.StageLabel.TabIndex = 0;
            this.StageLabel.Text = "Этап 1/N";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(113, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(601, 94);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SampleText
            // 
            this.SampleText.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SampleText.Location = new System.Drawing.Point(70, 178);
            this.SampleText.Name = "SampleText";
            this.SampleText.ReadOnly = true;
            this.SampleText.Size = new System.Drawing.Size(669, 115);
            this.SampleText.TabIndex = 100;
            this.SampleText.Text = "";
            // 
            // UserInput
            // 
            this.UserInput.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UserInput.Location = new System.Drawing.Point(70, 313);
            this.UserInput.Name = "UserInput";
            this.UserInput.Size = new System.Drawing.Size(669, 133);
            this.UserInput.TabIndex = 2;
            this.UserInput.Text = "";
            this.UserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserInput_KeyDown);
            this.UserInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserInput_KeyPress);
            this.UserInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UserInput_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(240, 458);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Нажмите на кнопку, чтобы запустить тестирование";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(38, 493);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 61);
            this.button1.TabIndex = 1;
            this.button1.Text = "Начать тестирование";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(303, 493);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(231, 61);
            this.button2.TabIndex = 3;
            this.button2.Text = "Следующий этап";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FinishTest
            // 
            this.FinishTest.Enabled = false;
            this.FinishTest.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FinishTest.Location = new System.Drawing.Point(562, 493);
            this.FinishTest.Name = "FinishTest";
            this.FinishTest.Size = new System.Drawing.Size(231, 61);
            this.FinishTest.TabIndex = 101;
            this.FinishTest.Text = "Готово";
            this.FinishTest.UseVisualStyleBackColor = true;
            this.FinishTest.Click += new System.EventHandler(this.FinishTest_Click);
            // 
            // KeyboardAuthTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 581);
            this.Controls.Add(this.FinishTest);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UserInput);
            this.Controls.Add(this.SampleText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "KeyboardAuthTest";
            this.Text = "KeyboardAuthTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StageLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox SampleText;
        private System.Windows.Forms.RichTextBox UserInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button FinishTest;
    }
}