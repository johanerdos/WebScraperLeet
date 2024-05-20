using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperLeet.FileService
{
    public interface IFileService
    {
        Task SavePageAsync(string localFilePath, string fileName, string content);
    }
}
