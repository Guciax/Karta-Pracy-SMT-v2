using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class StartUp
    {
        private static FileInfo exeFile = null;
        private static string localExeFile = null;
        public static bool DoWeNeedToStartAppFromLocalDrive()
        {

            if (!IsAppRunningFromLocalDrive())
            {
                CheckUpdatedFiles(exeFile);
                if (GetRunningAppCount(exeFile.Name) < 2)
                    Process.Start(localExeFile);
                return true;
            }
            return false;
        }

        private static bool IsAppRunningFromLocalDrive()
        {
            var exeLoc = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeFile = new FileInfo(exeLoc);
            string drive = Path.GetPathRoot(exeFile.FullName);
            //MessageBox.Show($"I'm running from {drive}");
            if (drive.ToUpper() == @"C:\") return true;
            return false;
        }

        private static bool CheckUpdatedFiles(FileInfo netExeFile)
        {
            DirectoryInfo localPath = new DirectoryInfo(@"C:\Planowanie zleceń LED");
            localExeFile = Path.Combine(localPath.FullName, exeFile.Name);
            Directory.CreateDirectory(localPath.FullName);
            bool result = false;

            foreach (string dirPath in Directory.GetDirectories(netExeFile.DirectoryName, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(netExeFile.DirectoryName, localPath.FullName));

            foreach (string netFile in Directory.GetFiles(netExeFile.DirectoryName, "*.*", SearchOption.AllDirectories))
            {
                FileInfo destFile = new FileInfo(netFile.Replace(netExeFile.DirectoryName, localPath.FullName));
                FileInfo sourceFile = new FileInfo(netFile);
                if (destFile.Exists)
                {
                    if (sourceFile.LastWriteTime <= destFile.LastWriteTime)
                    {
                        continue;
                    }
                }
                File.Copy(netFile, netFile.Replace(netExeFile.DirectoryName, localPath.FullName), true);
                result = true;
            }
            return result;
        }
        private static int GetRunningAppCount(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);
            return processes.Count();
        }
    }
}
