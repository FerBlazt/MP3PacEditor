using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MP3PacEditor.Tests
{
    public class AudioFileHelperTests
    {
        [Test]
        public void GetSupportedAudioFiles_ReturnsOnlyMatchingExtensions()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            try
            {
                List<string> expected = new();
                string[] supported = { ".mp3", ".flac", ".ogg", ".wav" };
                foreach (string ext in supported)
                {
                    string filePath = Path.Combine(tempDir, $"track{ext}");
                    File.WriteAllText(filePath, "test");
                    expected.Add(filePath);
                }

                // Unsupported file should be ignored
                string otherFile = Path.Combine(tempDir, "notes.txt");
                File.WriteAllText(otherFile, "ignore me");

                var result = AudioFileHelper.GetSupportedAudioFiles(tempDir).ToList();

                CollectionAssert.AreEquivalent(expected, result);
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}