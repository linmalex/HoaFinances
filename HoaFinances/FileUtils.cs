using System;
using System.Globalization;
using System.IO;

namespace HoaFinances
{
    public class FileUtils
    {
        public static void RenameAllFiles(string dirPath)
        {
            DirectoryInfo directory = new DirectoryInfo(dirPath);
            FileInfo[] files = directory.GetFiles();

            foreach (var item in files)
            {
                string newPath = GetNewName(dirPath, item);
                item.MoveTo(newPath);
            }
        }
        //this logic will change depending on use case, obv
        private static string GetNewName(string srcDir, FileInfo item)
        {
            var fileName = Path.GetFileNameWithoutExtension(item.Name);
            var nameInt = Int32.Parse(fileName);
            var month = DateTimeFormatInfo.InvariantInfo.GetMonthName(nameInt);
            var newName = $"{fileName}_{month}.pdf";
            return Path.Combine(srcDir, newName);
        }
    }
}
