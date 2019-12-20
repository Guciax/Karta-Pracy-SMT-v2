namespace Karta_Pracy_SMT_v2.Forms
{
    partial class Options
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
            this.label1 = new System.Windows.Forms.Label();
            this.bQrReaderProgramming = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.bCardReaderProgramming = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Programowanie czytnika QR";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bQrReaderProgramming
            // 
            this.bQrReaderProgramming.Location = new System.Drawing.Point(159, 13);
            this.bQrReaderProgramming.Name = "bQrReaderProgramming";
            this.bQrReaderProgramming.Size = new System.Drawing.Size(75, 23);
            this.bQrReaderProgramming.TabIndex = 1;
            this.bQrReaderProgramming.Text = "Programuj";
            this.bQrReaderProgramming.UseVisualStyleBackColor = true;
            this.bQrReaderProgramming.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Aktualizacja ilości co";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(159, 77);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "min";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bCardReaderProgramming
            // 
            this.bCardReaderProgramming.Location = new System.Drawing.Point(159, 48);
            this.bCardReaderProgramming.Name = "bCardReaderProgramming";
            this.bCardReaderProgramming.Size = new System.Drawing.Size(75, 23);
            this.bCardReaderProgramming.TabIndex = 6;
            this.bCardReaderProgramming.Text = "Programuj";
            this.bCardReaderProgramming.UseVisualStyleBackColor = true;
            this.bCardReaderProgramming.Click += new System.EventHandler(this.bCardReaderProgramming_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Programowanie czytnika kart";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 169);
            this.Controls.Add(this.bCardReaderProgramming);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bQrReaderProgramming);
            this.Controls.Add(this.label1);
            this.Name = "Options";
            this.Text = "Ustawienia";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bQrReaderProgramming;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bCardReaderProgramming;
        private System.Windows.Forms.Label label4;
    }
}