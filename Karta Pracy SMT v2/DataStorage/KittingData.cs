using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.DataStorage
{
    public class KittingData
    {
        public static Dictionary<string, MST.MES.OrderStructureByOrderNo.Kitting> KittingDict { get; set; }
        public static async void ReloadAsync()
        {
            await GetData().ConfigureAwait(false);
        }

        private static async Task GetData()
        {
            KittingDict = MST.MES.SqlDataReaderMethods.Kitting.GetKittingDataForClientAndProductGroup(MST.MES.SqlDataReaderMethods.Kitting.clientGroup.MST, 90, new MST.MES.SqlDataReaderMethods.ProductGroup[] { MST.MES.SqlDataReaderMethods.ProductGroup.LED });
        }
    }
}
