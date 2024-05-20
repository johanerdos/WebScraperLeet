using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperLeet.FileService
{
    public class FileService : IFileService
    {
        public async Task SavePageAsync(string localFilePath, string fileName, string content)
        {
            var folderName = Path.Combine(localFilePath, fileName);

            try
            {
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var filePath = Path.Combine(folderName, "index.html");
                await File.WriteAllTextAsync(filePath, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when saving the file: {ex.Message}");
            }
        }
    }
}
