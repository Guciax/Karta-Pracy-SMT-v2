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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QrReaderLearning learnQrForm = new QrReaderLearning();
            learnQrForm.ShowDialog();
        }

        private void bCardReaderProgramming_Click(object sender, EventArgs e)
        {
            using (ReadRfidCard readForm = new ReadRfidCard())
            {
                if(readForm.ShowDialog() == DialogResult.OK)
                {
                    GlobalParameters.CardReaderName = readForm.deviceMonikerString;
                }
            }
        }
    }
}
