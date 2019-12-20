using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    public class ChangeOver
    {
        public static DateTime ChangeOverStart = DateTime.MinValue;
        public static string FromModel { get; set; }
        public static string ToModel { get; set; }
        public static Panel ChangeOverPanel;
        public static Timer ChangeOverTimer;
        public static DataGridView dgvAcceptance;
        public static bool changeOverInProgress = false;
        public static MST.MES.UsersDataBase.DataStructures.UserStructure technician { get; set; }
        public static MST.MES.UsersDataBase.DataStructures.UserStructure oqa { get; set; }

        public static void StartChangeOver()
        {
            if (changeOverInProgress) return;
            ChangeOverStart = DateTime.Now;
            ChangeOverPanel.Visible = true;
            ChangeOverPanel.BackColor = Color.FromArgb(255, 26, 188, 156);
            ChangeOverTimer.Enabled = true;
            changeOverInProgress = true;

            dgvAcceptance.Rows.Add("Technik:", "", "Dodaj...");
            dgvAcceptance.Rows.Add("Inspektor OQA:", "", "Dodaj...");
        }

        public static void FinishChangeOver()
        {
            if (!changeOverInProgress) return;
            if (CurrentMstOrder.currentOrder == null) 
            {
                MessageBox.Show("Wczytaj zlecenie");
                return;
            }

            ChangeOverPanel.Visible = false;
            ChangeOverTimer.Enabled = false;
            
            CurrentMstOrder.currentOrder.SmtData.smtStartDate = DateTime.Now;
            SqlOperations.InsertChangeOverRecordToDb(ChangeOverStart,
                                                    DateTime.Now,
                                                    GlobalParameters.SmtLine,
                                                    CurrentMstOrder.currentOrder.SmtData.operatorSmt,
                                                    CurrentMstOrder.currentOrder.OrderNo,
                                                    CurrentMstOrder.currentOrder.Model10Nc,
                                                    ChangeOver.technician.Name,
                                                    ChangeOver.oqa.Name);

            dgvAcceptance.Rows.Clear();
            ChangeOverStart = DateTime.MinValue;
            changeOverInProgress = false;
            CurrentMstOrder.currentOrder.SmtData.smtStartDate = DateTime.Now;
            CurrentMstOrder.UpdateListViewOrderInfo();
        }
    }
}
