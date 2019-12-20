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

        public int result = 0;
        int pcbPerMb = 1;

        public UpdateOrderQuantity()
        {
            InitializeComponent();

        }

        private void UpdateOrderQuantity_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = BlurredBackground.ssGrayColor;
            if (CurrentMstOrder.currentOrder.modelInfo.DtModel00 != null)
            {
                pcbPerMb = (int)DevTools.CurrentModelPcbPerMb;
                lPcbQtyInfo.Text = $"{pcbPerMb} PCB / MB  => ";
            }
            else
            {
                lPcbQtyInfo.Text = $"PCB/MB Brak danych => ";
            }
            textBox1.Text = (CurrentMstOrder.currentOrder.ManufacturedQty/ pcbPerMb).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
            lPcbQtyCalculated.Text = (int.Parse(textBox1.Text) * pcbPerMb).ToString();
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            int val = 0;
            if(!int.TryParse(textBox1.Text, out val))
            {
                textBox1.Text = "0";
                return;
            }
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
