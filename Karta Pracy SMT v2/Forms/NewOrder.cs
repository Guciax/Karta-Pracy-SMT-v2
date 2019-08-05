using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.DataStructures;
using RawInput_dll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2.Forms
{
    public partial class NewOrder : Form
    {
        public MstOrder dialogResult = new MstOrder();
        MST.MES.OrderStructureByOrderNo.Kitting kittingData;
        MST.MES.OrderStructureByOrderNo.SMT smtData;
        List<ushort> keysList = new List<ushort>();

        public NewOrder()
        {
            InitializeComponent();
        }

        private void NewOrder_Load(object sender, EventArgs e)
        {
            cbOperator.Items.AddRange(GetOperatorsArray(30));
            pbLoading.Parent = this;
            pbLoading.BringToFront();
            pbLoading.BackColor = Color.White;
            pbLoading.Location = new Point(430, 3);
        }

        public static string[] GetOperatorsArray(int daysAgo)
        {
            DateTime untilDate = System.DateTime.Now.AddDays(daysAgo * (-1));
            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = String.Format(@"SELECT OperatorSMT,DataCzasKoniec FROM tb_SMT_Karta_Pracy Where DataCzasKoniec>@Data");
            command.Parameters.AddWithValue("@Data", untilDate.Date);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            HashSet<string> operators = new HashSet<string>();
            foreach (DataRow row in sqlTable.Rows)
            {
                operators.Add(row["OperatorSMT"].ToString().Trim());
            }

            return operators.OrderBy(o => o).ToArray();
        }

        private void tbOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void DisplayOrderInfo()
        {
            if (kittingData != null)
            {
                lOrderInfo.Text = $"10NC: {kittingData.modelId_12NCFormat}" + Environment.NewLine
                            + $"Nazwa: {kittingData.ModelName}" + Environment.NewLine
                            + $"Ilość zlecona: {kittingData.orderedQty}" + Environment.NewLine;
                try
                {
                    lOrderInfo.Text += $"Ilość wykonana: {smtData.totalManufacturedQty}";
                }
                catch { }


                var dtModel = MST.MES.DtTools.GetDtModel00(kittingData.modelId, DevTools.DtDb);
                if (dtModel != null)
                {
                    lModelInfo.Text = $"Ilość LED: {MST.MES.DtTools.GetLedCount(dtModel)} szt." + Environment.NewLine
                                     + $"Ilość CONN: {MST.MES.DtTools.GetConnCount(dtModel)} szt." + Environment.NewLine
                                     + $"Ilość RES: {MST.MES.DtTools.GetResCount(dtModel)} szt." + Environment.NewLine
                                     + $"Ilość PCB/NB: {MST.MES.DtTools.GetPcbPerMbCount(dtModel)} szt." + Environment.NewLine;
                }
            }
        }

        private void comboBoxOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbOperator.Text == "")
            {
                cbOperator.Text = "Operator";
                cbOperator.ForeColor = Color.DarkGray;
            }
            else
            {
                cbOperator.ForeColor = Color.Black;
            }
            tbStencil.Enabled = true;
            this.ActiveControl = tbStencil;
        }

        private bool CheckUserData()
        {
            if(kittingData == null)
            {
                MessageBox.Show("Brak numeru zlecenia.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(kittingData.orderNo))
            {
                MessageBox.Show("Brak numeru zlecenia.");
                return false;
            }
            if (cbOperator.Text == "Operator")
            {
                MessageBox.Show("Uzupełnij pole Operator");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbStencil.Text))
            {
                MessageBox.Show("Uzupełnij pole Stencil");
                return false;
            }
            return true;
        }
        private bool LoadData()
        {
            bool result = true;
            try
            {
                var kitting = MST.MES.SqlDataReaderMethods.Kitting.GetKittingDataForOrders(new string[] { tbOrderNo.Text });
                if (kitting.Count == 0)
                {
                    MessageBox.Show("Brak podanego numeru zlecenia w bazie MES");
                    return false;
                }
                kittingData = kitting[tbOrderNo.Text];
                dialogResult.KittingData = kittingData;
                smtData = MST.MES.SqlDataReaderMethods.SMT.GetOneOrder(tbOrderNo.Text);
            }
            catch
            {
                MessageBox.Show("Brak połączenia z siecią, spróbuj ponownie później.");
                return false;
            }

            var dtModel00 = MST.MES.DtTools.GetDtModel00(kittingData.modelId, DevTools.DtDb);
            if (dtModel00 == null)
            {
                MessageBox.Show($"Brak Modelu {kittingData.modelId_12NCFormat} w bazie DevTools.");
                return false;
            }
            var dtModel46 = MST.MES.DtTools.GetDtModel46(kittingData.modelId, DevTools.DtDb);
            dialogResult.modelInfo = new MstOrder.ModelInfo
            {
                DtModel00 = dtModel00,
                DtModel46 = dtModel46
            };

            return result;
        }

        private async Task LoadDataAsynch()
        {
            bool success = true;
            await Task.Run(() => success = LoadData());
            if (success)
            {
                tbOrderNo.Enabled = false;
                tbOrderNo.ForeColor = Color.Black;
                tbOrderNo.BackColor = Color.Lime;
            }
            
        }

        private async void tbOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                pbLoading.Visible = true;
                await LoadDataAsynch();
                pbLoading.Visible = false;
                DisplayOrderInfo();
                cbOperator.Enabled = true;
            }
        }

        private void tbOrderNo_Leave(object sender, EventArgs e)
        {
            if (tbOrderNo.Enabled)
            {
                this.ActiveControl = tbOrderNo;
            }
        }

        private void tbStencil_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                if(QrReader.lastInputDeviceName != GlobalParameters.QrReaderName)
                {
                    MessageBox.Show("Użyj czytnika!");
                    tbStencil.Text = "";
                    return;
                }
                if (CheckUserData())
                {
                    dialogResult.StencilId = tbStencil.Text;
                    dialogResult.OperatorName = cbOperator.Text;
                    dialogResult.StartTime = DateTime.Now;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
