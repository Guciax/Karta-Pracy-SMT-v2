using Karta_Pracy_SMT_v2.Forms;
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
        private static bool alertIsDisplayed = false;


        internal static void CheckIfNeedToShowAlert()
        {
            if (alertIsDisplayed) return;
            if((DateTime.Now - lastCheckTime).TotalMinutes > 60)
            {
                alertIsDisplayed = true;
                using(CheckSolderPasteForm spForm = new CheckSolderPasteForm())
                {
                    if(spForm.ShowDialog() == DialogResult.OK)
                    {
                        alertIsDisplayed = false;
                        lastCheckTime = DateTime.Now;
                    }
                }
            }
        }
    }
}
