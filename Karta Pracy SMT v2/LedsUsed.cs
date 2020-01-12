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
using static Graffiti.MST.ComponentsTools;

namespace Karta_Pracy_SMT_v2
{
    public class LedsUsed
    {
        public static ObjectListView olvLedsUsed;
        public static List<LedsUsedStruct> ledsUsedList
        {
            get
            {
                return ComponentsOnRw.LedCollection.Select(x => new LedsUsedStruct { ConnectedComponentFromRwList = x }).ToList();
            }
        }

        public class LedsUsedStruct
        {
            public int SortPriority 
            { 
                get 
                {
                    if (CurrentlyInUse) return 0;
                    if (ComponentInTrash) return 2;
                    return 1;
                } 
            }
            public ComponentStruct ConnectedComponentFromRwList { get; set; }
            public string qrCode
            {
                get { return ConnectedComponentFromRwList.qrCode; }
            }
            public bool ComponentInTrash
            {
                get { return ConnectedComponentFromRwList.attributesCechy.InTrash; }
            }
            public string Nc12
            {
                get { return $"{Collective12Nc} {RankId}"; }
            }
            public string Nc12_Formated
            {
                get { return ConnectedComponentFromRwList.Nc12_Formated_Rank; }
            }
            public string Collective12Nc
            {
                get { return ConnectedComponentFromRwList.Nc12; }
            }
            public string RankId
            {
                get { return ConnectedComponentFromRwList.Rank; }
            }
            public string Id
            {
                get { return ConnectedComponentFromRwList.Id; }
            }
            public int Qty
            {
                get { return (int)ConnectedComponentFromRwList.Quantity; }
            }
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

            public bool CurrentlyInUse { get; set; }

            public Color BackGround
            {
                get
                {
                    if (CurrentlyInUse) return MST.MES.Colors.MaterialColorPalettes.Green().light;
                    if (ComponentInTrash) return MST.MES.Colors.MaterialColorPalettes.Grey().ulatraDark;
                    return Color.White;
                }
            }
            public Color ForeGround
            {
                get
                {
                    if (CurrentlyInUse) return Color.Black;
                    if (ComponentInTrash) return Color.White;
                    return MST.MES.Colors.MaterialColorPalettes.Grey().light;
                }
            }
        }

        public static void DebugAddLed()
        {
            //AddLedToList("401056011381", "HZ2E-LKJ", "100001", 12000);
            //AddLedToList("401056011381", "HZ2E-LKJ", "100002", 12000);
            //AddLedToList("401056011381", "HZ2E-LKJ", "100003", 12000);

            //MoveLedToTrash("401056011381", "100001");
        }


        public static void AddNewLed(Graffiti.MST.ComponentsTools.ComponentStruct componentGraffitiData)
        {
            var matchingComponents = ledsUsedList.Where(x => x.qrCode == componentGraffitiData.qrCode);
            if (!matchingComponents.Any())
            {
                MessageBox.Show($"Ta dioda nie aktualnie przypisana jest do tego zlecenia.");
                return;
            }
            LedsUsedStruct matchingReel = matchingComponents.First();

            if (ledsUsedList.Select(x => x.CurrentlyInUse).Count() >= 4) 
            {
                MessageBox.Show("Przenieś diody do kosza aby dodać nowe." + Environment.NewLine + "Max. 2 rolki w użyciu na każde 12NC diody.");
                return;
            }

            if(matchingReel.CurrentlyInUse)
            {
                MessageBox.Show("Ta dioda jest aktualnie w użyciu. ");
                return;
            }
            if (matchingReel.CurrentlyInUse)
            {
                MessageBox.Show("Ta dioda została już zużyta i przeniesiona do kosza.");
                return;
            }

            matchingReel.CurrentlyInUse = true;
            olvLedsUsed.SetObjects(ledsUsedList);
        }


        public static void MoveLedToTrash(string qrCode)
        {
            var matchingComponents = ledsUsedList.Where(x => x.qrCode == qrCode);
            if (!matchingComponents.Any())
            {
                MessageBox.Show("Brak rolki LED na liście dodanych.");
                return;
            }
            
            ComponentsOnRw.TrashComponent(qrCode);
            olvLedsUsed.SetObjects(ledsUsedList);
        }

        public static void ClearList()
        {
            ComponentsOnRw.ClearList();
            olvLedsUsed.Items.Clear();
            olvLedsUsed.SetObjects(ledsUsedList);
        }
    }
}
