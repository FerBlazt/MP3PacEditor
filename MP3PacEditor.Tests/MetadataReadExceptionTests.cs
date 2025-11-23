using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MP3PacEditor.Tests
{
    public class MetadataReadExceptionTests
    {
        [Test]
        public void Constructor_SetsFilePathAndLogsMessage()
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exceptionlog.txt");
            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }

            string filePath = "/music/song.mp3";
            string message = "Unable to read metadata";

            var exception = new MetadataReadException(filePath, message, new InvalidOperationException("broken"));

            Assert.That(exception.FilePath, Is.EqualTo(filePath));
            Assert.That(exception.Message, Is.EqualTo(message));
            Assert.That(File.Exists(logPath), Is.True);

            string logContent = File.ReadAllText(logPath);
            StringAssert.Contains(filePath, logContent);
            StringAssert.Contains(message, logContent);
            StringAssert.Contains("InvalidOperationException", logContent);
        }
    }
}