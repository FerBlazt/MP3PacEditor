using System;
using System.IO;

namespace MP3PacEditor
{
    public class AudioFileCopy
    {
        public string FilePath { get; }

        public AudioFileCopy(string filePath)
        {
            FilePath = filePath;
        }

        public AudioFileCopy Clone(string customFileName = null)
        {
            string directory = Path.GetDirectoryName(FilePath);
            string extension = Path.GetExtension(FilePath);
            string baseName = Path.GetFileNameWithoutExtension(FilePath);
            string copyName = customFileName ?? $"{baseName}_copy";
            string newPath = Path.Combine(directory, copyName + extension);

            int count = 1;
            while (File.Exists(newPath))
            {
                newPath = Path.Combine(directory, $"{copyName}({count}){extension}");
                count++;
            }

            File.Copy(FilePath, newPath);
            return new AudioFileCopy(newPath);
        }
    }
}
