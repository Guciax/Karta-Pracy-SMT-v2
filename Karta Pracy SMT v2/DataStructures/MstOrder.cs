using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MST.MES.OrderStructureByOrderNo;

namespace Karta_Pracy_SMT_v2.DataStructures
{
    public class MstOrder
    {
        public string OrderNo
        {
            get
            {
                if (KittingData != null)
                    return KittingData.orderNo;
                return null;
            }
        }
        public string Model10Nc
        {
            get
            {
                if (KittingData != null)
                    return KittingData.modelId;
                return null;
            }
        }
        public string Model10NcFormated
        {
            get { if (Model10Nc.Length == 10)
                {
                    return Model10Nc.Insert(4, " ").Insert(8, " ");
                }
                return Model10Nc;
            }
        }
        public string ModelName
        {
            get
            {
                if (KittingData != null)
                    return KittingData.ModelName;
                return null;
            }
        }
        public string StencilId { get; set; }
        public string OperatorName { get; set; }
        public int ManufacturedQty
        {
            get { return _ManufacturedQty; }
            set
            {
                _ManufacturedQty = value;
                LastUpdateTime = DateTime.Now;
                CurrentMstOrder.UpdateListViewOrderInfo();
            }
        }
        private int _ManufacturedQty;
        public int NgQty { get; set; }

        public int dbRecordIndex = -1;
        public DateTime StartTime = DateTime.Now;
        public DateTime LastUpdateTime = DateTime.Now;
        public DateTime EndTime { get; set; }
        public Kitting KittingData { get; set; }
        public SmtRecords SmtData { get; set; }

        public ModelInfo modelInfo;
        public class ModelInfo
        {
            public MST.MES.Data_structures.DevToolsModelStructure DtModel00 { get; set; }
            public MST.MES.Data_structures.DevToolsModelStructure DtModel46 { get; set; }
            public float PcbPerMbCount
            {
                get { return MST.MES.DtTools.GetPcbPerMbCount(DtModel00); }
            }

            public float LedCount { get
                {
                    return MST.MES.DtTools.GetLedCount(DtModel00);
                } }
        }
    }
}
