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
            public string Nc12
            {
                get { return $"{Collective12Nc} {RankId}"; }
            }
            public string Nc12_Formated
            {
                get
                {
                    return Nc12.Insert(4, " ").Insert(8, " ");
                }
            }
            public string Collective12Nc { get; set; }
            public string RankId { get; set; }
            public string Id { get; set; }
            public int Qty { get; set; }
            public int QtyNew { get; set; }
            //public string Bin { get; set; }
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
            AddLedToList("401056011381", "HZ2E-LKJ", "100001", 12000);
            AddLedToList("401056011381", "HZ2E-LKJ", "100002", 12000);
            AddLedToList("401056011381", "HZ2E-LKJ", "100003", 12000);

            MoveLedToTrash("401056011381", "100001");
        }

        public static void AddNewLed(string collective12Nc, string id)
        {
            if (ledsUsedList.Where(x => x.Nc12 == collective12Nc & x.QtyNew > 0).Count() > 2)
            {
                MessageBox.Show("Przenieś diody do kosza aby dodać nowe." + Environment.NewLine + "Max. 2 rolki w użyciu na każde 12NC diody.");
                return;
            }

            if(ledsUsedList.Where(x=>x.Nc12==collective12Nc & x.Id == id).Count() > 0)
            {
                MessageBox.Show("Ta dioda została już dodana." + Environment.NewLine + $"12NC: {collective12Nc}" + Environment.NewLine + $"ID: {id}");
                return;
            }

            //DataTable reelTable = MST.MES.SqlOperations.SparingLedInfo.GetInfoFor12NC_ID(nc12, id);
            var reelFromGraffiti = Graffiti.MST.ComponentsTools.GetDbData.GetComponentData($"{collective12Nc}|ID:{id}");

            //if (reelTable.Rows.Count == 0)
            if (reelFromGraffiti == null)
            {
                MessageBox.Show("Brak informacji tym kodzie w bazie danych.");
                return;
            }

            string qty = reelFromGraffiti.Quantity.ToString();
            string zlecenieString = reelFromGraffiti.ConnectedToOrder.ToString();
            string rankId = reelFromGraffiti.Rank;
            
            if (zlecenieString != CurrentMstOrder.currentOrder.OrderNo & zlecenieString != CurrentMstOrder.currentOrder.KittingData.connectedOrder)
            {
                MessageBox.Show($"Ta dioda aktualnie przypisana jest do zlecenia: {zlecenieString}");
                return;
            }

            AddLedToList(collective12Nc, rankId, id, int.Parse(qty));
        }
        private static void AddLedToList(string collective12Nc,string rank, string id, int qty)
        {
            LedsUsedStruct newLed = new LedsUsedStruct
            {
                Collective12Nc = collective12Nc,
                RankId = rank,
                Id = id,
                Qty = qty,
                QtyNew = qty
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
            ledsUsedList = new List<LedsUsedStruct>();
            olvLedsUsed.Items.Clear();
            olvLedsUsed.SetObjects(ledsUsedList);
        }
    }
}
