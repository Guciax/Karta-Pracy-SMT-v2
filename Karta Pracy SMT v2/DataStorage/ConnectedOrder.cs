using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class ConnectedToCurrentOrder
    {
        private static MstOrder _ConnectedOrder;

        public static MstOrder ConnectedOrder
        {
            get
            {
                return _ConnectedOrder;
            }
            set
            {
                _ConnectedOrder = value;
            }
        }

        

        public static void SetUpConnectedOrder()
        {
            ConnectedOrder = null;

            if (CurrentMstOrder.currentOrder == null || CurrentMstOrder.currentOrder.KittingData.connectedOrder == "")
            {
                return;
            }
            ConnectedOrder = new MstOrder();
            ConnectedOrder.KittingData = MesData.KittingData.Where(o => o.Key == CurrentMstOrder.currentOrder.KittingData.connectedOrder)
                                                            .Select(o => o.Value)
                                                            .First();

            ConnectedOrder.SmtData = new MST.MES.OrderStructureByOrderNo.SmtRecords
            {
                smtStartDate = DateTime.Now,
                operatorSmt = CurrentMstOrder.currentOrder.SmtData.operatorSmt,
                smtEndDate = DateTime.MinValue,
                smtLine = GlobalParameters.SmtLine,
                stencilId = CurrentMstOrder.currentOrder.SmtData.stencilId
            };

            var dtModel00 = MST.MES.DtTools.GetDtModel00(CurrentMstOrder.currentOrder.KittingData.modelId, DevTools.DtDb);
            var dtModel46 = MST.MES.DtTools.GetDtModel46(CurrentMstOrder.currentOrder.KittingData.modelId, DevTools.DtDb);
            ConnectedOrder.modelInfo = new MstOrder.ModelInfo
            {
                DtModel00 = dtModel00,
                DtModel46 = dtModel46
            };

        }

        public static int GetPcbPerMb
        {
            get
            {
                if (ConnectedOrder == null) return 0;
                return CurrentMstOrder.currentOrder.KittingData.connectedOrderPcbOnMb;
            }
        }

        public static void UpdateConnectedOrderQty()
        {
            if (ConnectedOrder != null)
            {
                var baseModelPcbPerMb = (int)MST.MES.DtTools.GetPcbPerMbCount(CurrentMstOrder.currentOrder.modelInfo.DtModel00);
                var baseModelMbQty = CurrentMstOrder.currentOrder.ManufacturedQty / baseModelPcbPerMb;
                ConnectedOrder.ManufacturedQty = CurrentMstOrder.currentOrder.KittingData.connectedOrderPcbOnMb * baseModelMbQty;
            }
        }
    }
}
