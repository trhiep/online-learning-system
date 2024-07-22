using System.IO.Compression;

namespace OnlineLearningSystem.Utils
{
    public class FileHelper
    {
        public static string ZipFolder(string sourceFolder, string zipFilePath)
        {
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            ZipFile.CreateFromDirectory(sourceFolder, zipFilePath);
            return zipFilePath;
        }
    }
}
