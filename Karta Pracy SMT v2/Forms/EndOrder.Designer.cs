namespace Karta_Pracy_SMT_v2.Forms
{
    partial class EndOrder
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
            this.tbManufacuredQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lLedScrap = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.bNgDown = new System.Windows.Forms.Button();
            this.bNgUp = new System.Windows.Forms.Button();
            this.tbNg = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lPcbQty = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.olvLeds = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.olvPcb = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnNewQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPlus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMinus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvLeds)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvPcb)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
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
            this.tableLayoutPanel2.Controls.Add(this.tbManufacuredQty, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 42);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(342, 83);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // bUp
            // 
            this.bUp.BackgroundImage = global::Karta_Pracy_SMT_v2.Properties.Resources.upArrow;
            this.bUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bUp.Location = new System.Drawing.Point(3, 3);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(79, 77);
            this.bUp.TabIndex = 0;
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.BackgroundImage = global::Karta_Pracy_SMT_v2.Properties.Resources.downArrow;
            this.bDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bDown.Location = new System.Drawing.Point(259, 3);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(80, 77);
            this.bDown.TabIndex = 1;
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // tbManufacuredQty
            // 
            this.tbManufacuredQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbManufacuredQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbManufacuredQty.Location = new System.Drawing.Point(88, 3);
            this.tbManufacuredQty.Name = "tbManufacuredQty";
            this.tbManufacuredQty.ReadOnly = true;
            this.tbManufacuredQty.Size = new System.Drawing.Size(165, 75);
            this.tbManufacuredQty.TabIndex = 2;
            this.tbManufacuredQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbManufacuredQty.TextChanged += new System.EventHandler(this.tbManufacuredQty_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Końcowa ilość MB:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(374, 601);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.lLedScrap);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 453);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(368, 145);
            this.panel6.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(12, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(298, 26);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ilość diody LED do usunięcia:";
            // 
            // lLedScrap
            // 
            this.lLedScrap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lLedScrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lLedScrap.Location = new System.Drawing.Point(0, 22);
            this.lLedScrap.Name = "lLedScrap";
            this.lLedScrap.Size = new System.Drawing.Size(368, 123);
            this.lLedScrap.TabIndex = 6;
            this.lLedScrap.Text = "0";
            this.lLedScrap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.tableLayoutPanel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 303);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(368, 144);
            this.panel5.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(12, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 26);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ilość PCB NG:";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.bNgDown, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.bNgUp, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbNg, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(12, 48);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(342, 83);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // bNgDown
            // 
            this.bNgDown.BackgroundImage = global::Karta_Pracy_SMT_v2.Properties.Resources.upArrow;
            this.bNgDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bNgDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bNgDown.Location = new System.Drawing.Point(3, 3);
            this.bNgDown.Name = "bNgDown";
            this.bNgDown.Size = new System.Drawing.Size(79, 77);
            this.bNgDown.TabIndex = 0;
            this.bNgDown.UseVisualStyleBackColor = true;
            this.bNgDown.Click += new System.EventHandler(this.bNgDown_Click);
            // 
            // bNgUp
            // 
            this.bNgUp.BackgroundImage = global::Karta_Pracy_SMT_v2.Properties.Resources.downArrow;
            this.bNgUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bNgUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bNgUp.Location = new System.Drawing.Point(259, 3);
            this.bNgUp.Name = "bNgUp";
            this.bNgUp.Size = new System.Drawing.Size(80, 77);
            this.bNgUp.TabIndex = 1;
            this.bNgUp.UseVisualStyleBackColor = true;
            this.bNgUp.Click += new System.EventHandler(this.bNgUp_Click);
            // 
            // tbNg
            // 
            this.tbNg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNg.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbNg.Location = new System.Drawing.Point(88, 3);
            this.tbNg.Name = "tbNg";
            this.tbNg.ReadOnly = true;
            this.tbNg.Size = new System.Drawing.Size(165, 75);
            this.tbNg.TabIndex = 2;
            this.tbNg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbNg.TextChanged += new System.EventHandler(this.tbNg_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lPcbQty);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 153);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(368, 144);
            this.panel4.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "Końcowa ilość PCB:";
            // 
            // lPcbQty
            // 
            this.lPcbQty.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lPcbQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lPcbQty.Location = new System.Drawing.Point(0, 21);
            this.lPcbQty.Name = "lPcbQty";
            this.lPcbQty.Size = new System.Drawing.Size(368, 123);
            this.lPcbQty.TabIndex = 5;
            this.lPcbQty.Text = "0";
            this.lPcbQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 144);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.olvLeds);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(377, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.panel2.Size = new System.Drawing.Size(414, 595);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(368, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Diody LED - stan na koniec zlecenia.";
            // 
            // olvLeds
            // 
            this.olvLeds.AllColumns.Add(this.olvColumn1);
            this.olvLeds.AllColumns.Add(this.olvColumn2);
            this.olvLeds.AllColumns.Add(this.olvColumn3);
            this.olvLeds.AllColumns.Add(this.olvColumn4);
            this.olvLeds.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.olvLeds.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.olvLeds.CellEditUseWholeCell = false;
            this.olvLeds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.olvLeds.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvLeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvLeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.olvLeds.Location = new System.Drawing.Point(10, 40);
            this.olvLeds.Name = "olvLeds";
            this.olvLeds.Size = new System.Drawing.Size(394, 545);
            this.olvLeds.TabIndex = 5;
            this.olvLeds.UseCompatibleStateImageBehavior = false;
            this.olvLeds.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Nc12_Formated";
            this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Text = "12NC";
            this.olvColumn1.Width = 130;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Id";
            this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Text = "ID";
            this.olvColumn2.Width = 65;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "QtyNew";
            this.olvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn3.Text = "Ilość";
            this.olvColumn3.Width = 75;
            // 
            // olvColumn4
            // 
            this.olvColumn4.CheckBoxes = true;
            this.olvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Text = "Kosz";
            this.olvColumn4.Width = 45;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panel3.Controls.Add(this.olvPcb);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(797, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 40, 10, 10);
            this.panel3.Size = new System.Drawing.Size(392, 595);
            this.panel3.TabIndex = 2;
            // 
            // olvPcb
            // 
            this.olvPcb.AllColumns.Add(this.olvColumn5);
            this.olvPcb.AllColumns.Add(this.olvColumn6);
            this.olvPcb.AllColumns.Add(this.olvColumnNewQty);
            this.olvPcb.AllColumns.Add(this.olvColumnPlus);
            this.olvPcb.AllColumns.Add(this.olvColumnMinus);
            this.olvPcb.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.olvPcb.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.olvPcb.CellEditEnterChangesRows = true;
            this.olvPcb.CellEditUseWholeCell = false;
            this.olvPcb.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumnNewQty,
            this.olvColumnPlus,
            this.olvColumnMinus});
            this.olvPcb.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvPcb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvPcb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.olvPcb.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvPcb.Location = new System.Drawing.Point(10, 40);
            this.olvPcb.Name = "olvPcb";
            this.olvPcb.ShowGroups = false;
            this.olvPcb.Size = new System.Drawing.Size(372, 545);
            this.olvPcb.TabIndex = 6;
            this.olvPcb.UseCompatibleStateImageBehavior = false;
            this.olvPcb.View = System.Windows.Forms.View.Details;
            this.olvPcb.ButtonClick += new System.EventHandler<BrightIdeasSoftware.CellClickEventArgs>(this.olvPcb_ButtonClick);
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Nc12_Formated";
            this.olvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn5.Text = "12NC";
            this.olvColumn5.Width = 130;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Id";
            this.olvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.Text = "ID";
            this.olvColumn6.Width = 65;
            // 
            // olvColumnNewQty
            // 
            this.olvColumnNewQty.AspectName = "QtyNew";
            this.olvColumnNewQty.CellEditUseWholeCell = true;
            this.olvColumnNewQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnNewQty.Text = "Ilość";
            this.olvColumnNewQty.Width = 75;
            // 
            // olvColumnPlus
            // 
            this.olvColumnPlus.AspectName = "UpNewQty";
            this.olvColumnPlus.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.CellBounds;
            this.olvColumnPlus.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnPlus.IsButton = true;
            this.olvColumnPlus.Tag = "Plus";
            this.olvColumnPlus.Text = "+";
            this.olvColumnPlus.Width = 40;
            // 
            // olvColumnMinus
            // 
            this.olvColumnMinus.AspectName = "DownNewQty";
            this.olvColumnMinus.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.CellBounds;
            this.olvColumnMinus.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnMinus.IsButton = true;
            this.olvColumnMinus.Tag = "Minus";
            this.olvColumnMinus.Text = "-";
            this.olvColumnMinus.Width = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(363, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Płyty PCB - stan na koniec zlecenia.";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.37584F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.2349F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 601F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1192, 601);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // bSave
            // 
            this.bSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.bSave.FlatAppearance.BorderSize = 2;
            this.bSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bSave.Location = new System.Drawing.Point(0, 0);
            this.bSave.Margin = new System.Windows.Forms.Padding(0);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(1198, 55);
            this.bSave.TabIndex = 6;
            this.bSave.Text = "ZAKOŃCZ";
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1198, 662);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.4641F));
            this.tableLayoutPanel6.Controls.Add(this.bSave, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 607);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1198, 55);
            this.tableLayoutPanel6.TabIndex = 6;
            // 
            // EndOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(1198, 662);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Name = "EndOrder";
            this.Text = "Zakończenie zlecenia";
            this.Load += new System.EventHandler(this.EndOrder_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvLeds)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvPcb)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.TextBox tbManufacuredQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private BrightIdeasSoftware.ObjectListView olvLeds;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.Panel panel3;
        private BrightIdeasSoftware.ObjectListView olvPcb;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumnNewQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lPcbQty;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lLedScrap;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button bNgDown;
        private System.Windows.Forms.Button bNgUp;
        private System.Windows.Forms.TextBox tbNg;
        private System.Windows.Forms.Button bSave;
        private BrightIdeasSoftware.OLVColumn olvColumnPlus;
        private BrightIdeasSoftware.OLVColumn olvColumnMinus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    }
}