using BrightIdeasSoftware;
using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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
        public static List<LedsUsedStruct> ledsInUseList { get; set; } = new List<LedsUsedStruct>();

        public static void SyncListWithRwList(List<ComponentStruct> newLedRwList)
        {
            foreach (var component in newLedRwList)
            {
                var matchinbLedComponent = ledsInUseList.Where(led => led.Collective12Nc + led.Id == component.Nc12 + component.Id).ToList();
                if (matchinbLedComponent.Any()) continue;
                ledsInUseList.Add(new LedsUsedStruct { ConnectedComponentFromRwList = component });
            }
            LedsUsed.RefreshDisplay();
        }
        public static void RefreshDisplay()
        {
            olvLedsUsed.SetObjects(ledsInUseList);
            if (olvLedsUsed.Items.Count > 0)
            {
                olvLedsUsed.RedrawItems(0, olvLedsUsed.Items.Count - 1, false);
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
            public string ListViewGroup
            {
                get
                {
                    if (CurrentlyInUse) return "W użyciu";
                    if (ComponentInTrash) return "W koszu";
                    return "Nie użyte";
                }
            }
            public ComponentStruct ConnectedComponentFromRwList { get; set; }
            public string qrCode
            {
                get { return ConnectedComponentFromRwList.QrCode; }
            }
            public bool ComponentInTrash
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(ConnectedComponentFromRwList.StatusTrash)) return false;
                    if (ConnectedComponentFromRwList.StatusTrash.ToUpper() == "KOSZ") return true;
                    return false;
                }
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
                get
                {
                    if (ComponentInTrash) return 0;
                    return (int)ConnectedComponentFromRwList.Quantity;
                }
            }
            public int QtyNew { get; set; }
            //public string Bin { get; set; }
            public Bitmap StatusIcon
            {
                get
                {
                    if (RefreshingDataRightNow) return Karta_Pracy_SMT_v2.Properties.Resources.spinnerIcon;
                    if (ComponentInTrash) return Karta_Pracy_SMT_v2.Properties.Resources.Trash_White;
                    if(CurrentlyInUse) return Karta_Pracy_SMT_v2.Properties.Resources.InUse_Black;
                    return Karta_Pracy_SMT_v2.Properties.Resources.available_gray;
                }
            }
            public bool CurrentlyInUseNotTrashed
            {
                get
                {
                    return (!ComponentInTrash & CurrentlyInUse);
                }
            }
            public bool RefreshingDataRightNow { get; set; }
            public bool CurrentlyInUse { get; set; }
            public Color BackGround
            {
                get
                {
                    if (ComponentInTrash) return Color.Black;
                    if (CurrentlyInUse) return MST.MES.Colors.MaterialColorPalettes.Green().light;
                    return Color.White;
                }
            }
            public Color ForeGround
            {
                get
                {
                    if (ComponentInTrash) return Color.White;
                    if (CurrentlyInUse) return Color.Black;
                    return MST.MES.Colors.MaterialColorPalettes.Grey().mainLighter;
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
        public static void AddNewLed(string qrCode)
        {
            var matchingComponents = ledsInUseList.Where(x => x.qrCode == qrCode);
            if (!matchingComponents.Any())
            {
                MessageBox.Show($"Ta dioda nie jest przypisana do tego zlecenia.");
                return;
            }

            LedsUsedStruct matchingReel = matchingComponents.First();

            if (ledsInUseList.Where(x => x.CurrentlyInUseNotTrashed).Count() >= 4) 
            {
                MessageBox.Show("Przenieś diody do kosza aby dodać nowe." + Environment.NewLine + "Max. 2 rolki w użyciu na każde 12NC diody.");
                return;
            }
            if (matchingReel.CurrentlyInUse) 
            {
                MessageBox.Show("Ta dioda jest aktualnie w użyciu. ");
                return;
            }
            matchingReel.CurrentlyInUse = true;
            olvLedsUsed.SetObjects(ledsInUseList);
        }
        public static async void MoveLedToTrash(string qrCode)
        {
            var matchingComponents = ledsInUseList.Where(x => x.qrCode == qrCode);
            if (!matchingComponents.Any())
            {
                MessageBox.Show("Brak rolki LED na liście dodanych.");
                return;
            }
            var ledReel = matchingComponents.First();
            if (!ledReel.CurrentlyInUse)
            {
                MessageBox.Show("Rolka nie jest w użyciu.");
                return;
            }
            ledReel.RefreshingDataRightNow = true;
            olvLedsUsed.Refresh();
            await Task.Run(() => ComponentsOnRw.TrashComponent(ledReel.ConnectedComponentFromRwList));
            ledReel.RefreshingDataRightNow = false;
            olvLedsUsed.SetObjects(ledsInUseList);
        }

        public static void ClearList()
        {
            ComponentsOnRw.ClearList();
            olvLedsUsed.Items.Clear();
            ledsInUseList = new List<LedsUsedStruct>();
            olvLedsUsed.SetObjects(ledsInUseList);
            olvLedsUsed.Refresh();
        }
    }
}
