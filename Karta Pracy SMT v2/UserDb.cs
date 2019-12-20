using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class UserDb
    {
        public static List<MST.MES.UsersDataBase.DataStructures.UserStructure> Users { get; set; }
        private static void GetDbUsers()
        {
            Users = MST.MES.UsersDataBase.SqlOperations.GetUsersFromDb();
        }

        public static async Task GetUsersFromDbAsync()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => GetDbUsers()));
            await Task.WhenAll(tasks);
        }
    }
}
