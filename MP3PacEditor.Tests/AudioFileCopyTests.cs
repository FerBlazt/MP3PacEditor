using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MP3PacEditor.Tests
{
    public class AudioFileCopyTests
    {
        [Test]
        public void Clone_CreatesCopyWithDefaultName()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            try
            {
                string sourceFile = Path.Combine(tempDir, "song.mp3");
                File.WriteAllText(sourceFile, "audio data");

                var copy = new AudioFileCopy(sourceFile);
                AudioFileCopy result = copy.Clone();

                StringAssert.Contains("song_copy.mp3", result.FilePath);
                Assert.That(File.Exists(result.FilePath));
                Assert.That(File.ReadAllText(result.FilePath), Is.EqualTo("audio data"));
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public void Clone_AppendsCounterWhenCopyExists()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            try
            {
                string sourceFile = Path.Combine(tempDir, "song.mp3");
                string customName = "custom_name";
                File.WriteAllText(sourceFile, "audio data");

                string existingCopy = Path.Combine(tempDir, $"{customName}.mp3");
                File.WriteAllText(existingCopy, "existing copy");

                var copy = new AudioFileCopy(sourceFile);
                AudioFileCopy result = copy.Clone(customName);

                StringAssert.Contains("custom_name(1).mp3", result.FilePath);
                Assert.That(File.Exists(result.FilePath));
                Assert.That(File.ReadAllText(result.FilePath), Is.EqualTo("audio data"));
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}