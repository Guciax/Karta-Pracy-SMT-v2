using BrightIdeasSoftware;
using Karta_Pracy_SMT_v2.CurrentOrder;
using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Karta_Pracy_SMT_v2.PcbUsedInOrder;

namespace Karta_Pracy_SMT_v2.Forms
{
    public partial class EndOrder : Form
    {
        public EndOrder()
        {
            InitializeComponent();
        }

        public int finalQty = 0;

        private void EndOrder_Load(object sender, EventArgs e)
        {
            foreach (var led in LedsUsed.ledsInUseList)
            {
                if (led.ComponentInTrash) led.QtyNew = 0;
                else led.QtyNew = led.Qty;
            }

            olvLeds.SetObjects(LedsUsed.ledsInUseList);
            olvPcb.SetObjects(PcbUsedInOrder.pcbUsedList);

            olvLeds.ColumnsInDisplayOrder[3].AspectGetter = delegate (object rowObject) {
                LedsUsed.LedsUsedStruct model = rowObject as LedsUsed.LedsUsedStruct;
                return model.QtyNew == 0;
            };

            olvLeds.ColumnsInDisplayOrder[3].AspectPutter = delegate (object rowObject, object value) {
                LedsUsed.LedsUsedStruct model = rowObject as LedsUsed.LedsUsedStruct;
                if ((bool)value) model.QtyNew = 0;
                else model.QtyNew = model.Qty;
            };

            tbManufacuredQty.Text = Math.Round(CurrentMstOrder.currentOrder.ManufacturedQty / DevTools.CurrentModelPcbPerMb, 0).ToString();
            tbNg.Text = CurrentMstOrder.currentOrder.NgQty.ToString();
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            tbManufacuredQty.Text = (int.Parse(tbManufacuredQty.Text) + 1).ToString();
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            if (tbManufacuredQty.Text == "0") return;
            tbManufacuredQty.Text = (int.Parse(tbManufacuredQty.Text) - 1).ToString();
        }

        private void tbManufacuredQty_TextChanged(object sender, EventArgs e)
        {
            lPcbQty.Text = (int.Parse(tbManufacuredQty.Text) * DevTools.CurrentModelPcbPerMb).ToString();
        }

        private void bNgDown_Click(object sender, EventArgs e)
        {
            tbNg.Text = (int.Parse(tbNg.Text) + 1).ToString();
        }

        private void bNgUp_Click(object sender, EventArgs e)
        {
            if (tbNg.Text == "0") return;
            tbNg.Text = (int.Parse(tbNg.Text) - 1).ToString();
        }

        private void tbNg_TextChanged(object sender, EventArgs e)
        {
            lLedScrap.Text = (int.Parse(tbNg.Text) * CurrentMstOrder.currentOrder.modelInfo.LedCount).ToString();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            //try
            {
                foreach (var led in LedsUsed.ledsInUseList)
                {
                    //if (led.QtyNew > 0) continue;
                    //MST.MES.SqlOperations.SparingLedInfo.UpdateLedQuantity(led.Nc12, led.Id, "0");
                    //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentQty($"{led.Nc12}|ID:{led.Id}", 0);
                    //In Graffiti - do nothing!
                }
                foreach (var pcb in PcbUsedInOrder.pcbUsedList)
                {
                    int newQty = 0;
                    foreach (ListViewItem olvItem in olvPcb.Items)
                    {
                        if(olvItem.SubItems[1].Text  == pcb.Id)
                        {
                            newQty = int.Parse(olvItem.SubItems[2].Text);
                            break;
                        }
                    }
                    if (newQty == 0) continue;

                    Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentQty(pcb.qrCode, newQty);
                    

                    

                    //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(pcb.Nc12, pcb.Id, pcb.OriginalLocation);
                    //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{pcb.Nc12}|ID:{pcb.Id}", pcb.OriginalLocation);
                }
                finalQty = int.Parse(lPcbQty.Text);
                this.DialogResult = DialogResult.OK;
            }
            //catch
            {
                //MessageBox.Show("Błąd połączenia z bazą danych. Spróbuj ponownie za jakiś czas.");
            }
        }
        
        private void olvPcb_ButtonClick(object sender, CellClickEventArgs e)
        {
            PcbUsedStruct field = (PcbUsedStruct)e.Model;
            var item = e.Item.SubItems[2];
            int currentQty = int.Parse(item.Text);

            if (e.Column.AspectName == "UpNewQty")
            {
                if (currentQty >= field.Qty) return;
                field.QtyNew = currentQty + 1;
                var id = field.Id;
                var pcbInList = PcbUsedInOrder.pcbUsedList.Where(pcb => pcb.Id == id).First();
                pcbInList.QtyNew = currentQty + 1;
                olvPcb.RefreshItem(e.Item);
            }
            if (e.Column.AspectName == "DownNewQty" & currentQty > 0)
            {
                field.QtyNew = currentQty - 1;
                olvPcb.RefreshItem(e.Item);
            }
        }
    }
}
