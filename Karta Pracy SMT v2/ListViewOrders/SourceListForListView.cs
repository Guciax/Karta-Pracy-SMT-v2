using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.ListViewOrders
{
    public class SourceListForListView
    {
        public static BrightIdeasSoftware.ObjectListView olv;
        public static List<DataModel> ListOfMstOrders
        {
            get
            {
                return DataStorage.OrdersHistory.ordersHistory
                    .Select(o => new DataModel { ConnectedMstOrder = o })
                    .OrderBy(o=>o.StartDate)
                    .ToList();
            }
        }

        public static void RefreshListView()
        {
            olv.SetObjects(ListOfMstOrders);
            //olv.Sort(olv.GetColumn(8), System.Windows.Forms.SortOrder.Descending);
            olv.GetColumn(0).Width = 1;
            olv.GetColumn(1).Width = 75;
            olv.GetColumn(2).Width = 130;
            olv.GetColumn(3).Width = (olv.Width - 620);
            olv.GetColumn(4).Width = 60;
            olv.GetColumn(5).Width = 110;
            olv.GetColumn(6).Width = 60;
            olv.GetColumn(7).Width = 50;
            olv.GetColumn(8).Width = 50;
        }

        public static void SetUpGrouping()
        {
            
        }
    }
}
