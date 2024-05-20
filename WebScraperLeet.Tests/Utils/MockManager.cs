using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraperLeet.HttpService;

namespace WebScraperLeet.Tests.Utils
{
    public class MockManager
    {
        public MockContainer SetUpMocks()
        {
            return new MockContainer()
            {
                HttpServiceMock = new Mock<IHttpService>()
            };
        }
    }
}
