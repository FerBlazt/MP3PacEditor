using System;
using System.IO;

namespace MP3PacEditor
{
    public class MetadataReadException : Exception
    {
        private const string LogFileName = "exceptionlog.txt";

        public string FilePath { get; }

        public MetadataReadException(string filePath, string message, Exception innerException = null)
            : base(message, innerException)
        {
            FilePath = filePath;

            TryLogToFile(this);
        }

        private static void TryLogToFile(MetadataReadException exception)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFileName);

            try
            {
                using (var writer = new StreamWriter(logPath, append: true))
                {
                    WriteLogEntry(writer, exception);
                }
            }
            catch (Exception logEx)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to write metadata log: {logEx.Message}");
            }
        }

        private static void WriteLogEntry(TextWriter writer, MetadataReadException exception)
        {
            Exception inner = exception.InnerException;

            writer.WriteLine("=== Metadata Read Exception ===");
            writer.WriteLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            writer.WriteLine($"File: {exception.FilePath}");
            writer.WriteLine($"Message: {exception.Message}");

            if (inner != null)
            {
                writer.WriteLine($"Inner Exception: {inner.GetType().Name} - {inner.Message}");
                writer.WriteLine($"Stack Trace:{Environment.NewLine}{inner.StackTrace}");
            }

            writer.WriteLine(new string('-', 100));
        }
    }
}
