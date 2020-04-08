using Karta_Pracy_SMT_v2.CurrentOrder;
using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.Forms;
using MST.MES.Data_structures.DevTools;
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
        public string StencilId { get { return SmtData.stencilId; } }
        public string OperatorName { get { return SmtData.operatorSmt; } }
        public int ManufacturedQty
        {
            get { return _ManufacturedQty; }
            set
            {
                _ManufacturedQty = value;
                LastUpdateTime = DateTime.Now;
                CurrentMstOrder.UpdateListViewOrderInfo();
                modelInfo.CheckMbQty();
            }
        }
        private int _ManufacturedQty;
        public int NgQty { get; set; }
        public MST.MES.DateTools.dateShiftNo ShiftInfo
        {
            get
            {
                return MST.MES.DateTools.GetOrderOwningShift(SmtData.smtStartDate, SmtData.smtEndDate);
            }
        }
        public int dbRecordIndex = -1;
        public DateTime LastUpdateTime = DateTime.Now;
        public Kitting KittingData { get; set; }
        public SmtRecords SmtData { get; set; }

        public ModelInfo modelInfo;
        public class ModelInfo
        {
            public DevToolsModelStructure DtModel00 { get; set; }
            public DevToolsModelStructure DtModel46 { get; set; }
            public float PcbPerMbCount
            {
                get
                {
                    float dtQty= MST.MES.DtTools.GetPcbPerMbCount(DtModel00);
                    if (dtQty > 0) return dtQty;
                    return manuallyEnteredPcbQty;
                }
            }
            private float manuallyEnteredPcbQty = 1;

            /// <summary>
            /// If DT data is not OK enter PCB qty manually.
            /// </summary>
            public void CheckMbQty()
            {
                float dtQty = MST.MES.DtTools.GetPcbPerMbCount(DtModel00);
                if (dtQty < 0)
                {
                    using(EnterPcbOnMbQtyManually enterQtyForm = new EnterPcbOnMbQtyManually())
                    {
                        if(enterQtyForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            manuallyEnteredPcbQty = enterQtyForm.currentQty;
                        }
                    }
                }
            }

            public float LedCount { get
                {
                    return MST.MES.DtTools.GetLedCount(DtModel00);
                } }
        }
    }
}
