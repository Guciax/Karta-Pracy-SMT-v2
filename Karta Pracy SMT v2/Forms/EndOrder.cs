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

namespace Karta_Pracy_SMT_v2.Forms
{
    public partial class EndOrder : Form
    {
        public EndOrder()
        {
            InitializeComponent();
        }

        private void EndOrder_Load(object sender, EventArgs e)
        {
            olvLeds.SetObjects(LedsUsed.ledsUsedList);
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

            tbManufacuredQty.Text = CurrentMstOrder.currentOrder.ManufacturedQty.ToString();
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
            lPcbQty.Text = (int.Parse(tbManufacuredQty.Text) * CurrentMstOrder.currentOrder.modelInfo.PcbPerMbCount).ToString();
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
    }
}
