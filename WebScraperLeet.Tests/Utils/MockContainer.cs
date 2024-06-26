﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraperLeet.FileService;
using WebScraperLeet.HttpService;

namespace WebScraperLeet.Tests.Utils
{
    public class MockContainer
    {
        public Mock<IHttpService>? HttpServiceMock { get; set; }
        public Mock<IFileService>? FileServiceMock { get; set; }
    }
}
