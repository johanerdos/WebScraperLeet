using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperLeet.HttpService
{
    public interface IHttpService
    {
        Task<string> FetchUrlContentAsync(string path);
    }
}
