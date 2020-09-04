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
using static Graffiti.MST.ComponentsTools;

namespace Karta_Pracy_SMT_v2
{
    public class PcbUsedInOrder
    {
        public static ObjectListView olvPcbUsed { get; set; }
        public static List<PcbUsedStruct> pcbUsedList { get; set; } = new List<PcbUsedStruct>();
        public class PcbUsedStruct
        {
            public Graffiti.MST.ComponentsTools.ComponentStruct ConnectedComponentFromRw { get; set; }
            public int SortPriority
            {
                get
                {
                    if (CurrentlyInUse) return 0;
                    if (ComponentInTrash) return 2;
                    return 1;
                }
            }
            public string qrCode
            {
                get { return ConnectedComponentFromRw.QrCode; }
            }
            public bool ComponentInTrash
            {
                get {
                    if (string.IsNullOrWhiteSpace(ConnectedComponentFromRw.StatusTrash)) return false;
                    return ConnectedComponentFromRw.StatusTrash.ToUpper() == "KOSZ";
                }
            }
            public bool RefreshingDataRightNow { get; set; }
            public string Nc12 { get { return ConnectedComponentFromRw.Nc12; } }
            public string Nc12_Formated { get { return ConnectedComponentFromRw.Nc12_Formated; } }
            public string Id { get { return ConnectedComponentFromRw.Id; } }
            public int Qty { get { return (int)ConnectedComponentFromRw.Quantity; } }
            private int _qtyNew;
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
            public string OriginalLocation { get; set; }

            public string UpNewQty { get { return "+"; } }
            public string DownNewQty { get { return "-"; } }
        }
        public static void SyncListWithPcbRwList(List<ComponentStruct> newPcbRwList)
        {
            pcbUsedList.Clear();
            var grouppedByNcId = newPcbRwList.GroupBy(x => x.Nc12 + x.Id);
            foreach (var pcb in grouppedByNcId)
            {
                double qty = 0;
                if (pcb.Last().Quantity < 0)
                {
                    qty = Math.Abs(pcb.Last().Quantity);
                }
                    
                    
                
                PcbUsedStruct newPcb = new PcbUsedStruct
                {
                    ConnectedComponentFromRw = pcb.Last(),
                    QtyNew = (int)qty
                };
                pcbUsedList.Add(newPcb);
            }

            //foreach (var component in newPcbRwList)
            //{
            //    var matchinbLedComponent = pcbUsedList.Where(pcb => pcb.Nc12 + pcb.Id == component.Nc12 + component.Id);
            //    if (matchinbLedComponent.Any()) continue;
            //    pcbUsedList.Add(new PcbUsedStruct { ConnectedComponentFromRw = component });
            //}
            RefreshDisplay();
        }
        public static void RefreshDisplay()
        {
            olvPcbUsed.SetObjects(pcbUsedList);
            if (olvPcbUsed.Items.Count > 0)
            {
                olvPcbUsed.RedrawItems(0, olvPcbUsed.Items.Count - 1, false);
            }
        }
        public static void AddNewPcb(Graffiti.MST.ComponentsTools.ComponentStruct compFromGraffiti)
        {
            string nc12 = compFromGraffiti.Nc12;
            string id = compFromGraffiti.Id;
            var matchingPcbs = pcbUsedList.Where(p => p.qrCode == compFromGraffiti.QrCode);
            
            if(matchingPcbs.Any())
            {
                MessageBox.Show("Ta płyta PCB została już dodana." + Environment.NewLine + $"12NC: {nc12}" + Environment.NewLine + $"ID: {id}");
                return;
            }
            if (compFromGraffiti == null) 
            {
                MessageBox.Show("Brak informacji o tym kodzie w bazie danych.");
                return;
            }
            if(compFromGraffiti.DocumentSymbol == "Rw")
            {
                //if(compFromGraffiti.ConnectedToOrder != CurrentOrder.CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00)
                //{
                //    var order = DataStorage.KittingData.KittingDict.Where(o=>o.Value.GraffitiOrderNo != null).Where(o => o.Value.GraffitiOrderNo.PrimaryKey_00 == compFromGraffiti.ConnectedToOrder);
                //    if (order.Any())
                //    {
                //        MessageBox.Show($"Ta płyta jest aktualnie przypisana do innego zlecenia." + Environment.NewLine
                //            + $"Numer zlecenia {order.First().Value.GraffitiOrderNo.PrimaryKey_46}" + Environment.NewLine
                //            + $" 12NC: {order.First().Value.modelId}");
                //        return;
                //    }
                //}
                MessageBox.Show($"Ta płyta jest już przypisana do zlecenia GraffitiID: {compFromGraffiti.ConnectedToOrder}." + Environment.NewLine
                    + $"Aktualne zlecenie GraffitiID: {CurrentOrder.CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00}");
                return;
            }
            int qty = (int)compFromGraffiti.Quantity;
            string location = compFromGraffiti.Location;
            //AddLedToListView(nc12, id, qty, binId);

            foreach (var pcb in pcbUsedList)
            {
                pcb.QtyNew = 0;
            }
            //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation(compFromGraffiti.QrCode, Graffiti.MST.ComponentsLocations.LineNumberToLocation( GlobalParameters.SmtLine));
            Graffiti.MST.ComponentsTools.UpdateDbData.BindComponentToOrderNumber(compFromGraffiti.QrCode, CurrentOrder.CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00);
            ComponentsOnRw.Refresh();
            olvPcbUsed.SetObjects(pcbUsedList);
        }
        public static async void MovePcbToTrash(string qrCode)
        {
            var items = pcbUsedList.Where(x => x.qrCode == qrCode);
            if (items.Count() == 0)
            {
                MessageBox.Show("Ta płyta PCB nie została wczytana.");
                return;
            }
            PcbUsedStruct thisPcb = items.First();
            thisPcb.QtyNew = 0;

            thisPcb.RefreshingDataRightNow = true;
            olvPcbUsed.Refresh();
            await Task.Run(() => ComponentsOnRw.TrashComponent(thisPcb.ConnectedComponentFromRw));
            thisPcb.RefreshingDataRightNow = false;
            olvPcbUsed.SetObjects(pcbUsedList);
        }
        internal static void ClearList()
        {
            ComponentsOnRw.ClearList();
            olvPcbUsed.Items.Clear();
            olvPcbUsed.SetObjects(pcbUsedList);
        }


    }
}
