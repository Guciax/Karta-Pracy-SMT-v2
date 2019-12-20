namespace Karta_Pracy_SMT_v2.Forms
{
    partial class NewOrder
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
            this.tbOrderNo = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbOperatorCardId = new System.Windows.Forms.TextBox();
            this.lOperatorCard = new System.Windows.Forms.Label();
            this.lStencilId = new System.Windows.Forms.Label();
            this.tbStencil = new System.Windows.Forms.TextBox();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lModelInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.lOrderInfo = new System.Windows.Forms.Label();
            this.lvOrdersQueue = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // tbOrderNo
            // 
            this.tbOrderNo.AcceptsReturn = true;
            this.tbOrderNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbOrderNo.Location = new System.Drawing.Point(1, 1);
            this.tbOrderNo.Margin = new System.Windows.Forms.Padding(1);
            this.tbOrderNo.Multiline = true;
            this.tbOrderNo.Name = "tbOrderNo";
            this.tbOrderNo.Size = new System.Drawing.Size(345, 38);
            this.tbOrderNo.TabIndex = 0;
            this.tbOrderNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOrderNo_KeyPress);
            this.tbOrderNo.Leave += new System.EventHandler(this.tbOrderNo_Leave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbOrderNo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(440, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(347, 381);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tbOperatorCardId);
            this.panel3.Controls.Add(this.lOperatorCard);
            this.panel3.Controls.Add(this.lStencilId);
            this.panel3.Controls.Add(this.tbStencil);
            this.panel3.Controls.Add(this.cbOperator);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 241);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 139);
            this.panel3.TabIndex = 3;
            // 
            // tbOperatorCardId
            // 
            this.tbOperatorCardId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbOperatorCardId.Location = new System.Drawing.Point(10, 30);
            this.tbOperatorCardId.Name = "tbOperatorCardId";
            this.tbOperatorCardId.Size = new System.Drawing.Size(323, 26);
            this.tbOperatorCardId.TabIndex = 4;
            this.tbOperatorCardId.Visible = false;
            this.tbOperatorCardId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOperatorCardId_KeyPress);
            // 
            // lOperatorCard
            // 
            this.lOperatorCard.AutoSize = true;
            this.lOperatorCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lOperatorCard.Location = new System.Drawing.Point(10, 7);
            this.lOperatorCard.Name = "lOperatorCard";
            this.lOperatorCard.Size = new System.Drawing.Size(188, 20);
            this.lOperatorCard.TabIndex = 3;
            this.lOperatorCard.Text = "Skanuj kartę użytkownika";
            this.lOperatorCard.Visible = false;
            // 
            // lStencilId
            // 
            this.lStencilId.AutoSize = true;
            this.lStencilId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lStencilId.Location = new System.Drawing.Point(10, 79);
            this.lStencilId.Name = "lStencilId";
            this.lStencilId.Size = new System.Drawing.Size(222, 20);
            this.lStencilId.TabIndex = 2;
            this.lStencilId.Text = "Stencil QR (skanuj czytnikiem)";
            this.lStencilId.Visible = false;
            // 
            // tbStencil
            // 
            this.tbStencil.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbStencil.Location = new System.Drawing.Point(10, 100);
            this.tbStencil.Name = "tbStencil";
            this.tbStencil.Size = new System.Drawing.Size(323, 26);
            this.tbStencil.TabIndex = 1;
            this.tbStencil.Visible = false;
            this.tbStencil.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbStencil_KeyDown);
            // 
            // cbOperator
            // 
            this.cbOperator.Enabled = false;
            this.cbOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbOperator.ForeColor = System.Drawing.Color.DarkGray;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(246, 62);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(87, 32);
            this.cbOperator.TabIndex = 0;
            this.cbOperator.Text = "Operator";
            this.cbOperator.Visible = false;
            this.cbOperator.SelectedIndexChanged += new System.EventHandler(this.comboBoxOperator_SelectedIndexChanged);
            this.cbOperator.Enter += new System.EventHandler(this.cbOperator_Enter);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lModelInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 141);
            this.panel2.Margin = new System.Windows.Forms.Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 98);
            this.panel2.TabIndex = 2;
            // 
            // lModelInfo
            // 
            this.lModelInfo.AutoSize = true;
            this.lModelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lModelInfo.Location = new System.Drawing.Point(6, 3);
            this.lModelInfo.Name = "lModelInfo";
            this.lModelInfo.Size = new System.Drawing.Size(21, 20);
            this.lModelInfo.TabIndex = 1;
            this.lModelInfo.Text = "...";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbLoading);
            this.panel1.Controls.Add(this.lOrderInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 41);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 98);
            this.panel1.TabIndex = 1;
            // 
            // pbLoading
            // 
            this.pbLoading.Image = global::Karta_Pracy_SMT_v2.Properties.Resources.loadingSpinner;
            this.pbLoading.Location = new System.Drawing.Point(169, 21);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(35, 35);
            this.pbLoading.TabIndex = 1;
            this.pbLoading.TabStop = false;
            this.pbLoading.Visible = false;
            // 
            // lOrderInfo
            // 
            this.lOrderInfo.AutoSize = true;
            this.lOrderInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lOrderInfo.Location = new System.Drawing.Point(6, 6);
            this.lOrderInfo.Name = "lOrderInfo";
            this.lOrderInfo.Size = new System.Drawing.Size(21, 20);
            this.lOrderInfo.TabIndex = 0;
            this.lOrderInfo.Text = "...";
            // 
            // lvOrdersQueue
            // 
            this.lvOrdersQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.lvOrdersQueue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvOrdersQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lvOrdersQueue.Location = new System.Drawing.Point(0, 0);
            this.lvOrdersQueue.Name = "lvOrdersQueue";
            this.lvOrdersQueue.Size = new System.Drawing.Size(440, 381);
            this.lvOrdersQueue.TabIndex = 2;
            this.lvOrdersQueue.UseCompatibleStateImageBehavior = false;
            this.lvOrdersQueue.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Kolejka";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "10NC";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ilość";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Wykonano";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 105;
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(0, 381);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(5);
            this.checkBox1.Size = new System.Drawing.Size(787, 52);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Rozpocznij przestawienie";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // NewOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(787, 433);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lvOrdersQueue);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NewOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nowe zlecenie";
            this.Load += new System.EventHandler(this.NewOrder_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbOrderNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.TextBox tbStencil;
        private System.Windows.Forms.Label lOrderInfo;
        private System.Windows.Forms.Label lModelInfo;
        private System.Windows.Forms.Label lStencilId;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.ListView lvOrdersQueue;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox tbOperatorCardId;
        private System.Windows.Forms.Label lOperatorCard;
    }
}