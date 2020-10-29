using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLibrary.Interfaces;

namespace TechLibrary.Config
{
    public class AppConfig : IAppConfig
    {

        //private readonly IConfiguration _configuration;

        const string ConfigSection = "AppConfig";

        public AppConfig(IConfiguration configuration)
        {
            var section = configuration.GetSection(ConfigSection);
            DefaultRecordsPerPage = section.GetValue<int>("DefaultRecordsPerPage");
            AmazonURL = section.GetValue<string>("AmazonURL");
        }

        public string AmazonURL { get; set; }
        public int DefaultRecordsPerPage { get; set; }
    }
}
