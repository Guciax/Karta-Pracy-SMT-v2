using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.CurrentOrder
{
    public class BomForCurrentOrderFromGraffiti
    {
        public static List<Graffiti.MST.ComponentsTools.ComponentStruct> bomList { get; set; }

        public static void Reload()
        {
            bomList = Graffiti.MST.OrdersOperations.GetData.GetCurrentOrderBom(CurrentOrder.CurrentMstOrder.currentOrder.KittingData.GraffitiOrders.PrimaryKey46).ToList();
        }

        public static List<Graffiti.MST.ComponentsTools.ComponentStruct> GetLedDiode
        {
            get { return bomList.Where(x => x.componentType == Graffiti.MST.ComponentsTools.ComponentType.LedDiode).ToList(); }
        }
        public static List<Graffiti.MST.ComponentsTools.ComponentStruct> GetPcb
        {
            get { return bomList.Where(x => x.componentType == Graffiti.MST.ComponentsTools.ComponentType.PCB).ToList(); }
        }
        public static List<Graffiti.MST.ComponentsTools.ComponentStruct> GetOtherComponents
        {
            get { return bomList.Where(x => 
            x.componentType == Graffiti.MST.ComponentsTools.ComponentType.Resistor
            || x.componentType == Graffiti.MST.ComponentsTools.ComponentType.Connector
            ).ToList(); }
        }
    }
}
