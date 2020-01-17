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
        public static List<PcbUsedStruct> pcbUsedList { get { return ComponentsOnRw.PcbCollection.Select(pcb => new PcbUsedStruct { ConnectedComponentFromRw = pcb }).ToList(); } }

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
                get { return ConnectedComponentFromRw.attributesCechy.InTrashBool; }
            }
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
                if(compFromGraffiti.ConnectedToOrder != DataStorage.CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00)
                {
                    var order = DataStorage.KittingData.KittingDict.Where(o => o.Value.GraffitiOrderNo.PrimaryKey_00 == compFromGraffiti.ConnectedToOrder);
                    if (order.Any())
                    {
                        MessageBox.Show($"Ta płyta jest aktualnie przypisana do innego zlecenia." + Environment.NewLine
                            + $"Numer zlecenia {order.First().Value.GraffitiOrderNo.PrimaryKey_46}" + Environment.NewLine
                            + $" 12NC: {order.First().Value.modelId}");
                        return;
                    }
                }
                MessageBox.Show($"Ta płyta jest już przypisana do zlecenia {compFromGraffiti.ConnectedToOrder}.");
                return;
            }

            int qty = (int)compFromGraffiti.Quantity;
            string location = compFromGraffiti.Location;
            //AddLedToListView(nc12, id, qty, binId);

            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation(compFromGraffiti.QrCode, Graffiti.MST.ComponentsLocations.LineNumberToLocation( GlobalParameters.SmtLine));
            Graffiti.MST.ComponentsTools.UpdateDbData.BindComponentToOrderNumber(compFromGraffiti.QrCode, CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00);
            ComponentsOnRw.Refresh();
            olvPcbUsed.SetObjects(pcbUsedList);
        }


        public static void MovePcbToTrash(string qrCode)
        {
            var items = pcbUsedList.Where(x => x.qrCode == qrCode);
            if (items.Count() == 0)
            {
                MessageBox.Show("Ta płyta PCB nie została wczytana.");
                return;
            }
            PcbUsedStruct thisPcb = items.First();
            thisPcb.QtyNew = 0;

            ComponentsOnRw.TrashComponent(qrCode);
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
