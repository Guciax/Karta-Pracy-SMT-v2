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
    public partial class EnterPcbOnMbQtyManually : Form
    {
        public int currentQty = 0;
        public EnterPcbOnMbQtyManually()
        {
            InitializeComponent();
        }

        private void EnterPctOnMbQtyManually_Load(object sender, EventArgs e)
        {
            tbPcbPerMb.Text = "-1";
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            currentQty = int.Parse(tbPcbPerMb.Text);
            currentQty++;
            tbPcbPerMb.Text = currentQty.ToString();
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            currentQty = int.Parse(tbPcbPerMb.Text);
            if (currentQty > 0)
            {
                currentQty--;
                tbPcbPerMb.Text = currentQty.ToString();
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (currentQty > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
