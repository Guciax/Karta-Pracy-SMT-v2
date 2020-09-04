using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.DataStorage
{
    public class MesData
    {
        public static Dictionary<string, MST.MES.OrderStructureByOrderNo.Kitting> KittingData { get; set; }
        public static Dictionary<string, MST.MES.OrderStructureByOrderNo.SMT> SmtData { get; set; }

        public static async Task AsyncLoader()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => GetKitting()));
            tasks.Add(Task.Run(() => GetDevTools()));
            tasks.Add(Task.Run(() => GetSmt()));
            tasks.Add(Task.Run(() => LedCollectiveDb.LoadDb()));
            //tasks.Add(Task.Run(() => OtherComponents.GetOtherComponentsForSmtLineFromDb()));
            await Task.WhenAll(tasks);
            OtherComponents.GetOtherComponentsForSmtLineFromDb();
        }

        private static void GetKitting()
        {
            KittingData = MST.MES.SqlDataReaderMethods.Kitting.GetKittingDataForClientGroup(MST.MES.SqlDataReaderMethods.Kitting.clientGroup.MST, 100);

        }

        private static void GetDevTools()
        {
            DevTools.ReloadDb();
        }

        private static void GetSmt()
        {
            SmtData = MST.MES.SqlDataReaderMethods.SMT.GetOrdersByDataReader(100);
        }
    }
}
