using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class SmtLineFile
    {
        public static string ReadLine()
        {
            string fileName = @"Linia SMT.txt";
            if (File.Exists(fileName))
            {
                var fileContent = File.ReadAllText(fileName).Trim();
                if (fileContent.StartsWith("SMT")) return fileContent;
            }

            File.WriteAllText(fileName, "SMT2");
            return "SMT2";
        }
    }
}
