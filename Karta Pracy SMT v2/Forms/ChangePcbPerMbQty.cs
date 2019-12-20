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
    public partial class ChangePcbPerMbQty : Form
    {
        public int qty = 0;
        public ChangePcbPerMbQty()
        {
            InitializeComponent();
        }

        private void ChangePcbPerMbQty_Load(object sender, EventArgs e)
        {
            tbQty.Text = DevTools.CurrentModelPcbPerMb.ToString();
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            var qty = int.Parse(tbQty.Text);
            tbQty.Text = (qty + 1).ToString();
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            var qty = int.Parse(tbQty.Text);
            if (qty > 0)
            {
                tbQty.Text = (qty - 1).ToString();
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            qty = int.Parse(tbQty.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
