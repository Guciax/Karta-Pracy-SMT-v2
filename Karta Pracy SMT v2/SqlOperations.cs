using Karta_Pracy_SMT_v2.DataStorage;
using Karta_Pracy_SMT_v2.DataStructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class SqlOperations
    {
        internal static void UpdateCurrentMstOrderQuantity( int recordId)
        {
            using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
            {
                openCon.Open();
                int newQty = CurrentOrder.CurrentMstOrder.currentOrder.ManufacturedQty;
                string updString = "UPDATE tb_SMT_Karta_Pracy SET IloscWykonana=@qty, DataCzasKoniec=@DataCzasKoniec WHERE id = @id";
                using (SqlCommand querySave = new SqlCommand(updString))
                {
                    querySave.Connection = openCon;
                    querySave.Parameters.AddWithValue("@qty", newQty);
                    querySave.Parameters.AddWithValue("@id", recordId);
                    querySave.Parameters.AddWithValue("@DataCzasKoniec", DateTime.Now);
                    querySave.ExecuteNonQuery();
                }
            }
        }
        internal static int InsertSmtRecordToDb(MstOrder mstRecord)
        {
            int lastRecordId = InsertRecordToDb(mstRecord.SmtData.smtStartDate,
                             DateTime.Now,
                             GlobalParameters.SmtLine,
                             mstRecord.OperatorName,
                             mstRecord.OrderNo,
                             mstRecord.Model10Nc,
                             mstRecord.ManufacturedQty.ToString(),
                             mstRecord.NgQty.ToString(),
                             "0",
                             "check",
                             "",
                             mstRecord.StencilId,
                             "MST",
                             0);

            if (lastRecordId > 0)
            {
                return lastRecordId;
            }
            return -1;
        }


        public static int InsertRecordToDb(DateTime startDate, DateTime endDate, string smtLine, string operatorSMT, string lotNo, string model, string manufacturedQty, string ngQty, string scrapQty, string firstPieceCheck, string ledLefts, string stencil, string client, double totalLedsUsed)
        {
            int result = -1;
            bool release = true;
            if ((endDate - startDate).TotalMinutes < 1)
            {
                endDate = endDate.AddMinutes(1);
            }

#if DEBUG
            //release = false;
#endif
            if (release)
            {
                using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
                {
                    string save = "INSERT into tb_SMT_Karta_Pracy (DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,NGIlosc,ScrapIlosc,Kontrola1szt,KoncowkiLED,StencilQR,Client,Zuzycie_Led) VALUES (@DataCzasStart,@DataCzasKoniec,@LiniaSMT,@OperatorSMT,@NrZlecenia,@Model,@IloscWykonana,@NGIlosc,@ScrapIlosc,@Kontrola1szt,@KoncowkiLED,@StencilQR,@Client,@Zuzycie_Led)";
                    save += "; SELECT CAST(scope_identity() AS int)";
                    using (SqlCommand querySave = new SqlCommand(save))
                    {

                        querySave.Connection = openCon;
                        querySave.Parameters.Add("@DataCzasStart", SqlDbType.SmallDateTime).Value = startDate;
                        querySave.Parameters.Add("@DataCzasKoniec", SqlDbType.SmallDateTime).Value = endDate;
                        querySave.Parameters.Add("@LiniaSMT", SqlDbType.VarChar, 50).Value = smtLine;
                        querySave.Parameters.Add("@OperatorSMT", SqlDbType.VarChar, 50).Value = operatorSMT;
                        querySave.Parameters.Add("@NrZlecenia", SqlDbType.VarChar, 50).Value = lotNo;
                        querySave.Parameters.Add("@Model", SqlDbType.VarChar, 50).Value = model;
                        querySave.Parameters.Add("@IloscWykonana", SqlDbType.VarChar, 50).Value = manufacturedQty;
                        querySave.Parameters.Add("@NGIlosc", SqlDbType.VarChar, 50).Value = ngQty;
                        querySave.Parameters.Add("@ScrapIlosc", SqlDbType.VarChar, 50).Value = scrapQty;
                        querySave.Parameters.Add("@Kontrola1szt", SqlDbType.VarChar, 50).Value = firstPieceCheck;
                        querySave.Parameters.Add("@KoncowkiLED", SqlDbType.VarChar, 255).Value = ledLefts;
                        querySave.Parameters.Add("@Zuzycie_Led", SqlDbType.Int, 255).Value = totalLedsUsed;
                        querySave.Parameters.Add("@StencilQR", SqlDbType.VarChar, 255).Value = stencil;
                        querySave.Parameters.Add("@Client", SqlDbType.VarChar, 3).Value = client;
                        openCon.Open();
                        result = (int)querySave.ExecuteScalar();
                        ///querySave.ExecuteNonQuery();
                    }
                }
            }
            return result;
        }

        public static void InsertChangeOverRecordToDb(DateTime startDate, DateTime endDate, string smtLine, string operatorSMT, string lotNo, string model, string technician, string oqa)
        {

            bool release = true;
            if ((endDate - startDate).TotalMinutes < 1)
            {
                endDate = endDate.AddMinutes(1);
            }

#if DEBUG
            //release = false;
#endif
            if (release)
            {
                using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
                {
                    string save = "INSERT into tb_SMT_Karta_Pracy (DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,KoncowkiLED,Przestawienie,NGIlosc,ScrapIlosc,Kontrola1szt,StencilQR,Client,Zuzycie_Led) VALUES (@DataCzasStart,@DataCzasKoniec,@LiniaSMT,@OperatorSMT,@NrZlecenia,@Model,@IloscWykonana,@KoncowkiLED, @Przestawienie,@NGIlosc,@ScrapIlosc,@Kontrola1szt,@StencilQR,@Client,@Zuzycie_Led)";

                    using (SqlCommand querySave = new SqlCommand(save))
                    {
                        querySave.Connection = openCon;
                        querySave.Parameters.Add("@DataCzasStart", SqlDbType.SmallDateTime).Value = startDate;
                        querySave.Parameters.Add("@DataCzasKoniec", SqlDbType.SmallDateTime).Value = endDate;
                        querySave.Parameters.Add("@LiniaSMT", SqlDbType.VarChar, 50).Value = smtLine;
                        querySave.Parameters.Add("@OperatorSMT", SqlDbType.VarChar, 50).Value = operatorSMT;
                        querySave.Parameters.Add("@NrZlecenia", SqlDbType.VarChar, 50).Value = lotNo;
                        querySave.Parameters.Add("@Model", SqlDbType.VarChar, 50).Value = model;
                        querySave.Parameters.Add("@IloscWykonana", SqlDbType.VarChar, 50).Value = "0";
                        querySave.Parameters.Add("@KoncowkiLED", SqlDbType.VarChar, 255).Value = "";
                        querySave.Parameters.Add("@Przestawienie", SqlDbType.VarChar, 255).Value = 1;
                        querySave.Parameters.Add("@NGIlosc", SqlDbType.VarChar, 255).Value = "0";
                        querySave.Parameters.Add("@ScrapIlosc", SqlDbType.VarChar, 255).Value = "0";
                        querySave.Parameters.Add("@Kontrola1szt", SqlDbType.VarChar, 255).Value = $"{technician};{oqa}";
                        querySave.Parameters.Add("@StencilQR", SqlDbType.VarChar, 255).Value = "";
                        querySave.Parameters.Add("@Client", SqlDbType.VarChar, 255).Value = "";
                        querySave.Parameters.Add("@Zuzycie_Led", SqlDbType.VarChar, 255).Value = 0;
                        openCon.Open();

                        querySave.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
