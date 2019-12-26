﻿using BrightIdeasSoftware;
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
            public string Name
            {
                get
                {
                    if (componentMissing) return "BRAK!";
                    if (LedCollectiveDb.nc12ToCollective.ContainsKey(Nc12)) return LedCollectiveDb.nc12ToCollective[Nc12].name;
                    return "";
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
                    if (Nc12.StartsWith("4010450")) return "Rezystor";
                    if (Nc12.StartsWith("4010411")) return "Konektor";
                    return "";
                } }

            public bool MatchesWithCurrentOrder { get; set; }
            public bool componentMissing = false;
        }

        public static List<OtherComponentsStruct> otherComponentsList = new List<OtherComponentsStruct>();

        public static void UpdateList()
        {
            CheckComponentsAvailabilityForCurrentOder();
            olvOtherComponents.SetObjects(otherComponentsList);
            olvOtherComponents.Columns[2].Width = 120;
            olvOtherComponents.Columns[3].Width = 60;
            olvOtherComponents.Columns[4].Width = 60;
            olvOtherComponents.Columns[5].Width = 90;
            //olvOtherComponents.AutoResizeColumn(1, System.Windows.Forms.ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public static void AddNewComponent(string nc12, string id)
        {
            if(otherComponentsList.Where(x=>x.Nc12 == nc12 & x.Id == id).Count() > 0)
            {
                MessageBox.Show("Ten komponent już jest na liście");
                return;
            }

            var existingComponents = otherComponentsList.Where(x => x.Nc12 == nc12 & !x.componentMissing);
            if (existingComponents.Count() > 0) 
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
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(nc12, id, GlobalParameters.SmtLine);
            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{nc12}|ID:{id}", GlobalParameters.SmtLine);
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
            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentQty($"{nc12}|ID:{id}", 0);
            //MST.MES.SqlOperations.SparingLedInfo.UpdateLedLocation(nc12, id, "KOSZ");
            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{nc12}|ID:{id}", "KOSZ");
            
            UpdateList();
            olvOtherComponents.SetObjects(otherComponentsList);
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

            Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentLocation($"{nc12}|ID:{id}", Graffiti.MST.ComponentsTools.GetComponentDefaultLocation(nc12));
            UpdateList();
            olvOtherComponents.SetObjects(otherComponentsList);
        }

        public static void GetOtherComponentsForSmtLineFromDb()
        {
            List<OtherComponentsStruct> result = new List<OtherComponentsStruct>();
            var locations = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsInLocations("EL:"+GlobalParameters.SmtLine);
            var qrList = locations.SelectMany(x => x.Value).ToList();
            var allComponents = Graffiti.MST.ComponentsTools.GetDbData.GetComponentDataWithAttributes(qrList);
            var removedLedAndPcb = allComponents.Where(c => !c.ComponentIsLedDiode)
                                                .Where(c => !c.Nc12.StartsWith("4010440"))
                                                .Where(c => !c.Nc12.StartsWith("4010441"));
            foreach (var component in removedLedAndPcb)
            {
                otherComponentsList.Add(new OtherComponentsStruct
                {
                    Nc12 = component.Nc12,
                    Id = component.Id,
                    Date = component.operationDate.ToString(),
                    Qty = component.Quantity.ToString()
                });
            }
            otherComponentsList = result;
        }

        public static void CheckComponentsAvailabilityForCurrentOder()
        {
            var requiredOtherComps = DevTools.OtherComponentsForCurrentOrder.GetOtherComp12Nc();
            var notFoundList = requiredOtherComps.Except(otherComponentsList.Select(c => c.Nc12));

            foreach (var comp in otherComponentsList)
            {
                comp.MatchesWithCurrentOrder = requiredOtherComps.Contains(comp.Nc12);
                
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
