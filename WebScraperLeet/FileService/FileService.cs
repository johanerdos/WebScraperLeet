using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperLeet.FileService
{
    public class FileService : IFileService
    {
        public async Task<string> SavePageAsync(string localFilePath, string fileName, string content, string? parentFolderName = null)
        {
            var folderName = Path.Combine(localFilePath, fileName.Replace("/", "_"));
            var targetFolderName = string.IsNullOrEmpty(parentFolderName) ? folderName : Path.Combine(parentFolderName, fileName.Replace('/', '_'));

            try
            {
                if (!Directory.Exists(targetFolderName))
                {
                    Directory.CreateDirectory(targetFolderName);
                }

                var filePath = Path.Combine(targetFolderName, "index.html");
                await File.WriteAllTextAsync(filePath, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when saving the file: {ex.Message}");
            }

            return folderName;
        }
    }
}
