using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.DataStructures;
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
    public partial class UpdateOrderQuantity : Form
    {
        private readonly MstOrder currentOrder;
        public int result = 0;
        int pcbPerMb = 1;

        public UpdateOrderQuantity(MstOrder currentOrder)
        {
            InitializeComponent();
            this.currentOrder = currentOrder;
        }

        private void UpdateOrderQuantity_Load(object sender, EventArgs e)
        {
            if (currentOrder.modelInfo.DtModel00 != null)
            {
                pcbPerMb = (int)MST.MES.DtTools.GetPcbPerMbCount(currentOrder.modelInfo.DtModel00);
                lPcbQtyInfo.Text = $"{pcbPerMb} PCB / MB  => ";
            }
            else
            {
                lPcbQtyInfo.Text = $"PCB/MB Brak danych => ";
            }

            textBox1.Text = (currentOrder.ManufacturedQty/ pcbPerMb).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
            lPcbQtyCalculated.Text = (int.Parse(textBox1.Text) * pcbPerMb).ToString();
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            int val = int.Parse(textBox1.Text);
            textBox1.Text = (val + 1).ToString();
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            int val = int.Parse(textBox1.Text);
            if (val > 0)
            {
                textBox1.Text = (val - 1).ToString();
            }
            
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            result = int.Parse(textBox1.Text) * pcbPerMb;
            this.DialogResult = DialogResult.OK;
        }


    }
}
