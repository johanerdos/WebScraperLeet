using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperLeet.ScrapingService
{
    public interface IScrapingService
    {
        Task InvokeScraper();
    }
}
