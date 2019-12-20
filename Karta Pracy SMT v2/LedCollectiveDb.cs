using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class LedCollectiveDb
    {
        public static Dictionary<string, LedOracleSpecStruct> nc12ToCollective = new Dictionary<string, LedOracleSpecStruct>();

        public class LedOracleSpecStruct
        {
            public string nc12;
            public string name;
            public int cct = 0;
            public string collective = "";
            public int qtyPerModel;
        }
        public static void LoadDb()
        {
            Dictionary<string, LedOracleSpecStruct> result = new Dictionary<string, LedOracleSpecStruct>();
            using (SqlConnection conn = new SqlConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Connection.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=ConnectToMSTDB;User Id=mes;Password=mes;";
                    cmd.CommandText = @"SELECT NC12,Opis,CCT,Colective FROM ConnectToMSTDB.dbo.v_Item";
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string collective = SafeGetString(rdr, "Colective");
                            string nc12 = SafeGetString(rdr, "NC12");
                            //if (!nc12.StartsWith("4010560")) continue;
                            string description = SafeGetString(rdr, "Opis");
                            int cct = SafeGetInt(rdr, "CCT");

                            //if (collective == "")
                            //{
                            //    //collective = nc12;
                            //    collective = "";
                            //}

                            if (nc12.StartsWith("4010460"))
                            {
                                //collective = nc12;
                                collective = nc12;
                            }

                            if (!result.ContainsKey(nc12))
                            {
                                result.Add(nc12, new LedOracleSpecStruct
                                {
                                    collective = collective,
                                    name = description,
                                    cct = cct,
                                    nc12 = nc12
                                });
                            }
                            else
                            {
                                if (result[nc12].collective == "")
                                {
                                    result[nc12].collective = collective;
                                    result[nc12].cct = cct;
                                }
                            }
                        }
                    }
                }
            }
            nc12ToCollective = result;
        }

        private static string SafeGetString(SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return "";
        }

        private static int SafeGetInt(SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return 0;
        }
    }
}
