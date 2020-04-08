using Karta_Pracy_SMT_v2.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2.ListViewOrders
{
    public class DataModel
    {

        public MstOrder ConnectedMstOrder { get; set; }
        public string OrderNo { get
            {
                return ConnectedMstOrder.OrderNo;
            } }

        public string Model10Nc
        {
            get
            {
                return ConnectedMstOrder.Model10NcFormated;
            }
        }

        public string ModelName
        {
            get
            {
                if (ConnectedMstOrder.SmtData.changeOver) return "Przestawienie";
                return ConnectedMstOrder.modelInfo.DtModel46.name;
            }
        }
        public int ManufacturedQty
        {
            get
            {
                if (ConnectedMstOrder.SmtData.changeOver) return 0;
                if (ConnectedMstOrder.ManufacturedQty > 0)
                    return ConnectedMstOrder.ManufacturedQty;
                else return ConnectedMstOrder.SmtData.manufacturedQty;
            }
        }
        public MST.MES.DateTools.dateShiftNo ShiftInfo
        {
            get
            {

                return MST.MES.DateTools.GetOrderOwningShift(StartDate, EndDate);
            }
        }
        public string ShiftInfoString
        {
            get
            {
                return $"{ShiftInfo.fixedDate:dd-MM} zm.{ShiftInfo.shift}";
            }
        }
        public DateTime StartDate
        {
            get
            {
                return ConnectedMstOrder.SmtData.smtStartDate;
            }
        }
        public string StartDateString
        {
            get
            {
                return StartDate.ToString("dd-MM HH:mm");
            }
        }
        public DateTime EndDate
        {
            get
            {
                if (ConnectedMstOrder.SmtData.smtEndDate.Year < 2000) return DateTime.Now;
                return ConnectedMstOrder.SmtData.smtEndDate;
            }
        }
        public string EndDateString
        {
            get
            {
                return EndDate.ToString("HH:mm");
            }
        }
        public int Duration
        {
            get
            {
                return (int)Math.Round((EndDate - StartDate).TotalMinutes, 0);
            }
        }
        public string Efficiency
        {
            get
            {
                string result = "-";
                if (ConnectedMstOrder.SmtData.changeOver) return result;
                var dtModel00 = MST.MES.DtTools.GetDtModel00(Model10Nc.Replace(" ", ""), DataStorage.DevTools.DtDb);
                if (dtModel00 == null) return result;
                var eff = MST.MES.EfficiencyCalculation.CalculateEfficiency(ConnectedMstOrder.SmtData.smtStartDate,
                                                                            ConnectedMstOrder.SmtData.smtEndDate,
                                                                            dtModel00,
                                                                            ManufacturedQty,
                                                                            ConnectedMstOrder.SmtData.smtLine,
                                                                            false);
                return Math.Round(eff * 100, 1).ToString() + "%";

            }
        }

    }
}
