using Karta_Pracy_SMT_v2.CurrentOrder;
using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            float numberOfOrders = 0;

            if(CurrentMstOrder.currentOrder != null)
            {
                var currentOrderNorm = MST.MES.EfficiencyCalculation.CalculateModelNormPerHour(CurrentMstOrder.currentOrder.modelInfo.DtModel00,
                                                                                               CurrentMstOrder.currentOrder.SmtData.smtLine);

                normTotalMinutes = (float)CurrentMstOrder.currentOrder.ManufacturedQty * 60 / (float)currentOrderNorm.outputPerHour;
                realTotalMinutes = (float)(CurrentMstOrder.currentOrder.LastUpdateTime - CurrentMstOrder.currentOrder.SmtData.smtStartDate).TotalMinutes;
            }

            var currentShift = MST.MES.DateTools.whatDayShiftIsit(DateTime.Now);
            foreach (var order in OrdersHistory.ordersHistory)
            {
                if (order.SmtData.changeOver) continue;
                DateTime endDate = order.SmtData.smtEndDate;

                if(endDate == DateTime.MinValue)
                {
                    endDate = order.LastUpdateTime;
                }

                var orderShift = MST.MES.DateTools.GetOrderOwningShift(order.SmtData.smtStartDate, endDate);
                if (currentShift.fixedDate != orderShift.fixedDate) continue;

                numberOfOrders++;
                var norm = MST.MES.EfficiencyCalculation.CalculateModelNormPerHour(order.modelInfo.DtModel00, order.SmtData.smtLine);

                //var eff = MST.MES.EfficiencyCalculation.CalculateEfficiency(order.SmtData.smtStartDate,
                //                                                            order.SmtData.smtEndDate,
                //                                                            order.modelInfo.DtModel00,
                //                                                            order.SmtData.manufacturedQty,
                //                                                            order.SmtData.smtLine,
                //                                                            false);


                normTotalMinutes += (float)order.SmtData.manufacturedQty * 60 / (float)norm.outputPerHour;
                var realMinutesThisOrder = (float)(endDate - order.SmtData.smtStartDate).TotalMinutes;
                if (realMinutesThisOrder > 0) realTotalMinutes += realMinutesThisOrder;
            }
            return (float)Math.Round((normTotalMinutes + (numberOfOrders) * 0) / realTotalMinutes, 3);
        }

        public static double CalculateCurrentOrderEfficiency()
        {
            if (CurrentMstOrder.currentOrder == null) return 0;
            return MST.MES.EfficiencyCalculation.CalculateEfficiency(CurrentMstOrder.currentOrder.SmtData.smtStartDate,
                                                                     CurrentMstOrder.currentOrder.LastUpdateTime,
                                                                     CurrentMstOrder.currentOrder.modelInfo.DtModel00,
                                                                     CurrentMstOrder.currentOrder.ManufacturedQty,
                                                                     GlobalParameters.SmtLine,
                                                                     false);
        }

        public static double CalculateCurrentShiftAvgChangeOverTime()
        {

            var currentShift = MST.MES.DateTools.whatDayShiftIsit(DateTime.Now);
            var changeOvers = OrdersHistory.ordersHistory.Where(o => o.SmtData.changeOver)
                                                         .Where(o => MST.MES.DateTools.GetOrderOwningShift(o.SmtData.smtStartDate, o.SmtData.smtEndDate).fixedDate == currentShift.fixedDate)
                                                         .ToList();


            return changeOvers.Select(o => (o.SmtData.smtEndDate - o.SmtData.smtStartDate).TotalMinutes).Sum() / changeOvers.Count();
        }

        public static void SpitOutEff()
        {
            Debug.WriteLine("12NC;P&P;SMT2 Reflow;SMT3 P&P;SMT3 Reflow");
            foreach (var ledModel in DevTools.DtDb)
            {
                if (ledModel.NcSeriesEnum != MST.MES.Data_structures.DevTools.DevToolsModelStructure.SeriesName.ModulyLED) continue;
                if (!ledModel.nc12.EndsWith("00")) continue;
                var norm2 = MST.MES.EfficiencyCalculation.CalculateEffAsmOptAlgorith(ledModel, "SMT2");
                var norm3 = MST.MES.EfficiencyCalculation.CalculateEffAsmOptAlgorith(ledModel, "SMT3");

                
                Debug.WriteLine($"{ledModel.nc12_formated};{norm2.siplaceCT};{norm2.reflowCT};{norm3.siplaceCT};{norm3.reflowCT}");
            }
        }
    }
}
