using BrightIdeasSoftware;
using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    public class LedsUsed
    {
        public static ObjectListView olvLedsUsed;
        public static List<LedsUsedStruct> ledsUsedList = new List<LedsUsedStruct>();

        public class LedsUsedStruct
        {
            public string Nc12 { get; set; }
            public string Nc12_Formated { get {
                    return Nc12.Insert(4, " ").Insert(8, " ");
                } }

            public string Id { get; set; }
            public int Qty { get; set; }
            public int QtyNew { get; set; }
            public string Bin { get; set; }
            public Bitmap StatusIcon
            {
                get
                {
                    if (QtyNew > 0) return Karta_Pracy_SMT_v2.Properties.Resources.InUse_Black;
                    return Karta_Pracy_SMT_v2.Properties.Resources.Trash_White;
                }
            }
        }

        public static void DebugAddLed()
        {
            AddLedToList("401056011381", "100001", 12000, "A");
            AddLedToList("401056011381", "100002", 12000, "A");
            AddLedToList("401056011381", "100003", 12000, "A");

            MoveLedToTrash("401056011381", "100001");
        }

        public static void AddNewLed(string nc12, string id)
        {
            DataTable reelTable = MST.MES.SqlOperations.SparingLedInfo.GetInfoFor12NC_ID(nc12, id);
            if (reelTable.Rows.Count == 0)
            {
                MessageBox.Show("Brak informacji tym kodzie w bazie danych.");
                return;
            }
            string qty = reelTable.Rows[0]["Ilosc"].ToString();
            string binId = reelTable.Rows[0]["Tara"].ToString();
            string zlecenieString = reelTable.Rows[0]["ZlecenieString"].ToString();

            if (zlecenieString != CurrentMstOrder.currentOrder.OrderNo)
            {
                MessageBox.Show($"Ta rolka aktualnie przypisana jest do zlecenia: {zlecenieString}");
                return;
            }

            AddLedToList(nc12, id, int.Parse(qty), binId);
        }
        private static void AddLedToList(string nc12, string id, int qty, string bin)
        {
            LedsUsedStruct newLed = new LedsUsedStruct
            {
                Nc12 = nc12,
                Id = id,
                Qty = qty,
                QtyNew = qty,
                Bin = bin
            };
            ledsUsedList.Add(newLed);
            olvLedsUsed.SetObjects(ledsUsedList);
        }

        public static void MoveLedToTrash(string nc12, string id)
        {
            var items = ledsUsedList.Where(x => x.Nc12 == nc12 & x.Id == id);
            if (items.Count() == 0)
            {
                MessageBox.Show("Brak rolki LED na liście dodanych.");
                return;
            }

            items.First().QtyNew = 0;
            olvLedsUsed.UpdateObject(items.First());
        }

        public static void ClearList()
        {
            ledsUsedList.Clear();
            olvLedsUsed.UpdateObjects(ledsUsedList);
        }

        

    }
}
