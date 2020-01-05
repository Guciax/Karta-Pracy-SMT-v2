using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class LedDiodesForCurrentOrder
    {
        public static List<Graffiti.MST.ComponentsTools.ComponentStruct> LedDiodesList { get; set; }

        public static void ReloadList()
        {
            LedDiodesList = Graffiti.MST.OrdersOperations.GetData.GetComponnetsConnectedToOrder(CurrentMstOrder.currentOrder.KittingData.GraffitiOrderNo.PrimaryKey_00).Where(c=>c.ComponentIsLedDiode).ToList();
        }
    }
}
