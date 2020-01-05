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
using static System.Windows.Forms.ListViewItem;

namespace Karta_Pracy_SMT_v2.Forms
{
    public partial class NewOrder : Form
    {
        public MstOrder dialogResult = new MstOrder();
        public bool startChangeover = false;
        MST.MES.OrderStructureByOrderNo.Kitting kittingData;
        MST.MES.OrderStructureByOrderNo.SMT smtData;
        List<ushort> keysList = new List<ushort>();

        public MST.MES.UsersDataBase.DataStructures.UserStructure userOperator = new MST.MES.UsersDataBase.DataStructures.UserStructure();

        public NewOrder()
        {
            InitializeComponent();
        }

        private async void NewOrder_Load(object sender, EventArgs e)
        {
            cbOperator.Items.AddRange(GetOperatorsArray(30));
            pbLoading.Parent = this;
            pbLoading.BringToFront();
            pbLoading.BackColor = Color.White;
            pbLoading.Location = new Point(550, 3);
            ShowOrdersQueue();

            await UserDb.GetUsersFromDbAsync();
            if (GlobalParameters.Debug)
            {
                tbOperatorCardId.Text = "3400927164";
            }
        }

        private void ShowOrdersQueue()
        {
            var orders = MesData.KittingData.Where(o => o.Value.endDate < o.Value.kittingDate).OrderBy(o=>o.Value.plannedEnd);
            foreach (var order in orders)
            {
                var smtQty = 0;
                if (MesData.SmtData.ContainsKey(order.Value.orderNo))
                {
                    smtQty = MesData.SmtData[order.Value.orderNo].totalManufacturedQty;
                }

                ListViewItem newItem = new ListViewItem();
                newItem.Text = order.Value.orderNo;
                newItem.SubItems.Add(order.Value.modelId_12NCFormat);
                newItem.SubItems.Add(order.Value.orderedQty.ToString());
                newItem.SubItems.Add(smtQty.ToString());
                lvOrdersQueue.Items.Add(newItem);
            }
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
                tbOrderNo.ReadOnly = true;
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
                //cbOperator.Enabled = true;

                MarkOrderOnList(tbOrderNo.Text);

                tbOperatorCardId.Visible = true;
                tbOperatorCardId.Enabled = true;
                lOperatorCard.Visible = true;
                this.ActiveControl = tbOperatorCardId;
            }
        }

        private void MarkOrderOnList(string orderNo)
        {
            foreach (ListViewItem item in lvOrdersQueue.Items)
            {
                if(item.Text == orderNo)
                {
                    foreach (ListViewSubItem subItem in item.SubItems)
                    {
                        subItem.BackColor = Color.DarkSlateBlue;
                        subItem.ForeColor = Color.White;
                    }
                    lvOrdersQueue.EnsureVisible(item.Index);
                }
            }
        }

        private void tbOrderNo_Leave(object sender, EventArgs e)
        {
            //if (tbOrderNo.Enabled)
            //{
            //    this.ActiveControl = tbOrderNo;
            //}
        }

        private void tbStencil_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                if(!GlobalParameters.Debug & KeyboardDeviceListener.lastInputDeviceName != GlobalParameters.QrReaderName)
                {
                    MessageBox.Show("Użyj czytnika!");
                    tbStencil.Text = "";
                    return;
                }
                //if (CheckUserData())
                {
                    dialogResult.SmtData = new MST.MES.OrderStructureByOrderNo.SmtRecords
                    {
                        smtStartDate = DateTime.Now,
                        operatorSmt = userOperator.Name,
                        smtEndDate = DateTime.MinValue,
                        smtLine = GlobalParameters.SmtLine,
                        stencilId = tbStencil.Text
                    };
                    dialogResult.SmtData.stencilId = tbStencil.Text;
                    //dialogResult.SmtData.operatorSmt = cbOperator.Text;
                    startChangeover = checkBox1.Checked;
                    CurrentMstOrder.userOperator = userOperator;

                    var stencilInfo = MST.MES.StencilManagement.GetOneStencilData(tbStencil.Text);
                    if(stencilInfo != null)
                    {
                        if (stencilInfo.MaintanaceNeeded)
                        {
                            MessageBox.Show($"Ten szablon wykonał już {stencilInfo.CyclesCount} cykli i wymaga przeglądu." + Environment.NewLine
                                            + "Po zakończeniu zlecenia przekaż go technikowi.");
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void cbOperator_Enter(object sender, EventArgs e)
        {
            cbOperator.ForeColor = Color.Black;
        }

        private void tbOperatorCardId_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void tbOperatorCardId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 & !tbOperatorCardId.ReadOnly)
            {
                //var cardHexValue = MST.MES.UsersDataBase.CardIdOperations.CardIntIdToReversedHex(tbOperatorCardId.Text);
                //var matchingUsers = UserDb.Users.Where(x => x.CardId == cardHexValue);
                //if (!matchingUsers.Any())
                //{
                //    MessageBox.Show("Brak uprawnień.");
                //    tbOperatorCardId.Clear();
                //    return;
                //}
                //userOperator = matchingUsers.First();
                MST.MES.UsersDataBase.DataStructures.UserStructure user;
                if (GlobalParameters.Debug)
                {
                    user = UserDb.Users.First();
                }
                else
                {
                    user = MST.MES.UsersDataBase.OperationsOnUsers.CheckAccessLevelReturnUser(MST.MES.UsersDataBase.DataStructures.Functions.Smt,
                                                                                              tbOperatorCardId.Text,
                                                                                              UserDb.Users);
                }
                
                if (user != null)
                {
                    userOperator = user;
                    tbOperatorCardId.Text = userOperator.Name;
                    tbOperatorCardId.ReadOnly = true;
                    tbOperatorCardId.BackColor = Color.Lime;
                    tbOperatorCardId.ReadOnly = true;

                    tbStencil.Visible = true;
                    lStencilId.Visible = true;

                    this.ActiveControl = tbStencil;
                }
                else
                {
                    tbOperatorCardId.Text = "";
                }
            }
        }
    }
}
