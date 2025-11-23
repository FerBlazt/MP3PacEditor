using System;
using System.Collections.Generic;
using System.IO;

namespace MP3PacEditor
{
    public static class AudioFileHelper
    {
        // All formats supported by TagLib#
        private static readonly string[] SupportedExtensions =
        {
            ".mp3", ".flac", ".ogg", ".wav", ".aiff", ".wma", ".m4a", ".mp4",
            ".aac", ".alac", ".ape", ".mpc", ".wv"
        };

        public static IEnumerable<string> GetSupportedAudioFiles(string folderPath)
        {
            foreach (string file in Directory.EnumerateFiles(folderPath, "*", SearchOption.TopDirectoryOnly))
            {
                string extension = Path.GetExtension(file);

                if (IsSupportedExtension(extension))
                {
                    yield return file;
                }
            }
        }

        private static bool IsSupportedExtension(string extension)
        {
            foreach (string supported in SupportedExtensions)
            {
                if (string.Equals(supported, extension, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
