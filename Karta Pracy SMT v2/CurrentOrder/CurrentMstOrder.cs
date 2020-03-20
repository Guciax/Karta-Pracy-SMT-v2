using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.DataStructures;
using Karta_Pracy_SMT_v2.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2.CurrentOrder
{
    public class CurrentMstOrder
    {
        private static MstOrder _currentOrder;
        public static bool updateFormIsDisplayed = false;
        public static ListView lvOrderInfo;
        public static ListView lvProdNorms;
        public static MST.MES.UsersDataBase.DataStructures.UserStructure userOperator = new MST.MES.UsersDataBase.DataStructures.UserStructure();

        public static MstOrder currentOrder
        {
            get
            {
                return _currentOrder;
            }
            set
            {
                _currentOrder = value;
                ConnectedToCurrentOrder.SetUpConnectedOrder();
                UpdateListViewOrderInfo();
                UpdateListViewProductionNorms();
                OtherComponents.otherComponentsList.RemoveAll(c => c.componentMissing);
                DevTools.PcbPerMbOverwritenByUser = 0;
            }
        }

        public static void UpdateListViewProductionNorms()
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
            var qtyToMake = currentOrder.KittingData.orderedQty - currentOrder.ManufacturedQty;
            lvProdNorms.Items.Add(new ListViewItem(new[] { $"Czas produkcji {qtyToMake}szt:", $"{(int)(60 / norm.outputPerHour * qtyToMake)} min." }));

            lvProdNorms.Columns[0].Width = -1;
            lvProdNorms.Columns[1].Width = -1;
        }

        public static void UpdateListViewOrderInfo()
        {
            lvOrderInfo.Items.Clear();
            if (_currentOrder != null) 
            {
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Numer:", _currentOrder.OrderNo }));
                if (ConnectedToCurrentOrder.ConnectedOrder != null)
                {
                    lvOrderInfo.Items.Add(new ListViewItem(new[] { "Pow. numer:", ConnectedToCurrentOrder.ConnectedOrder.OrderNo }));
                }
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "10NC:", _currentOrder.Model10NcFormated }));
                if (ConnectedToCurrentOrder.ConnectedOrder != null)
                {
                    lvOrderInfo.Items.Add(new ListViewItem(new[] { "Pow. 10NC:", ConnectedToCurrentOrder.ConnectedOrder.Model10NcFormated }));
                }
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Nazwa:", _currentOrder.ModelName }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Ilość zlecona:", $"{_currentOrder.KittingData.orderedQty} szt." }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Wykonano:", $"{_currentOrder.ManufacturedQty} szt." }));

                int previousQty = MesData.SmtData.Where(o => o.Key == _currentOrder.OrderNo).Select(x => x.Value.totalManufacturedQty).Sum();
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Łącznie:", $"{previousQty + _currentOrder.ManufacturedQty} szt." }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Start:", _currentOrder.SmtData.smtStartDate.ToString("HH:mm") }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Wydajność:", $"{Math.Round(_currentOrder.ManufacturedQty / (DateTime.Now - _currentOrder.SmtData.smtStartDate).TotalHours,1)} szt/h" }));
                lvOrderInfo.Items.Add(new ListViewItem(new[] { "Stencil:", _currentOrder.StencilId }));
                if(ConnectedToCurrentOrder.ConnectedOrder != null)

                if (_currentOrder.KittingData.ledsChoosenByPlanner != null) 
                {
                    lvOrderInfo.Items.Add(new ListViewItem(new[] { "Diody LED:" }));
                    Char binLetter = 'A';
                    foreach (var led in _currentOrder.KittingData.LedChoosenStructList)
                    {
                        lvOrderInfo.Items.Add(new ListViewItem(new[] { $"BIN{binLetter.ToString()}:", led.CollectiveFormatedRank }));
                        binLetter++;
                    }
                }
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
            var pcbPerMb = DevTools.CurrentModelPcbPerMb;
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

        public static void UpdateOrderQty(PictureBox pbBackgroundImage)
        {
            if (CurrentMstOrder.currentOrder == null) return;
            if ((DateTime.Now - CurrentMstOrder.currentOrder.LastUpdateTime).TotalMinutes < 30) return;
            if (CurrentMstOrder.updateFormIsDisplayed) return;
            if (ChangeOver.changeOverInProgress) return;

            using (UpdateOrderQuantity updForm = new UpdateOrderQuantity())
            {
                var smooth = BlurredBackground.ApplyBlur(BlurredBackground.ssGrayColor);
                pbBackgroundImage.Image = smooth;
                pbBackgroundImage.Visible = true;
                updateFormIsDisplayed = true;
                if (updForm.ShowDialog() == DialogResult.OK)
                {
                    if (!GlobalParameters.Debug)
                    {
                        CurrentMstOrder.currentOrder.ManufacturedQty = updForm.result;
                        ConnectedToCurrentOrder.UpdateConnectedOrderQty();
                        CurrentMstOrder.UpdateOrSaveCurretOrderAndConnected();
                        
                        updateFormIsDisplayed = false;
                    }
                }
                pbBackgroundImage.Visible = false;
            }
        }

        public static void UpdateOrSaveCurretOrderAndConnected()
        {
            if (CurrentMstOrder.currentOrder.dbRecordIndex > 0)
            {
                //update
                SqlOperations.UpdateCurrentMstOrderQuantity(CurrentMstOrder.currentOrder.dbRecordIndex);
                if (ConnectedToCurrentOrder.ConnectedOrder != null)
                {
                    SqlOperations.UpdateCurrentMstOrderQuantity(ConnectedToCurrentOrder.ConnectedOrder.dbRecordIndex);
                }
            }
            else
            {
                //insert
                int thisOrderDbId = SqlOperations.InsertSmtRecordToDb(CurrentMstOrder.currentOrder);
                CurrentMstOrder.currentOrder.dbRecordIndex = thisOrderDbId;
                if (ConnectedToCurrentOrder.ConnectedOrder != null)
                {
                    int connectedOrderId = SqlOperations.InsertSmtRecordToDb(ConnectedToCurrentOrder.ConnectedOrder);
                    ConnectedToCurrentOrder.ConnectedOrder.dbRecordIndex = connectedOrderId;
                }
            }

            MST.MES.StencilManagement.AddCyclesToStencil(currentOrder.StencilId, currentOrder.ManufacturedQty / (int)currentOrder.modelInfo.PcbPerMbCount);
            currentOrder.LastUpdateTime = DateTime.Now;
        }
    }
}
