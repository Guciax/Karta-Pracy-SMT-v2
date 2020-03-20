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
        public static void Show()
        {
            var currentShiftEff = CurrentShiftEfficiency.CalculateCurrentShiftEfficiency();
            var currentOrderEff = CurrentShiftEfficiency.CalculateCurrentOrderEfficiency();
            var currentOperatorEff = CurrentOperatorEfficiency.GetEff(CurrentOrder.CurrentMstOrder.userOperator.Name);

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
