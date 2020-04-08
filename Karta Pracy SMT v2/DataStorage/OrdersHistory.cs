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
                    //FillOutDgvOrders();
                    ListViewOrders.SourceListForListView.RefreshListView();
                    MakeShiftColors();
                }
            }
        }


        private static void FillOutDgvOrders()
        {
            dgvOrders.Rows.Clear();
            foreach (var order in _ordersHistory.OrderByDescending(o=>o.SmtData.smtStartDate))
            {
                if (order.OrderNo == "2011965")
                    ;
                var eff = MST.MES.EfficiencyCalculation.CalculateEfficiency(order.SmtData.smtStartDate,
                                                                            order.SmtData.smtEndDate,
                                                                            order.modelInfo.DtModel00,
                                                                            order.SmtData.manufacturedQty,
                                                                            order.SmtData.smtLine,
                                                                            false);
                var modelName = order.ModelName;
                if (order.SmtData.changeOver)
                {
                    modelName = "Przestawienie";
                    if (dgvOrders.Rows.Count > 0)
                    {
                        string prevOrderNo = dgvOrders.Rows[dgvOrders.Rows.Count - 1].Cells["ColOrderNo"].Value.ToString();
                        string prevModel = dgvOrders.Rows[dgvOrders.Rows.Count - 1].Cells["ColName"].Value.ToString();
                        if(prevModel == "Przestawienie" & prevOrderNo == order.OrderNo)
                        {
                            int prevDuration = int.Parse(dgvOrders.Rows[dgvOrders.Rows.Count - 1].Cells["ColDuration"].Value.ToString().Replace("min", ""));
                            dgvOrders.Rows[dgvOrders.Rows.Count - 1].Cells["ColStart"].Value = order.SmtData.smtStartDate;
                            DateTime endDate = (DateTime)dgvOrders.Rows[dgvOrders.Rows.Count - 1].Cells["ColEnd"].Value;
                            dgvOrders.Rows[dgvOrders.Rows.Count - 1].Cells["ColDuration"].Value = $"{(int)(endDate-order.SmtData.smtStartDate).TotalMinutes}min";
                            continue;
                        }
                    }
                }

                dgvOrders.Rows.Add(order.OrderNo,
                                   order.Model10NcFormated,
                                   modelName,
                                   order.SmtData.manufacturedQty,
                                   order.SmtData.ng,
                                   order.SmtData.smtStartDate,
                                   order.SmtData.smtEndDate,
                                   $"{(int)(order.SmtData.smtEndDate - order.SmtData.smtStartDate).TotalMinutes}min",
                                   Math.Round(eff * 100, 1) + "%");

                if (modelName == "Przestawienie") 
                {
                    foreach(DataGridViewCell cell in  dgvOrders.Rows[dgvOrders.Rows.Count - 1].Cells){
                        cell.Style.Font = new System.Drawing.Font(dgvOrders.DefaultCellStyle.Font.FontFamily, (float)9, System.Drawing.FontStyle.Regular);
                    }
                }
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
            var smtOrders = MesData.SmtData.SelectMany(o => o.Value.smtOrders)
                                           .Where(o => o.smtLine == GlobalParameters.SmtLine)
                                           .OrderByDescending(o => o.smtEndDate);
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
            ListViewOrders.SourceListForListView.RefreshListView();
            //FillOutDgvOrders();
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
