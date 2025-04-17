using System.Collections.Generic;
using System.IO;

namespace MP3PacEditor
{
    public static class AudioFileHelper
    {
        // All formats supported by TagLib#
        private static readonly string[] SupportedExtensions = new[]
        {
            ".mp3", ".flac", ".ogg", ".wav", ".aiff", ".wma", ".m4a", ".mp4", ".aac", ".alac", ".ape", ".mpc", ".wv"
        };

        public static IEnumerable<string> GetSupportedAudioFiles(string folderPath)
        {
            foreach (string ext in SupportedExtensions)
            {
                foreach (string file in Directory.EnumerateFiles(folderPath, $"*{ext}", SearchOption.TopDirectoryOnly))
                {
                    yield return file;
                }
            }
        }
    }
}
