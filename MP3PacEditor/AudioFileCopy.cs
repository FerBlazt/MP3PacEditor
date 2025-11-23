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
            string targetPath = GenerateCopyPath(customFileName);
            File.Copy(FilePath, targetPath);
            return new AudioFileCopy(targetPath);
        }

        private string GenerateCopyPath(string customFileName)
        {
            string directory = Path.GetDirectoryName(FilePath) ?? string.Empty;
            string extension = Path.GetExtension(FilePath);
            string baseName = Path.GetFileNameWithoutExtension(FilePath);

            string copyBaseName = string.IsNullOrWhiteSpace(customFileName)
                ? $"{baseName}_copy"
                : customFileName;

            string candidatePath = Path.Combine(directory, copyBaseName + extension);

            int count = 1;
            while (File.Exists(candidatePath))
            {
                candidatePath = Path.Combine(directory, $"{copyBaseName}({count}){extension}");
                count++;
            }

            return candidatePath;
        }
    }
}
