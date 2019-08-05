using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.Forms;
using RawInput_dll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OrdersHistory.dgvOrders = dgvOrders;
            CurrentMstOrder.lvOrderInfo = lfOrderInfo;
            CurrentMstOrder.lvProdNorms = lvProdNorms;
            LedsUsed.olvLedsUsed = olvLedsUsed;

            PcbUsedInOrder.olvPcbUsed = olvPcbUsed;

            QrReader.rawinput = new RawInput(Handle);
            QrReader.rawinput.KeyPressed += QrReader.rawinput_KeyPressed;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await MesData.AsyncLoader();
            OrdersHistory.MesOrdersToOrdersHistory(20);

            GlobalParameters.SmtLine = SmtLineFile.ReadLine();

#if DEBUG
            bDebug.Visible = true;
            GlobalParameters.Debug = true;
#endif
            lSmtLine.Text = GlobalParameters.SmtLine;
        }

        private void tDataUpdates_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - DevTools.lastUpdateTime).TotalHours > 1) 
            {
                try
                {
                    DevTools.DtDb = MST.MES.Data_structures.DevTools.DevToolsLoader.LoadDevToolsModels();
                }
                catch
                {
                    DevTools.lastUpdateSuccesfull = false;
                }
            }
        }

        private void bNewOrder_Click(object sender, EventArgs e)
        {
            using(NewOrder newOrderForm = new NewOrder())
            {
                if(newOrderForm.ShowDialog() == DialogResult.OK)
                {
                    CurrentMstOrder.currentOrder = newOrderForm.dialogResult;
                    LedsUsed.ClearList();
                    PcbUsedInOrder.ClearList();
                }

                if (GlobalParameters.Debug)
                {
                    LedsUsed.DebugAddLed();
                    PcbUsedInOrder.DebugAddPcb();
                }

            }
        }

        private void bAddLedQr_Click(object sender, EventArgs e)
        {
            if (CurrentMstOrder.currentOrder != null)
            {
                using (ScanLedQr scanForm = new ScanLedQr())
                {
                    if (scanForm.ShowDialog() == DialogResult.OK)
                    {
                        LedsUsed.AddNewLed(scanForm.nc12, scanForm.id);
                    }
                }
            }
            else { MessageBox.Show("Wczytaj najpierw zlecenie."); }
            
        }

        private void bMoveToTrash_Click(object sender, EventArgs e)
        {
            if (LedsUsed.ledsUsedList.Count == 0) return;
            using(ScanLedQr scanForm = new ScanLedQr())
            {
                if(scanForm.ShowDialog() == DialogResult.OK)
                {
                    LedsUsed.MoveLedToTrash(scanForm.nc12, scanForm.id);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            CurrentMstOrder.UpdateOrderQty();
        }

        private void bDebug_Click(object sender, EventArgs e)
        {
            

            
        }

        private void tClock_Tick(object sender, EventArgs e)
        {
            lClock.Text = $"{DateTime.Now.ToString("HH:mm:ss")}    {GlobalParameters.SmtLine}";
            CurrentMstOrder.UpdateOrderQty();
            EfficiencyChart.AddPoint(pbEfficiencyChart);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QrReaderLearning learnQrForm = new QrReaderLearning();
            learnQrForm.ShowDialog();
        }

        private void bFinishOrder_Click(object sender, EventArgs e)
        {
            if (CurrentMstOrder.currentOrder == null) return;
            using(EndOrder endForm = new EndOrder())
            {
                if(endForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void bAddPcb_Click(object sender, EventArgs e)
        {
            if (CurrentMstOrder.currentOrder == null)
            {
                MessageBox.Show("Wczytaj najpierw zlecenie.");
                return;
            }

            using (ScanLedQr scanForm = new ScanLedQr())
            {
                if (scanForm.ShowDialog() == DialogResult.OK)
                {
                    PcbUsedInOrder.AddNewPcb(scanForm.nc12, scanForm.id);
                }
            }

        }

        private void bMovePcbToTrash_Click(object sender, EventArgs e)
        {
            if (PcbUsedInOrder.pcbUsedList.Count == 0) return;
            using (ScanLedQr scanForm = new ScanLedQr())
            {
                if (scanForm.ShowDialog() == DialogResult.OK)
                {
                    PcbUsedInOrder.MovePcbToTrash(scanForm.nc12, scanForm.id);
                }
            }
        }

        private void olvLedsUsed_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            
        }

        private void olvLedsUsed_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            var ledRecord = (LedsUsed.LedsUsedStruct)e.Model;
            if (ledRecord.QtyNew == 0)
            {
                e.Item.BackColor = Color.Black;
                e.Item.ForeColor = Color.White;
            }
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
    }
}
