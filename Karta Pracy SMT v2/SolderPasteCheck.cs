using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    
    public class SolderPasteCheck
    {
        public static Form mainForm;
        private static Panel alertPanel = null;
        private static DateTime lastCheckTime = DateTime.Now;

        private static void ShowPanel()
        {
            if(alertPanel == null)
            {
                Initialize();
            }
            alertPanel.Visible = true;
        }
        private static void Initialize()
        {
            lastCheckTime = DateTime.Now;

            alertPanel = new Panel();
            alertPanel.BackColor = Color.FromArgb(255, 255, 82, 82);
            alertPanel.Parent = mainForm;
            alertPanel.BringToFront();
            alertPanel.Dock = DockStyle.Fill;

            Button bOk = new Button();
            bOk.Parent = alertPanel;
            bOk.FlatStyle = FlatStyle.Flat;
            bOk.BackColor = Color.White;
            bOk.Text = "OK";
            bOk.Click += BOk_Click;
            bOk.Font = new Font("Arial", 40, FontStyle.Regular);
            bOk.Dock = DockStyle.Bottom;
            bOk.Height = 80;
            
            Label lInfo = new Label();
            lInfo.Parent = alertPanel;
            lInfo.Text = "Sprawdź pastę lutowniczą!";
            lInfo.AutoSize = false;
            lInfo.TextAlign = ContentAlignment.MiddleCenter;
            lInfo.Dock = DockStyle.Fill;
            lInfo.Font = new Font("Arial", 80, FontStyle.Regular);

        }

        private static void BOk_Click(object sender, EventArgs e)
        {
            lastCheckTime = DateTime.Now;
            alertPanel.Visible = false;
        }

        internal static void CheckIfNeedToShowAlert()
        {
            if((DateTime.Now - lastCheckTime).TotalMinutes > 1)
            {
                ShowPanel();
            }
        }
    }
}
