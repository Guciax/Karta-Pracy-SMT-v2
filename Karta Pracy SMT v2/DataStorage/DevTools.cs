using MST.MES.Data_structures;
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

        public static DevToolsModelStructure DtCurrentModelModel00()
        {
            if (CurrentMstOrder.currentOrder == null) return null;
            var models = _DtDb.Where(x => x.nc12 == $"{CurrentMstOrder.currentOrder.Model10Nc}00");
            if (models.Count() > 0) return models.First();
            else return null;
        }

        public class OtherComponentsForCurrentOrder
        {
            public static string[] Resistors12NcList()
            {
                if (CurrentMstOrder.currentOrder == null) return new string[0];
                return MST.MES.DtTools.GetRes12Nc(DtCurrentModelModel00());
            }

            public static string[] Connectors12NcList()
            {
                if (CurrentMstOrder.currentOrder == null) return new string[0];
                return MST.MES.DtTools.GetConn12Nc(DtCurrentModelModel00());
            }

            public static string[] GetOtherComp12Nc()
            {
                //4010434 - label
                //4010441 - MB
                //4010440 - PCB
                //4010460 - LED
                //4010560 - LED

                if (CurrentMstOrder.currentOrder == null) return new string[0];
                return MST.MES.DtTools.GetOtherComp12Nc(DtCurrentModelModel00());
            }
        }

        public static float PcbPerMbOverwritenByUser = 0;
        public static float CurrentModelPcbPerMb
        {
            get
            {
                if (PcbPerMbOverwritenByUser > 0) return PcbPerMbOverwritenByUser;
                return MST.MES.DtTools.GetPcbPerMbCount(CurrentMstOrder.currentOrder.modelInfo.DtModel00);
            }
        }
    }
}
