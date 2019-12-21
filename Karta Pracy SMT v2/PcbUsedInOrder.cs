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
            private int _qtyNew;

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
            public int QtyNew
            {
                get { return _qtyNew; }
                set
                {
                    if (value < 0)
                    {
                        _qtyNew = 0;
                    }
                    _qtyNew = value;
                }
            }
            public Bitmap StatusIcon
            {
                get
                {
                    if (QtyNew > 0) return Karta_Pracy_SMT_v2.Properties.Resources.InUse_Black;
                    return Karta_Pracy_SMT_v2.Properties.Resources.Trash_White;
                }
            }

            public string UpNewQty
            {
                get { return "+"; }
            }

            public string DownNewQty
            {
                get { return "-"; }

            }

            public string OriginalLocation { get; set; }
        }

        public static void DebugAddPcb()
        {
            //AddNewPcb("401044101312", "100096");
            //AddNewPcb("401044101312", "100097");
            //AddNewPcb("401044101312", "100060");
            //MovePcbToTrash("401044101312", "100060");
        }

        public static bool AddNewPcb(string nc12, string id)
        {
            if(pcbUsedList.Where(x=>x.Nc12 == nc12 & x.Id == id).Count() > 0)
            {
                MessageBox.Show("Ta płyta PCB została już dodana." + Environment.NewLine + $"12NC: {nc12}" + Environment.NewLine + $"ID: {id}");
                return false;
            }

            //DataTable reelTable = MST.MES.SqlOperations.SparingLedInfo.GetInfoFor12NC_ID(nc12, id);
            var pcbFromGraffiti = Graffiti.MST.ComponentsTools.GetDbData.GetComponentData($"{nc12}|ID:{id}");
            if (pcbFromGraffiti == null)
            {
                MessageBox.Show("Brak informacji o tym kodzie w bazie danych.");
                return false;
            }
            //Graffiti needs new rule
            //if(reelTable.Rows[0]["Z_RegSeg"].ToString().ToUpper() == "VERTE")
            //{
            //    MessageBox.Show("Ten komponent nie został przyjęty na wejsciu do produkcji.");
            //    return false;
            //}


            int qty = (int)pcbFromGraffiti.Quantity;
            string location = pcbFromGraffiti.Location;
            //AddLedToListView(nc12, id, qty, binId);
            AddPcbToList(nc12, id, qty, location);
            return true;
        }

        private static void AddPcbToList(string nc12, string id, int qty, string originalLocation)
        {
            PcbUsedStruct newLed = new PcbUsedStruct
            {
                Nc12 = nc12,
                Id = id,
                Qty = qty,
                QtyNew = qty,
                OriginalLocation = originalLocation
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

            //MST.MES.SqlOperations.SparingLedInfo.UpdateLedQuantity(nc12, id, "0");
            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentQty($"{nc12}|ID:{id}", 0);
            //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(nc12, id, "KOSZ");
            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{nc12}|ID:{id}", "KOSZ");
            items.First().QtyNew = 0;
            items.First().Qty = 0; //both = 0 meaning saved to db.
            olvPcbUsed.UpdateObject(items.First());
        }

        internal static void ClearList()
        {
            pcbUsedList.Clear();
            olvPcbUsed.Items.Clear();
            olvPcbUsed.SetObjects(pcbUsedList);
        }
    }
}
