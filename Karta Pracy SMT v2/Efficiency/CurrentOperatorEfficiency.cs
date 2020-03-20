using Karta_Pracy_SMT_v2.CurrentOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.Efficiency
{
    public class CurrentOperatorEfficiency
    {
        public static double GetEff(string operatorName)
        {
            float normTotalMinutes = 0;
            float realTotalMinutes = 0;
            float numberOfOrders = 0;

            if (CurrentMstOrder.currentOrder != null)
            {
                var currentOrderNorm = MST.MES.EfficiencyCalculation.CalculateModelNormPerHour(CurrentMstOrder.currentOrder.modelInfo.DtModel00,
                                                                                               CurrentMstOrder.currentOrder.SmtData.smtLine);

                normTotalMinutes = (float)CurrentMstOrder.currentOrder.ManufacturedQty * 60 / (float)currentOrderNorm.outputPerHour;
                realTotalMinutes = (float)(CurrentMstOrder.currentOrder.LastUpdateTime - CurrentMstOrder.currentOrder.SmtData.smtStartDate).TotalMinutes;
            }
            var operatorsTodayOrders = DataStorage.MesData.SmtData
                .SelectMany(o => o.Value.smtOrders)
                .Where(o => o.operatorSmt == operatorName)
                .Where(o => o.smtStartDate.Date == DateTime.Now.Date);

            foreach (var order in operatorsTodayOrders)
            {
                var orderShift = MST.MES.DateTools.GetOrderOwningShift(order.smtStartDate, order.smtEndDate);

                numberOfOrders++;
                var dtModel = MST.MES.DtTools.GetDtModel00(order.orderInfo.modelId, DataStorage.DevTools.DtDb);
                var norm = MST.MES.EfficiencyCalculation.CalculateModelNormPerHour(dtModel, order.smtLine);

                normTotalMinutes += (float)order.manufacturedQty * 60 / (float)norm.outputPerHour;
                var realMinutesThisOrder = (float)(order.smtEndDate - order.smtStartDate).TotalMinutes;
                if (realMinutesThisOrder > 0) realTotalMinutes += realMinutesThisOrder;
            }

            return (double)Math.Round((normTotalMinutes + (numberOfOrders) * 0) / realTotalMinutes, 3);
        }
    }
}
