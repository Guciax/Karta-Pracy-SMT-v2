﻿namespace Karta_Pracy_SMT_v2.Forms
{
    partial class EnterPcbOnMbQtyManually
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bUp = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.tbPcbPerMb = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.bUp, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bDown, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbPcbPerMb, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 74);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(354, 82);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // bUp
            // 
            this.bUp.BackgroundImage = global::Karta_Pracy_SMT_v2.Properties.Resources.upArrow;
            this.bUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bUp.Location = new System.Drawing.Point(3, 3);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(82, 76);
            this.bUp.TabIndex = 0;
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.BackgroundImage = global::Karta_Pracy_SMT_v2.Properties.Resources.downArrow;
            this.bDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bDown.Location = new System.Drawing.Point(268, 3);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(83, 76);
            this.bDown.TabIndex = 1;
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // tbPcbPerMb
            // 
            this.tbPcbPerMb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPcbPerMb.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPcbPerMb.Location = new System.Drawing.Point(91, 3);
            this.tbPcbPerMb.Name = "tbPcbPerMb";
            this.tbPcbPerMb.ReadOnly = true;
            this.tbPcbPerMb.Size = new System.Drawing.Size(171, 75);
            this.tbPcbPerMb.TabIndex = 2;
            this.tbPcbPerMb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bOK, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 200);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 65);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Podaj ilość PCB na MB";
            // 
            // bOK
            // 
            this.bOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOK.Location = new System.Drawing.Point(3, 162);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(354, 35);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // EnterPcbOnMbQtyManually
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 200);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EnterPcbOnMbQtyManually";
            this.Text = "EnterPctOnMbQtyManually";
            this.Load += new System.EventHandler(this.EnterPctOnMbQtyManually_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.TextBox tbPcbPerMb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bOK;
    }
}