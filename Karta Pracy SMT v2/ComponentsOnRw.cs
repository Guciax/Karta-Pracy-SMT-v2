using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Graffiti.MST.ComponentsTools;
using static Karta_Pracy_SMT_v2.LedsUsed;

namespace Karta_Pracy_SMT_v2
{
    public class ComponentsOnRw
    {
        private static List<ComponentStruct> ComponentsOnRwCollection { get; set; }
        public static void ClearList()
        {
            ComponentsOnRwCollection = null;
        }

        public static IEnumerable<ComponentStruct> LedCollection
        {
            get
            {
                if (ComponentsOnRwCollection == null) Refresh();
                //var ledsCol = ComponentsOnRwCollection.Where(c => c.ComponentIsLedDiode);
                //var wl = ledsCol.ToList();
                return ComponentsOnRwCollection.Where(c => c.ComponentIsLedDiode).Where(l => l.DocumentSymbol == "Rw");
            }
        }
        public static IEnumerable<ComponentStruct> PcbCollection
        {
            get
            {
                if(CurrentOrder.CurrentMstOrder.currentOrder == null) return new List<ComponentStruct>();
                if (ComponentsOnRwCollection == null) Refresh();
                var pcbManual = ComponentsOnRwCollection.Where(c => c.Nc12.StartsWith("4060441")).ToList();
                return ComponentsOnRwCollection.Where(c => c.componentType == ComponentType.PCB);
            }
        }
        public static IEnumerable<ComponentStruct> OtherComponentsCollection
        {
            get
            {
                if (ComponentsOnRwCollection == null) Refresh();
                return ComponentsOnRwCollection.Where(c => c.componentType == ComponentType.Connector || c.componentType == ComponentType.Resistor);
            }
        }

        public static void Refresh()
        {
            if (CurrentOrder.CurrentMstOrder.currentOrder == null) return;
            ComponentsOnRwCollection = Graffiti.MST.OrdersOperations.GetData.GetComponnetsConnectedToOrder(CurrentOrder.CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00)
                .OrderBy(c=>c.Id)
                .ToList();
            Debug.WriteLine("RW:"+string.Join(",", ComponentsOnRwCollection.Select(c => c.Nc12).Distinct().ToArray()));
            LedsUsed.SyncListWithRwList(ComponentsOnRw.LedCollection.ToList());
            PcbUsedInOrder.SyncListWithPcbRwList(ComponentsOnRw.PcbCollection.ToList());
        }

        public static async Task TrashComponent(ComponentStruct connectedComponentOnRw)
        {
            connectedComponentOnRw.StatusTrash = "KOSZ";
            string[,] arr = new string[1, 2];
            arr[0, 0] = "333";
            arr[0, 1] = "KOSZ";
            //throw new NotImplementedException();

            //Graffiti.MST.ComponentsTools.UpdateDbData.SetRolkaTablica(qrCode, arr);
        }
    }
}
