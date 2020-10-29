using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TechLibrary.Interfaces;

namespace TechLibrary.Contracts.Responses
{
    public class BookSearchResponse
    {
        string _amazonLink;
        DateTime? _PublishedDate = null;

        public BookSearchResponse()
        {
        }

        public BookSearchResponse(IAppConfig appConfig)
        {
            _amazonLink = appConfig.AmazonURL;
        }


        public int Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("isbn")]
        public string ISBN { get; set; }
        public string PublishedDateString { get; set; }
        public string FormattedPublishedDateString
        {
            get
            {
                if (PublishedDate != null)
                {
                    return _PublishedDate.Value.Date.ToString("MM/dd/yyyy");
                }
                return null;
            }
        }

        [NotMapped]
        public DateTime? PublishedDate 
        {
            get
            {
                if (_PublishedDate == null && !string.IsNullOrWhiteSpace( PublishedDateString))
                {
                    DateTime result;
                    if (DateTime.TryParse(PublishedDateString, out result))
                    {
                        _PublishedDate= result.Date;
                    }
                }
                return _PublishedDate;
            }
        }
        public string ThumbnailUrl { get; set; }
        public string ShortDescr { get; set; }

        public string AmazonLink
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_amazonLink) || string.IsNullOrWhiteSpace(ISBN))
                    return null;
                return _amazonLink + ISBN;
            }
        }



    }
}
