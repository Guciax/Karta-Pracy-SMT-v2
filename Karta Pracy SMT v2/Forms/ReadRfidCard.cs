using Karta_Pracy_SMT_v2.DataStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MST.MES.UsersDataBase;
using static MST.MES.UsersDataBase.DataStructures;

namespace Karta_Pracy_SMT_v2.Forms
{
    public partial class ReadRfidCard : Form
    {
        private readonly string requiredAccess;
        public string idHex = "";
        public string userName = "";
        public UserStructure userData;
        public string deviceMonikerString = "";
        private List<UserStructure> usersList = new List<UserStructure>();
        public ReadRfidCard(string requiredAccess=null)
        {
            InitializeComponent();
            this.requiredAccess = requiredAccess;
        }

        private void ReadRfidCard_Load(object sender, EventArgs e)
        {
            usersList = MST.MES.UsersDataBase.SqlOperations.GetUsersFromDb();
            this.ActiveControl = tbScanCard;
        }

        private void tbScanCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;
            //if (KeyboardDeviceListener.lastInputDeviceName != GlobalParameters.CardReaderName)
            //{
            //    tbScanCard.Text = "";
            //    MessageBox.Show("Użyj czytnika kart");
            //    return;
            //}
            if (requiredAccess != null)
            {
                if (tbScanCard.Text.Length != 10)
                {
                    tbScanCard.Text = "";
                    MessageBox.Show("Błędne dane, spróbuj ponownie");
                    return;
                }

                long idInt = 0;
                if (!long.TryParse(tbScanCard.Text, out idInt))
                {
                    tbScanCard.Text = "";
                    MessageBox.Show("Błędne dane, spróbuj ponownie");
                    return;
                }
                idHex = KeyboardDeviceListener.ReverseHex(idInt.ToString("X"));

                //check access, compare to input requiredAccess
                var matchingUser = usersList.Where(u => u.CardId == idHex).ToList();
                if (matchingUser.Count == 0)
                {
                    MessageBox.Show("Brak użytkownika w bazie danych");
                    return;
                }
                userData = matchingUser.First();

                if (requiredAccess == "Technician" & !userData.AccessLevels.Technician)
                {
                    MessageBox.Show("Ten użytkownik nie ma uprawnień Technika");
                    return;
                }
                if (requiredAccess == "Oqa" & !userData.AccessLevels.Oqa)
                {
                    MessageBox.Show("Ten użytkownik nie ma uprawnień Inspektora Jakości");
                    return;
                }
            }
            

            deviceMonikerString = KeyboardDeviceListener.lastInputDeviceName;

            this.DialogResult = DialogResult.OK;
        }
    }
}
