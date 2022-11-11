using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

using KeLi.FileRenamer.Properties;

namespace KeLi.FileRenamer
{
    internal class Program
    {
        private static void Main()
        {
            var appDirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryInfo = new DirectoryInfo(appDirPath);
            var fileInfos = directoryInfo.GetFiles(Settings.Default.FilterType, SearchOption.AllDirectories);

            foreach (var fileInfo in fileInfos)
            {
                var fileDirPath = fileInfo.DirectoryName;
                var fileName = fileInfo.Name;
                var matches = Regex.Matches(fileName, Settings.Default.WordRegex);

                foreach (Match match in matches)
                {
                    fileName = fileName.Replace(match.Value + " ", match.Value);
                    fileName = fileName.Replace(" " + match.Value, match.Value);
                }

                File.Move(fileInfo.FullName, Path.Combine(fileDirPath, fileName));
            }
        }
    }
}
