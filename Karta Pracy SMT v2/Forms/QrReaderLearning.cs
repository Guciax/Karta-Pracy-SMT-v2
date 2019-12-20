using Karta_Pracy_SMT_v2.DataStorage;
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

namespace Karta_Pracy_SMT_v2.Forms
{
    public partial class QrReaderLearning : Form
    {

        public QrReaderLearning()
        {
            InitializeComponent();
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                GlobalParameters.QrReaderName = KeyboardDeviceListener.lastInputDeviceName;
                this.Close();
            }
        }

        private void QrReaderLearning_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }
    }
}
