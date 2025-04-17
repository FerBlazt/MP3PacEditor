using System;
using System.IO;

namespace MP3PacEditor
{
    public class MetadataReadException : Exception
    {
        public string FilePath { get; }

        public MetadataReadException(string filePath, string message, Exception innerException = null)
            : base(message, innerException)
        {
            FilePath = filePath;

            LogToFile(filePath, message, innerException);
        }

        private void LogToFile(string filePath, string message, Exception inner)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exceptionlog.txt");
            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine("=== Metadata Read Exception ===");
                    writer.WriteLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"File: {filePath}");
                    writer.WriteLine($"Message: {message}");
                    if (inner != null)
                    {
                        writer.WriteLine($"Inner Exception: {inner.GetType().Name} - {inner.Message}");
                        writer.WriteLine($"Stack Trace:\n{inner.StackTrace}");
                    }
                    writer.WriteLine(new string('-', 50));
                }
            }
            catch
            {
                // lol
            }
        }
    }
}
