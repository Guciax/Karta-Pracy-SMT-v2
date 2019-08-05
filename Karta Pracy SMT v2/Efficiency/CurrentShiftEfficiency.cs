using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.Efficiency
{
    class CurrentShiftEfficiency
    {
        public static float CalculateCurrentShiftEfficiency()
        {
            float normTotalMinutes = 0;
            float realTotalMinutes = 0;

            var currentShift = MST.MES.DateTools.whatDayShiftIsit(DateTime.Now);
            foreach (var order in OrdersHistory.ordersHistory)
            {
                var orderShift = MST.MES.DateTools.GetOrderOwningShift(order.SmtData.smtStartDate, order.SmtData.smtEndDate);
                if (currentShift.fixedDate != orderShift.fixedDate) continue;
                
                var norm = MST.MES.EfficiencyCalculation.CalculateModelNormPerHour(order.modelInfo.DtModel00, order.SmtData.smtLine);

                normTotalMinutes+= (float)order.SmtData.manufacturedQty * 60 / (float)norm.outputPerHour;
                realTotalMinutes += (float)(order.EndTime - order.StartTime).TotalMinutes;
            }

            return (float)Math.Round(realTotalMinutes / normTotalMinutes, 1);
        }
    }
}
