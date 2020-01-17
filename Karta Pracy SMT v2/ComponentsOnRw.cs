using System;
using System.Collections.Generic;
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
        private static IEnumerable<ComponentStruct> ComponentsOnRwCollection { get; set; }
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
                if (ComponentsOnRwCollection == null) Refresh();
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
            ComponentsOnRwCollection = Graffiti.MST.OrdersOperations.GetData.GetComponnetsConnectedToOrder(DataStorage.CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00).ToList();
            LedsUsed.ledsUsedList = ComponentsOnRw.LedCollection.Select(x => new LedsUsedStruct { ConnectedComponentFromRwList = x }).ToList();
            LedsUsed.RefreshLedUsedOlv();
        }

        public static void TrashComponent(string qrCode)
        {
            var matchingComponents = ComponentsOnRwCollection.Where(c => c.QrCode == qrCode);
            if (!matchingComponents.Any())
            {
                MessageBox.Show("Brak komponentu na liście używanych");
                return;
            }
            ComponentStruct componentToTrash = matchingComponents.First();
            componentToTrash.attributesCechy.InTrashString = "KOSZ";
            //string[,] arr = new string[1, 2];
            //arr[0, 0] = "kosz";
            //arr[0, 1] = "value";
            //throw new NotImplementedException();
            //Graffiti.MST.ComponentsTools.UpdateDbData.SetRolkaTablica(qrCode, arr);
        }
    }
}
