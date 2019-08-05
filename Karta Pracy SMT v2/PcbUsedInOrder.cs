using BrightIdeasSoftware;
using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    public class PcbUsedInOrder
    {
        public static ObjectListView olvPcbUsed;
        public static List<PcbUsedStruct> pcbUsedList = new List<PcbUsedStruct>();

        public class PcbUsedStruct
        {
            public string Nc12 { get; set; }
            public string Nc12_Formated
            {
                get
                {
                    return Nc12.Insert(4, " ").Insert(8, " ");
                }
            }

            public string Id { get; set; }
            public int Qty { get; set; }
            public int QtyNew { get; set; }
            public Bitmap StatusIcon
            {
                get
                {
                    if (QtyNew > 0) return Karta_Pracy_SMT_v2.Properties.Resources.InUse_Black;
                    return Karta_Pracy_SMT_v2.Properties.Resources.Trash_White;
                }
            }
        }

        public static void DebugAddPcb()
        {
            AddNewPcb("401044101312", "100096");
            AddNewPcb("401044101312", "100097");
            AddNewPcb("401044101312", "100060");

            MovePcbToTrash("401044101312", "100060");
        }

        public static void AddNewPcb(string nc12, string id)
        {
            DataTable reelTable = MST.MES.SqlOperations.SparingLedInfo.GetInfoFor12NC_ID(nc12, id);
            if (reelTable.Rows.Count == 0)
            {
                MessageBox.Show("Brak informacji tym kodzie w bazie danych.");
                return;
            }
            int qty = int.Parse(reelTable.Rows[0]["Ilosc"].ToString());


            //AddLedToListView(nc12, id, qty, binId);
            AddPcbToList(nc12, id, qty);
        }

        private static void AddPcbToList(string nc12, string id, int qty)
        {
            PcbUsedStruct newLed = new PcbUsedStruct
            {
                Nc12 = nc12,
                Id = id,
                Qty = qty,
                QtyNew = qty
            };
            pcbUsedList.Add(newLed);
            olvPcbUsed.SetObjects(pcbUsedList);
            //olvPcbUsed.AutoResizeColumns();
        }

        public static void MovePcbToTrash(string nc12, string id)
        {
            var items = pcbUsedList.Where(x => x.Nc12 == nc12 & x.Id == id);
            if (items.Count() == 0)
            {
                MessageBox.Show("Brak płyty PCB na liście.");
                return;
            }

            items.First().QtyNew = 0;
            olvPcbUsed.UpdateObject(items.First());
        }



        internal static void ClearList()
        {
            pcbUsedList.Clear();
            olvPcbUsed.UpdateObjects(pcbUsedList);
        }
    }
}
