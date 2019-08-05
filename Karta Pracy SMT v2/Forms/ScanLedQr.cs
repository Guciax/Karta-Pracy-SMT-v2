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
    public partial class ScanLedQr : Form
    {
        public string nc12;
        public string id;
        public ScanLedQr()
        {
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                if(QrReader.lastInputDeviceName!= GlobalParameters.QrReaderName)
                {
                    MessageBox.Show("Użytj czytnika QR.");
                    textBox1.Text = "";
                    return;
                }

                string[] split = textBox1.Text.Split('\t');
                if (split.Length > 4)
                {
                    id = split[5];
                    nc12 = split[0];
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Niepoprawny kod QR" + Environment.NewLine + textBox1.Text);
                    textBox1.Text = "";
                }
            }
        }

        private void ScanLedQr_Load(object sender, EventArgs e)
        {

        }
    }
}
