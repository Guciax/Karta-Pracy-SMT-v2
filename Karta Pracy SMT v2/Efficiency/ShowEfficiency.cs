using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2.Efficiency
{
    public class ShowEfficiency
    {
        public static Label lCurrentOrderEff;
        public static Label lShiftEff;
        public static Label lOperatorEff;
        public static double CurrentShiftEffValue { get { return CurrentShiftEfficiency.CalculateCurrentShiftEfficiency(); } }
        public static double CurrentOrderEffValue { get {return  CurrentShiftEfficiency.CalculateCurrentOrderEfficiency(); } }
        public static double CurrentOperatorEffValue { get { return CurrentShiftEfficiency.CalculateCurrentOrderEfficiency(); } }
        public static double CurrentOutputPerHour { get { return CalculateOutputPerHour(); } }
        public static double CurrentAverageCycleTime{ get { return CalculateAverageCyceTime(); } }

        private static double CalculateAverageCyceTime()
        {
            
            var orderDuration = (CurrentOrder.CurrentMstOrder.currentOrder.LastUpdateTime - CurrentOrder.CurrentMstOrder.currentOrder.SmtData.smtStartDate).TotalSeconds;
            var prodMb = CurrentOrder.CurrentMstOrder.currentOrder.SmtData.manufacturedQty / DevTools.CurrentModelPcbPerMb;
            if (orderDuration * prodMb == 0) return 0;
            return Math.Round(orderDuration / prodMb, 0);
        }
        private static double CalculateOutputPerHour()
        {
            var orderDuration = (CurrentOrder.CurrentMstOrder.currentOrder.LastUpdateTime - CurrentOrder.CurrentMstOrder.currentOrder.SmtData.smtStartDate).TotalHours;
            var prod = CurrentOrder.CurrentMstOrder.currentOrder.SmtData.manufacturedQty;
            if (prod * orderDuration == 0) return 0;
            return Math.Round(prod / orderDuration * 100, 1);
        }
        public static void RefreshLabels()
        {
            var currentShiftEff = CurrentShiftEffValue;
            var currentOrderEff = CurrentOrderEffValue;
            var currentOperatorEff = CurrentOperatorEffValue;

            string shiftEffString = "-";
            string orderEffString = "-";
            string operatorEffString = "-";

            if (currentShiftEff > 0) shiftEffString = Math.Round(currentShiftEff * 100, 0) + "%";
            if (currentOrderEff > 0) orderEffString = Math.Round(currentOrderEff * 100, 0) + "%";
            if (currentOperatorEff > 0) operatorEffString = Math.Round(currentOperatorEff * 100, 0) + "%";

            lCurrentOrderEff.Text = orderEffString;
            lShiftEff.Text = shiftEffString ;
            lOperatorEff.Text = operatorEffString;
        }
    }
}
