using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechLibrary.Domain;
using TechLibrary.Interfaces;

namespace TechLibrary.Contracts.Responses
{
    /// <summary>
    /// Returns a single Book object that may be viewed or edited
    /// Also, returns the result after an update
    /// </summary>
    public class BookEditResponse 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("isbn")]
        public string ISBN { get; set; }
        public string PublishedDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescr { get; set; }
        public string LongDescr { get; set; }

        public Category[] Categories { get; set; }
        
        public string ErrorMessage { get; internal set; }
    }
}
