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
        public string qrCode;
        public Graffiti.MST.ComponentsTools.ComponentStruct graffitiCompData = null;
        private readonly bool getDbData;

        public ScanLedQr(bool getDbData)
        {
            InitializeComponent();
            this.getDbData = getDbData;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                if(!GlobalParameters.Debug & KeyboardDeviceListener.lastInputDeviceName!= GlobalParameters.QrReaderName)
                {
                    MessageBox.Show("Użytj czytnika QR.");
                    textBox1.Text = "";
                    return;
                }
                string universalQrCode = textBox1.Text;
                if (textBox1.Text.Contains("|"))
                {
                    string[] split = textBox1.Text.Split(new string[] { "|ID:" }, StringSplitOptions.None);
                    if (split.Length != 2)
                    {
                        MessageBox.Show("Niepoprawny kod QR" + Environment.NewLine + textBox1.Text);
                        textBox1.Text = "";
                        return;
                    }
                    id = split[1];
                    nc12 = split[0];
                }
                else
                {
                    var oldQrCode = MST.MES.QrCode.DecodeQrCode(textBox1.Text);
                    if(oldQrCode == null)
                    {
                        MessageBox.Show("Niepoprawny kod QR" + Environment.NewLine + textBox1.Text);
                        textBox1.Text = "";
                        return;
                    }
                    universalQrCode = $"{oldQrCode.Nc12}|ID:{oldQrCode.Id}";
                    nc12 = oldQrCode.Nc12;
                    id = oldQrCode.Id;
                }

                graffitiCompData = Graffiti.MST.ComponentsTools.GetDbData.GetComponentData(universalQrCode);
                
                qrCode = textBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void ScanLedQr_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = BlurredBackground.ApplyBlur(BlurredBackground.ssGrayColor);
        }

        private void ScanLedQr_Resize(object sender, EventArgs e)
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
