using Karta_Pracy_SMT_v2.DataStructures;
using Karta_Pracy_SMT_v2.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2.DataStorage
{
    public class CurrentMstOrder
    {
        private static MstOrder _currentOrder;

        public static ListView lvOrderInfo;
        public static ListView lvProdNorms;
        public static MstOrder currentOrder
        {
            get
            {
                return _currentOrder;
            }
            set
            {
                _currentOrder = value;
                UpdateListViewOrderInfo();
                UpdateListViewProductionNorms();
            }
        }

        private static void UpdateListViewProductionNorms()
        {
            lvProdNorms.Items.Clear();
            if (_currentOrder == null) return;
            var norm = MST.MES.EfficiencyCalculation.CalculateModelNormPerHour(_currentOrder.modelInfo.DtModel00, GlobalParameters.SmtLine);
            TryGetMbOrPcb(lvProdNorms);
            TryGetPcbPerMb(lvProdNorms);
            TryGetLedCount(lvProdNorms);
            TryGetConnCount(lvProdNorms);
            TryGetResCount(lvProdNorms);

            lvProdNorms.Items.Add(new ListViewItem());
            lvProdNorms.Items.Add(new ListViewItem(new[] { "Czas cyklu:", $"{norm.lineCT} sek." }));
            lvProdNorms.Items.Add(new ListViewItem(new[] { "Wydajność:", $"{norm.outputPerHour} szt./godz." }));

            lvProdNorms.Columns[0].Width = -1;
            lvProdNorms.Columns[1].Width = -1;
        }

        public static void UpdateListViewOrderInfo()
        {
            lvOrderInfo.Items.Clear();
            if (_currentOrder != null)
            {
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Numer:", _currentOrder.OrderNo }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "10NC:", _currentOrder.Model10NcFormated }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Nazwa:", _currentOrder.ModelName }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Ilość zlecona:", $"{_currentOrder.KittingData.orderedQty} szt." }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Wykonano:", $"{_currentOrder.ManufacturedQty} szt." }));
                int previousQty = OrdersHistory.ordersHistory.Where(o => o.OrderNo == _currentOrder.OrderNo).Select(o => o.SmtData.manufacturedQty).Sum();
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Łącznie:", $"{previousQty + _currentOrder.ManufacturedQty} szt." }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Odpad:", $"{_currentOrder.NgQty} szt." }));
            }

            lvOrderInfo.Columns[0].Width = -1;
            lvOrderInfo.Columns[1].Width = -1;
        }

        private static void TryGetResCount(ListView lvOrderInfo)
        {
            var resCount = MST.MES.DtTools.GetResCount(_currentOrder.modelInfo.DtModel00);
            if (resCount > 0)
            {
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "RES:", resCount.ToString() + " szt." }));
            }
        }

        private static void TryGetConnCount(ListView lvOrderInfo)
        {
            var connCount = MST.MES.DtTools.GetConnCount(_currentOrder.modelInfo.DtModel00);
            if (connCount > 0)
            {
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "CONN:", connCount.ToString() + " szt." }));
            }
        }

        private static void TryGetLedCount(ListView lvOrderInfo)
        {
            var ledCount = MST.MES.DtTools.GetLedCount(_currentOrder.modelInfo.DtModel00);
            if (ledCount > 0)
            {
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "LED:", ledCount.ToString() + " szt." }));
            }
        }

        private static void TryGetPcbPerMb(ListView lvOrderInfo)
        {
            var pcbPerMb = MST.MES.DtTools.GetPcbPerMbCount(_currentOrder.modelInfo.DtModel00);
            if (pcbPerMb > 0)
            {
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "PCB / MB:", pcbPerMb.ToString() + " szt." }));
            }
        }

        private static void TryGetMbOrPcb(ListView lvOrderInfo)
        {
            if (_currentOrder.modelInfo.DtModel00 != null)
            {
                if (MST.MES.DtTools.GetMb12NC(_currentOrder.modelInfo.DtModel00) != null)
                {
                    lvOrderInfo.Items.Add(new ListViewItem( new[] { "MB:", MST.MES.DtTools.GetMb12NC(_currentOrder.modelInfo.DtModel00)}));
                }
                else if (MST.MES.DtTools.GetPcb12NC(_currentOrder.modelInfo.DtModel00) != null)
                {
                    lvOrderInfo.Items.Add(new ListViewItem(new[] {"PCB:", MST.MES.DtTools.GetMb12NC(_currentOrder.modelInfo.DtModel00)}));
                }
            }
        }

        private static bool updateFormIsDisplayed = false;
        public static void UpdateOrderQty()
        {
            if (currentOrder == null) return;
            if ((DateTime.Now - currentOrder.LastUpdateTime).TotalMinutes < 30) return;
            if (updateFormIsDisplayed) return;

            using (UpdateOrderQuantity updForm = new UpdateOrderQuantity(CurrentMstOrder.currentOrder))
            {
                updateFormIsDisplayed = true;
                if (updForm.ShowDialog() == DialogResult.OK)
                {
                    CurrentMstOrder.currentOrder.ManufacturedQty = updForm.result;

                    if (CurrentMstOrder.currentOrder.dbRecordIndex > 0)
                    {
                        //update
                        SqlOperations.UpdateCurrentMstOrderQuantity(updForm.result, CurrentMstOrder.currentOrder.dbRecordIndex);
                    }
                    else
                    {
                        //insert
                        CurrentMstOrder.currentOrder.dbRecordIndex = SqlOperations.InsertCurrentRecordToDb(CurrentMstOrder.currentOrder);
                    }

                    currentOrder.LastUpdateTime = DateTime.Now;
                    updateFormIsDisplayed = false;
                }
            }


        }
    }
}
