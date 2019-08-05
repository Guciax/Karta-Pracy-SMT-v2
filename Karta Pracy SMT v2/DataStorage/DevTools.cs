using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.DataStorage
{
    public class DevTools
    {
        private static List<MST.MES.Data_structures.DevToolsModelStructure> _DtDb = new List<MST.MES.Data_structures.DevToolsModelStructure>();
        public static List<MST.MES.Data_structures.DevToolsModelStructure> DtDb
        { get
            {
                return _DtDb;
            }
            set
            {
                _DtDb = value;
                lastUpdateTime = DateTime.Now;
                lastUpdateSuccesfull = true;
            }
        }
        public static DateTime lastUpdateTime = DateTime.MinValue;
        public static bool lastUpdateSuccesfull = false;
    }
}
