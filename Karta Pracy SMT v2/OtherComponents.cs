using BrightIdeasSoftware;
using Karta_Pracy_SMT_v2.CurrentOrder;
using Karta_Pracy_SMT_v2.DataStorage;
using MST.MES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    public class OtherComponents
    {
        public static ObjectListView olvOtherComponents;

        public class OtherComponentsStruct
        {
            public  MST.MES.Data_structures.DevTools.DevToolsModelStructure DtModel { get; set; }
            public string Name
            {
                get
                {
                    if (componentMissing) return "BRAK!";
                    if (DtModel == null) return "Nieznany";
                    return DtModel.name;
                }
            }
            public string Nc12 { get; set; }
            public string Nc12_Formated
            {
                get { return Nc12.Insert(4, " ").Insert(8, " "); }
            }
            public string Id { get; set; }
            public string Qty { get; set; }
            public string Date { get; set; }
            public string ComponentGroup { get
                {
                    if (DtModel == null) return "";
                    if(DtModel.NcSeriesEnum == MST.MES.Data_structures.DevTools.DevToolsModelStructure.SeriesName.Resistor) return "Rezystor";
                    if(DtModel.NcSeriesEnum == MST.MES.Data_structures.DevTools.DevToolsModelStructure.SeriesName.Connector) return "Konektor";
                    //if (Nc12.StartsWith("4010450")) return "Rezystor";
                    //if (Nc12.StartsWith("4010411")) return "Konektor";
                    return "";
                } }

            public bool MatchesWithCurrentOrder { get; set; }
            public bool componentMissing = false;
            /// <summary>
            /// Jeżeli komponent został zmieniony w trakcie trwania zlecenia to dzieki temu rozliczymy odpad.
            /// </summary>
            public int QtyPreviouslyUsedReelInSameOrder = 0;
        }
        public static List<OtherComponentsStruct> otherComponentsList = new List<OtherComponentsStruct>();
        public static class CurrentOrderRequirements
        {
            private static string _CheckedForOrder;
            public static string CheckedForOrder 
            {
                get
                {
                    return _CheckedForOrder;
                }
                set
                {
                    previousReelQty = new Dictionary<string, int>();
                    _CheckedForOrder = value;
                } 
            }
            private static string[] _componentsList { get; set; }
            public static string[] componentsList 
            { 
                get
                {
                    if (CurrentMstOrder.currentOrder == null) return new string[] { };
                    if(CheckedForOrder == null)
                    {
                        _componentsList = DevTools.OtherComponentsForCurrentOrder.GetOtherComp12Nc();
                        CheckedForOrder = CurrentMstOrder.currentOrder.OrderNo;
                        return _componentsList;
                    }
                    if (CheckedForOrder != CurrentMstOrder.currentOrder.OrderNo)
                    {
                        _componentsList = DevTools.OtherComponentsForCurrentOrder.GetOtherComp12Nc();
                        CheckedForOrder = CurrentMstOrder.currentOrder.OrderNo;
                        return _componentsList;
                    }
                    return _componentsList;
                } 
            }
        }
        /// <summary>
        /// key = 12NCID
        /// </summary>
        public static Dictionary<string, int> previousReelQty { get; set; } = new Dictionary<string, int>();
        public static void UpdateList()
        {
            CheckComponentsAvailabilityForCurrentOder();
            olvOtherComponents.SetObjects(otherComponentsList);
            olvOtherComponents.Columns[1].Width = olvOtherComponents.Width - 120 - 60 - 60 -20;
            olvOtherComponents.Columns[2].Width = 120;
            olvOtherComponents.Columns[3].Width = 60;
            olvOtherComponents.Columns[4].Width = 80;
            //olvOtherComponents.Columns[5].Width = 90;
            //olvOtherComponents.AutoResizeColumn(1, System.Windows.Forms.ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        public static void AddNewComponent(string nc12, string id)
        {
            if (otherComponentsList.Where(x => x.Nc12 == nc12 & x.Id == id).Any()) 
            {
                MessageBox.Show("Ten komponent już jest na liście");
                return;
            }
            var existingComponents = otherComponentsList.Where(x => x.Nc12 == nc12 & !x.componentMissing);
            int existingCompQty = 0;
            if (existingComponents.Any()) 
            {
                string message = "Obecnie używany komponent musi zostać przesunięty do KOSZA aby dodać nowy."
                                 + Environment.NewLine
                                 + "Czy chcesz automatycznie przesunąć obecny komponent do kosza?"
                                 + Environment.NewLine
                                 + string.Join(Environment.NewLine, existingComponents.Select(x => $"{x.Nc12.Insert(4, " ").Insert(8, " ")} {x.Id}"));

                DialogResult dialogResult = MessageBox.Show(message, "UWAGA", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (var oldComp in existingComponents)
                    {
                        MoveComponentToTrash(oldComp.Nc12, oldComp.Id);
                        existingCompQty = int.Parse(oldComp.Qty);
                        if (previousReelQty.ContainsKey($"{oldComp.Nc12}{oldComp.Id}"))
                        {
                            previousReelQty.Add($"{oldComp.Nc12}{oldComp.Id}", existingCompQty);
                        }
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(nc12, id, GlobalParameters.SmtLine);
            //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{nc12}|ID:{id}", Graffiti.MST.ComponentsLocations.LineNumberToLocation(GlobalParameters.SmtLine));
            Graffiti.MST.ComponentsTools.UpdateDbData.SetStatus($"{nc12}|ID:{id}", GlobalParameters.SmtLine);
            GetOtherComponentsForSmtLineFromDb();
            UpdateList();
        }
        public static void MoveComponentToTrash(string nc12, string id)
        {
            if(otherComponentsList.Where(x=>x.Nc12==nc12 & x.Id == id).Count() == 0)
            {
                MessageBox.Show("Tego komponentu nie ma liście");
                return;
            }
            //MST.MES.SqlOperations.SparingLedInfo.UpdateLedQuantity(nc12, id, "0");
            //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentQty($"{nc12}|ID:{id}", 0);
            //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(nc12, id, "KOSZ");
            //Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{nc12}|ID:{id}", Graffiti.MST.ComponentsLocations.ComponentsTrash);
            Graffiti.MST.ComponentsTools.UpdateDbData.SetStatus($"{nc12}|ID:{id}", "KOSZ");
            GetOtherComponentsForSmtLineFromDb();
            UpdateList();
        }
        public static void MoveComponentToStorage(string nc12, string id)
        {
            //4010441#PCB#EL2.B-1/1
            //4010440#PCB#EL2.B-1/1
            //4010460#Dioda LED#Kitting
            //4010560#Dioda LED#Kitting
            //4010450#Rezystor#EL2.A-2/1
            //4010411#Konektor#EL2.A-2/1
            //4010434#Etykieta#EL2.A-1/1

            if (otherComponentsList.Where(x => x.Nc12 == nc12 & x.Id == id).Count() == 0)
            {
                MessageBox.Show("Tego komponentu nie ma liście");
                return;
            }

            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{nc12}|ID:{id}", Graffiti.MST.ComponentsLocations.GetComponentDefaultLocation(nc12));
            GetOtherComponentsForSmtLineFromDb();
            UpdateList();
        }
        public static async void GetOtherComponentsForSmtLineFromDb()
        {
            List<OtherComponentsStruct> result = new List<OtherComponentsStruct>();
            var componentsQrCodes = Graffiti.MST.ComponentsTools.GetDbData.GetQrCodesFromStatus(GlobalParameters.SmtLine);
            foreach (var comp in componentsQrCodes)
            {
                var splittedQr = comp.Lp100.Split(new string[] { "|ID:" }, StringSplitOptions.None);

                var dtModels = DevTools.DtDb.Where(x => x.nc12 == splittedQr[0]);
                MST.MES.Data_structures.DevTools.DevToolsModelStructure dtModel = null;
                if (dtModels.Any())
                {
                    dtModel = dtModels.First();
                    if (dtModel.NcSeriesEnum == MST.MES.Data_structures.DevTools.DevToolsModelStructure.SeriesName.LedDiode
                       || dtModel.NcSeriesEnum == MST.MES.Data_structures.DevTools.DevToolsModelStructure.SeriesName.Pcb)
                        continue;
                }
                result.Add(new OtherComponentsStruct
                {
                    Nc12 = splittedQr[0],
                    Id = splittedQr[1],
                    Date = comp.SetStatusDate.ToString("dd-MM-yyyy"),
                    Qty = "0",
                    QtyPreviouslyUsedReelInSameOrder = 0,
                    DtModel = dtModel
                });
            }
            //foreach (var component in ComponentsOnSmtLineLocation.thisLineOtherComponents)
            //{
            //    int prevReelQty = 0;
            //    if (previousReelQty.ContainsKey($"{component.Nc12}{component.Id}"))
            //    {
            //        prevReelQty = previousReelQty[$"{component.Nc12}{component.Id}"];
            //    }
            //    result.Add(new OtherComponentsStruct
            //    {
            //        Nc12 = component.Nc12,
            //        Id = component.Id,
            //        Date = component.operationDate.ToString(),
            //        Qty = component.Quantity.ToString(),
            //        QtyPreviouslyUsedReelInSameOrder = prevReelQty
            //    });
            //}
            otherComponentsList = result;
        }

        public static async void GetOtherComponentsForSmtLineFromDb_OLD()
        {
            List<OtherComponentsStruct> result = new List<OtherComponentsStruct>();
            await ComponentsOnSmtLineLocation.ReloadAync();

            foreach (var component in ComponentsOnSmtLineLocation.thisLineOtherComponents)
            {
                int prevReelQty = 0;
                if (previousReelQty.ContainsKey($"{component.Nc12}{component.Id}"))
                {
                    prevReelQty = previousReelQty[$"{component.Nc12}{component.Id}"];
                }
                otherComponentsList.Add(new OtherComponentsStruct
                {
                    Nc12 = component.Nc12,
                    Id = component.Id,
                    Date = component.operationDate.ToString(),
                    Qty = component.Quantity.ToString(),
                    QtyPreviouslyUsedReelInSameOrder = prevReelQty
                });
            }
            otherComponentsList = result;
        }

        public static void CheckComponentsAvailabilityForCurrentOder()
        {
            var notFoundList = CurrentOrderRequirements.componentsList.Except(otherComponentsList.Select(c => c.Nc12));

            foreach (var comp in otherComponentsList)
            {
                comp.MatchesWithCurrentOrder = CurrentOrderRequirements.componentsList.Contains(comp.Nc12);
            }

            foreach (var missingComp in notFoundList)
            {
                otherComponentsList.Add(new OtherComponentsStruct
                {
                    Nc12 = missingComp,
                    componentMissing = true,
                });
            }
        }
    }
}
