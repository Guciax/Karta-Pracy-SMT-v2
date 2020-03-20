using BrightIdeasSoftware;
using Karta_Pracy_SMT_v2.CurrentOrder;
using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.Efficiency;
using Karta_Pracy_SMT_v2.Forms;
using RawInput_dll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Karta_Pracy_SMT_v2.OtherComponents;

namespace Karta_Pracy_SMT_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //3400927164
            InitializeComponent();
            OrdersHistory.dgvOrders = dgvOrders;
            CurrentMstOrder.lvOrderInfo = lfOrderInfo;
            CurrentMstOrder.lvProdNorms = lvProdNorms;
            LedsUsed.olvLedsUsed = olvLedsUsed;

            PcbUsedInOrder.olvPcbUsed = olvPcbUsed;

            KeyboardDeviceListener.rawinput = new RawInput(Handle);
            KeyboardDeviceListener.rawinput.KeyPressed += KeyboardDeviceListener.rawinput_KeyPressed;

            OtherComponents.olvOtherComponents = olvOtherComponents;

            //olvLedsUsed.AlwaysGroupByColumn = olvLedsUsed.GetColumn(0);

            Efficiency.ShowEfficiency.lCurrentOrderEff = lEfficiencyThisOrder;
            Efficiency.ShowEfficiency.lOperatorEff = lchangeOverTimeAvg;
            Efficiency.ShowEfficiency.lShiftEff = lEfficiencyThisShift;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ChangeOver.ChangeOverPanel = pChangeOver;
            ChangeOver.ChangeOverTimer = changeoverTimer;
            ChangeOver.dgvAcceptance = dgvAcceptance;
            pChangeOver.Parent = panel10;
            pChangeOver.Dock = DockStyle.Fill;
            pChangeOver.BringToFront();

            GlobalParameters.SmtLine = SmtLineFile.ReadLine();
            lSmtLine.Text = GlobalParameters.SmtLine;
            await MesData.AsyncLoader();
            bNewOrder.Enabled = true;
            OrdersHistory.MesOrdersToOrdersHistory(40);
            OtherComponents.UpdateList();

#if DEBUG
            bDebug.Visible = true;
            GlobalParameters.Debug = true;
            bDbg.Visible = true;
            GlobalParameters.SmtLine = "SMT2";
#endif

            pbBackgroundImage.Parent = this;
            pbBackgroundImage.Dock = DockStyle.Fill;
            pbBackgroundImage.BringToFront();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lVersion.Text = $"ver. {fvi.FileVersion}";

            SolderPasteCheck.mainForm = this;
            //CurrentShiftEfficiency.SpitOutEff();

            //olvLedsUsed.CustomSorter = delegate (OLVColumn column, SortOrder order) {
            //    this.olvLedsUsed.ListViewItemSorter = new ColumnComparer(
            //            this.olvColSortPriority, SortOrder.Ascending, column, order);
            //};
        }

        private void tDataUpdates_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - DevTools.lastUpdateTime).TotalHours > 1) 
            {
                try
                {
                    DevTools.ReloadDb();
                }
                catch
                {
                    DevTools.lastUpdateSuccesfull = false;
                }
            }
        }

        private void BNewOrder_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            
            using (NewOrder newOrderForm = new NewOrder())
            {
                if (newOrderForm.ShowDialog() == DialogResult.OK)
                {
                    OrdersHistory.ordersHistory.Add(newOrderForm.dialogResult);
                    CurrentMstOrder.currentOrder = OrdersHistory.ordersHistory.Where(o => o.OrderNo == newOrderForm.dialogResult.OrderNo).OrderByDescending(o => o.SmtData.smtStartDate).First();
                    LedsUsed.ClearList();
                    ComponentsOnRw.Refresh();
                    PcbUsedInOrder.ClearList();
                    tbNg.Text = "0";
                    bFinishOrder.Enabled = true;
                    if (newOrderForm.startChangeover)
                    {
                        ChangeOver.StartChangeOver();
                    }
                    ComponentsKittedForCurrentOrder.Reload();
                }
                if (GlobalParameters.Debug)
                {
                    //LedsUsed.DebugAddLed();
                    //PcbUsedInOrder.DebugAddPcb();
                }
            }
            OtherComponents.UpdateList();
        }

        private void bAddLedQr_Click(object sender, EventArgs e)
        {
            //LedDiodesForCurrentOrder.ReloadList();
            UpdateScreenSHot();
            if (CurrentMstOrder.currentOrder != null)
            {
                using (ScanLedQr scanForm = new ScanLedQr(true))
                {
                    if (scanForm.ShowDialog() == DialogResult.OK)
                    {
                        LedsUsed.AddNewLed(scanForm.graffitiCompData);
                        //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(scanForm.nc12, scanForm.id, GlobalParameters.SmtLine);
                        //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation(scanForm.graffitiCompData.QrCode,Graffiti.MST.ComponentsLocations.LineNumberToLocation( GlobalParameters.SmtLine));
                    }
                }
            }
            else { MessageBox.Show("Wczytaj najpierw zlecenie."); }
        }

        private void bMoveToTrash_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            if (LedsUsed.ledsInUseList == null) return;
            if (LedsUsed.ledsInUseList.Count() == 0) return;
            using (ScanLedQr scanForm = new ScanLedQr(false))
            {
                if(scanForm.ShowDialog() == DialogResult.OK)
                {
                    LedsUsed.MoveLedToTrash(scanForm.qrCode);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void bDebug_Click(object sender, EventArgs e)
        {

        }

        private void tClock_Tick(object sender, EventArgs e)
        {
            CurrentMstOrder.UpdateOrderQty(pbBackgroundImage);
            EfficiencyChart.AddPoint(pbEfficiencyChart);
            var effCurrentOrder = Math.Round(CurrentShiftEfficiency.CalculateCurrentOrderEfficiency() * 100, 1);
            var effCurOrdString = effCurrentOrder > 0 ? effCurrentOrder.ToString() : "-";

            var effCurrentShift = Math.Round(CurrentShiftEfficiency.CalculateCurrentShiftEfficiency() * 100, 1);
            var effCurrentShiftString = effCurrentShift > 0 ? effCurrentShift.ToString() : "-";

            var avgCO = Math.Round(CurrentShiftEfficiency.CalculateCurrentShiftAvgChangeOverTime(), 0);
            var avgCOString = avgCO > 0 ? avgCO.ToString() : "-";

            Efficiency.ShowEfficiency.Show();

            SolderPasteCheck.CheckIfNeedToShowAlert();

            //lEfficiencyThisOrder.Text = $"{effCurOrdString}%";
            //lEfficiencyThisShift.Text = $"{effCurrentShiftString}%";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Options optForm = new Options();
            optForm.ShowDialog();
        }

        private async void bFinishOrder_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            if (CurrentMstOrder.currentOrder == null) return;
            using(EndOrder endForm = new EndOrder())
            {
                if(endForm.ShowDialog() == DialogResult.OK)
                {
                    CurrentMstOrder.currentOrder.ManufacturedQty = endForm.finalQty;
                    ConnectedToCurrentOrder.UpdateConnectedOrderQty();
                    CurrentMstOrder.UpdateOrSaveCurretOrderAndConnected();
                    CurrentMstOrder.currentOrder = null;
                    bFinishOrder.Enabled = false;
                    tbNg.Text = "";

                    LedsUsed.ClearList();
                    PcbUsedInOrder.ClearList();

                    await MesData.AsyncLoader();
                    bNewOrder.Enabled = true;
                    OrdersHistory.MesOrdersToOrdersHistory(40);
                    OtherComponents.UpdateList();

                    ChangeOver.StartChangeOver();
                    bNewOrder.PerformClick();
                }
            }
        }

        private void bAddPcb_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            if (CurrentMstOrder.currentOrder == null)
            {
                MessageBox.Show("Wczytaj najpierw zlecenie.");
                return;
            }

            using (ScanLedQr scanForm = new ScanLedQr(true))
            {
                if (scanForm.ShowDialog() == DialogResult.OK)
                {
                    PcbUsedInOrder.AddNewPcb(scanForm.graffitiCompData);
                    {
                        //MST.MES.SqlOperations.SparingLedInfo.UpdateLedZlecenieStringBinIdLocation(scanForm.nc12, scanForm.id, CurrentMstOrder.currentOrder.OrderNo, "A", GlobalParameters.SmtLine);
                        //Graffiti.MST.ComponentsTools.UpdateDbData.BindComponentToOrderNumber($"{scanForm.nc12}|ID:{scanForm.id}", CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00);
                        //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{scanForm.nc12}|ID:{scanForm.id}", Graffiti.MST.ComponentsLocations.LineNumberToLocation( GlobalParameters.SmtLine));
                    }
                    //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(scanForm.nc12, scanForm.id, GlobalParameters.SmtLine);
                }
            }
        }

        private void bMovePcbToTrash_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            if (PcbUsedInOrder.pcbUsedList.Count == 0) return;
            using (ScanLedQr scanForm = new ScanLedQr(false))
            {
                if (scanForm.ShowDialog() == DialogResult.OK)
                {
                    PcbUsedInOrder.MovePcbToTrash(scanForm.qrCode);
                }
            }
        }

        private void olvLedsUsed_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            
        }

        private void olvLedsUsed_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            var ledRecord = (LedsUsed.LedsUsedStruct)e.Model;

            e.Item.BackColor = ledRecord.BackGround;
            e.Item.ForeColor = ledRecord.ForeGround;
            
        }

        private void olvPcbUsed_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            var pcbRecord = (PcbUsedInOrder.PcbUsedStruct)e.Model;
            if (pcbRecord.QtyNew == 0)
            {
                e.Item.BackColor = Color.Black;
                e.Item.ForeColor = Color.White;
            }
        }

        private void bNgUp_Click(object sender, EventArgs e)
        {
            int curValue = 0;
            if (!int.TryParse(tbNg.Text, out curValue)) return;
            tbNg.Text = (curValue + 1).ToString();
        }

        private void bNgDown_Click(object sender, EventArgs e)
        {
            var curValue = 0;
            if (!int.TryParse(tbNg.Text, out curValue)) return;
            if (curValue > 0)
            {
                tbNg.Text = (curValue - 1).ToString();
            }
        }

        private void tbNg_TextChanged(object sender, EventArgs e)
        {
            if(CurrentMstOrder.currentOrder != null)
            {
                int ngQty = int.Parse(tbNg.Text);
                lLedWasteFromNg.Text = $"Odpad LED: {(ngQty * CurrentMstOrder.currentOrder.modelInfo.LedCount).ToString()}";
                CurrentMstOrder.currentOrder.NgQty = ngQty;
            }
        }

        private void bOtherComponentsAdd_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            using (ScanLedQr scanForm = new ScanLedQr(false))
            {
                if(scanForm.ShowDialog() == DialogResult.OK)
                {
                    OtherComponents.AddNewComponent(scanForm.nc12, scanForm.id);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateScreenSHot()
        {
            //Bitmap ss = new Bitmap(this.Width, this.Height);
            //this.DrawToBitmap(ss, new Rectangle(0, 0, this.Width, this.Height));
            //BlurredBackground.currentScreenShot = ss;
        }

        private void lClock_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void bDbg_Click(object sender, EventArgs e)
        {
            //SolderPasteCheck.ShowPanel();
            //var c = Graffiti.MST.ComponentsTools.GetDbData.GetComponentDataWithAttributes(new string[] { "401046000172|ID:57819" }).ToList();
            //ComponentsOnRw.TrashComponent("401046001391|ID:100053");
            //string[,] arr = new string[1, 2];
            //arr[0, 0] = "333";
            //arr[0, 1] = "KOSZ";
            //throw new NotImplementedException();
            //Graffiti.MST.ComponentsTools.UpdateDbData.SetRolkaTablica("401056018021|ID:100022", arr);

            //var cx = Graffiti.MST.ComponentsTools.GetDbData.GetComponentDataWithAttributes(new string[] { "401046000172|ID:57819" }).ToList();
            ;
            ////ChangeOver.StartChangeOver();
            //CurrentMstOrder.UpdateOrderQty(pbBackgroundImage);
            //CurrentMstOrder.UpdateListViewOrderInfo();
        }

        private void bOtherComponentsTrash_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            using (ScanLedQr scanForm = new ScanLedQr(false))
            {
                if (scanForm.ShowDialog() == DialogResult.OK)
                {
                    OtherComponents.MoveComponentToTrash(scanForm.nc12, scanForm.id);
                }
            }
        }

        private void bMoveToStorage_Click(object sender, EventArgs e)
        {
            UpdateScreenSHot();
            using (ScanLedQr scanForm = new ScanLedQr(false))
            {
                if (scanForm.ShowDialog() == DialogResult.OK)
                {
                    OtherComponents.MoveComponentToStorage(scanForm.nc12, scanForm.id);
                }
            }
        }

        private void olvOtherComponents_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            
        }

        private void olvOtherComponents_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            OtherComponentsStruct model = e.Model as OtherComponentsStruct;
            if (model == null) return;

            if (model.MatchesWithCurrentOrder) e.Item.BackColor = Color.Lime;
            if (model.Name == "BRAK!")
            {
                e.Item.BackColor = Color.Red;
                e.Item.ForeColor = Color.White;
            }
        }

        private void changeoverTimer_Tick(object sender, EventArgs e)
        {
            var changeOverDuration = DateTime.Now - ChangeOver.ChangeOverStart;
            var seconds = 
            lChangeOverTime.Text = $"{changeOverDuration.TotalMinutes.ToString("00")}:{changeOverDuration.Seconds.ToString("00")}";
            if (changeOverDuration.TotalMinutes > 25)
            {
                pChangeOver.BackColor = Color.FromArgb(255, 253, 216, 53);
            }
            if(changeOverDuration.TotalMinutes > 35)
            {
                pChangeOver.BackColor = Color.FromArgb(255, 255, 82, 82);
            }
        }

        private void bStartChangeOver_Click(object sender, EventArgs e)
        {
            if (CurrentMstOrder.currentOrder == null)
            {
                MessageBox.Show("Najpierw wczytaj zlecenie");
                return;
            }
            if(ChangeOver.ChangeOverStart == DateTime.MinValue)
            {
                return;
            }
            ChangeOver.StartChangeOver();
        }

        private void bChangeOverFinish_Click(object sender, EventArgs e)
        {
            if (bChangeOverFinish.Text == "Zakończ przestawienie")
            {
                ChangeOver.FinishChangeOver();
                bChangeOverFinish.Text = "Brak akceptacji!";
            }
        }


        private void dgvAcceptance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvAcceptance.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {
                string requiredAccess = "Technician";
                if(e.RowIndex > 0)
                {
                    requiredAccess = "Oqa";

                }
                using (ReadRfidCard cardForm = new ReadRfidCard(requiredAccess))
                {
                    if (cardForm.ShowDialog() == DialogResult.OK)
                    {
                        dgvAcceptance.Rows[e.RowIndex].Cells["ColUserName"].Value = cardForm.userData.Name;
                        dgvAcceptance.Rows[e.RowIndex].Cells["ColDateTime"].Value = DateTime.Now.ToString("HH:mm:ss");

                        if (requiredAccess == "Technician")
                        {
                            ChangeOver.technician = cardForm.userData;
                        }
                        else
                        {
                            ChangeOver.oqa = cardForm.userData;
                        }
                    }
                }

                

                if (dgvAcceptance.Rows[0].Cells["ColUserName"].Value != null & dgvAcceptance.Rows[1].Cells["ColUserName"].Value != null) 
                {
                    if (dgvAcceptance.Rows[0].Cells["ColUserName"].Value.ToString() != "" & dgvAcceptance.Rows[1].Cells["ColUserName"].Value.ToString() != "")
                        bChangeOverFinish.Text = "Zakończ przestawienie";
                }
            }
        }

        private void lvProdNorms_Click(object sender, EventArgs e)
        {
            
        }

        private void lvProdNorms_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //var i = e.Item;//21 szt.
            //if(i.Text == "PCB / MB:")
            //{
            //    var qty = int.Parse(i.SubItems[1].Text.Replace(" szt.", ""));
            //    using(ChangePcbPerMbQty changeForm = new ChangePcbPerMbQty())
            //    {
            //        if(changeForm.ShowDialog() == DialogResult.OK)
            //        {
            //            DevTools.PcbPerMbOverwritenByUser = changeForm.qty;
            //            CurrentMstOrder.UpdateListViewProductionNorms();
            //        }
            //    }
            //}
        }

        private void lvProdNorms_ItemActivate(object sender, EventArgs e)
        {
            int idx = lvProdNorms.SelectedIndices[0];
            var i = lvProdNorms.Items[idx];
            if (i.Text == "PCB / MB:")
            {
                var qty = int.Parse(i.SubItems[1].Text.Replace(" szt.", ""));
                using (ChangePcbPerMbQty changeForm = new ChangePcbPerMbQty())
                {
                    if (changeForm.ShowDialog() == DialogResult.OK)
                    {
                        DevTools.PcbPerMbOverwritenByUser = changeForm.qty;
                        CurrentMstOrder.UpdateListViewProductionNorms();
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void rbLedShowOnlyCurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLedShowAll.Checked)
            {
                olvLedsUsed.ModelFilter = null;
            }
            else
            {
                olvLedsUsed.ModelFilter = new ModelFilter(delegate (object x) {
                    return ((LedsUsed.LedsUsedStruct)x).CurrentlyInUse;
                });
            }
        }

        private void olvLedsUsed_BeforeCreatingGroups(object sender, CreateGroupsEventArgs e)
        {
            
        }

        private void olvLedsUsed_BeforeSorting(object sender, BeforeSortingEventArgs e)
        {
            //if (e.ColumnToSort == null)
            //{
                
            //    Debug.WriteLine("sorting skipped");
            //    return;
            //}
            //// example sort based on the last letter of the object name
            //var s = new OLVColumn();
            //s.AspectGetter = (o) => ((LedsUsed.LedsUsedStruct)o).SortPriority;

            //olvLedsUsed.ListViewItemSorter = new ColumnComparer(
            //            s, SortOrder.Ascending, e.ColumnToSort, e.SortOrder);
            //e.Handled = true;
        }
    }
}
