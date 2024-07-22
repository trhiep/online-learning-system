using System.Reflection.Metadata;

namespace OnlineLearningSystem.Utils
{
    public class Logger
    {
        private readonly string _directoryPath;
        private readonly string _filePath;

        public Logger(string directoryPath, string fileName)
        {
            _directoryPath = directoryPath;
            _filePath = Path.Combine(_directoryPath, fileName);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            // Tạo file nếu chưa tồn tại
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }
        }

        public void Log(string account,string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine($"{account}| {DateTime.Now}| {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }

}
