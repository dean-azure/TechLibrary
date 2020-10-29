using System;
using System.Collections.Generic;
using System.Text;
using TechLibrary.Config;
using TechLibrary.Interfaces;

namespace TechLibrary.Test.Common
{
    public class MockAppConfig : IAppConfig
    {

        public static IAppConfig CreateMockAppConfig()
        {
            return new MockAppConfig()
            {
                AmazonURL = "https://www.amazon.com/s?k=",
                DefaultRecordsPerPage = 10
            };

        }

        public MockAppConfig() { }

        public string AmazonURL { get; set; }
        public int DefaultRecordsPerPage { get; set; }
    }
}
