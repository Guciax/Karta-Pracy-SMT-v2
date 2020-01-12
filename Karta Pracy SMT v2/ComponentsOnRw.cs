using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Graffiti.MST.ComponentsTools;

namespace Karta_Pracy_SMT_v2
{
    public class ComponentsOnRw
    {
        private static List<ComponentStruct> ComponentsOnRwCollection { get; set; }
        public static void ClearList()
        {
            ComponentsOnRwCollection.Clear();
        }
        public static List<ComponentStruct> LedCollection
        {
            get
            {
                if (ComponentsOnRwCollection == null) Refresh();
                return ComponentsOnRwCollection.Where(c => c.ComponentIsLedDiode).Where(c => c.DocumentSymbol == "Rw").ToList();
            }
        }
        public static List<ComponentStruct> PcbCollection
        {
            get
            {
                if (ComponentsOnRwCollection == null) Refresh();
                return ComponentsOnRwCollection.Where(c => c.componentType == ComponentType.PCB).ToList();
            }
        }
        public static List<ComponentStruct> OtherComponentsCollection
        {
            get
            {
                if (ComponentsOnRwCollection == null) Refresh();
                return ComponentsOnRwCollection.Where(c => c.componentType == ComponentType.Connector || c.componentType == ComponentType.Resistor).ToList();
            }
        }

        public static void Refresh()
        {
            ComponentsOnRwCollection = Graffiti.MST.OrdersOperations.GetData.GetCompOnRwWithAttributes().ToList();
        }

        public static void TrashComponent(string qrCode)
        {
            var matchingComponents = ComponentsOnRwCollection.Where(c => c.QrCode == qrCode);
            if (!matchingComponents.Any())
            {
                MessageBox.Show("Brak komponentu na liście używanych");
                return;
            }
            ComponentStruct componentToTrash = matchingComponents().First();
            componentToTrash.attributesCechy.InTrash = true;
            Graffiti.MST.ComponentsTools.UpdateDbData.Trash(qrCode);
        }
    }
}
