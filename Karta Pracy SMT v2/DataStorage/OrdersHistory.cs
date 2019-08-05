using Karta_Pracy_SMT_v2.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2.DataStorage
{
    public class OrdersHistory
    {
        public static DataGridView dgvOrders;
        private static List<MstOrder> _ordersHistory = new List<MstOrder>();
        private static bool suspendGridUpdate = false;
        public static List<MstOrder> ordersHistory
        {
            get
            {
                return _ordersHistory;
            }
            set
            {
                _ordersHistory = value;
                if (!suspendGridUpdate)
                {
                    FillOutDgvOrders();
                    MakeShiftColors();
                }
            }
        }

        private static void FillOutDgvOrders()
        {
            dgvOrders.Rows.Clear();
            
            foreach (var order in _ordersHistory.OrderByDescending(o=>o.EndTime))
            {
                var eff = MST.MES.EfficiencyCalculation.CalculateEfficiency(order.SmtData.smtStartDate,
                                                                            order.SmtData.smtEndDate,
                                                                            order.modelInfo.DtModel00,
                                                                            order.SmtData.manufacturedQty,
                                                                            order.SmtData.smtLine,
                                                                            true);
                dgvOrders.Rows.Add(order.OrderNo,
                                   order.Model10NcFormated,
                                   order.ModelName,
                                   order.SmtData.manufacturedQty,
                                   order.SmtData.ng,
                                   order.SmtData.smtStartDate,
                                   order.SmtData.smtEndDate,
                                   Math.Round(eff * 100, 1) + "%");
            }

            foreach (DataGridViewColumn column in dgvOrders.Columns)
            {
                if(column.Name == "ColName")
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            MakeShiftColors();
        }

        public static void MesOrdersToOrdersHistory(int ordersCount)
        {
            var smtOrders = MesData.SmtData.SelectMany(o => o.Value.smtOrders).OrderByDescending(o => o.smtEndDate);
            ordersHistory.Clear();
            foreach (var smtOrder in smtOrders)
            {
                if (!MesData.KittingData.ContainsKey(smtOrder.orderInfo.orderNo)) continue;
                var newOrder = new MstOrder
                {
                    KittingData = MesData.KittingData[smtOrder.orderInfo.orderNo],
                    SmtData = smtOrder,
                    modelInfo = new MstOrder.ModelInfo
                    {
                        DtModel00 = MST.MES.DtTools.GetDtModel00(MesData.KittingData[smtOrder.orderInfo.orderNo].modelId, DevTools.DtDb),
                        DtModel46 = MST.MES.DtTools.GetDtModel46(MesData.KittingData[smtOrder.orderInfo.orderNo].modelId, DevTools.DtDb)
                    }
                };
                ordersHistory.Add(newOrder);
                if (ordersHistory.Count == ordersCount) break;
            }
            suspendGridUpdate = false;
            FillOutDgvOrders();
        }

        private static void MakeShiftColors()
        {
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                DateTime start = (DateTime)row.Cells["ColStart"].Value;
                DateTime end = (DateTime)row.Cells["ColEnd"].Value;

                var startShift = MST.MES.DateTools.whatDayShiftIsit(start);
                var endShift = MST.MES.DateTools.whatDayShiftIsit(end);
                MST.MES.DateTools.dateShiftNo shift;

                if((end-startShift.fixedDate).Ticks > (startShift.fixedDate - start).Ticks)
                {
                    shift = endShift;
                }
                else
                {
                    shift = startShift;
                }

                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = MST.MES.Colors.shiftNoToColor[shift.shift];
                }
            }
        }
    }
}
