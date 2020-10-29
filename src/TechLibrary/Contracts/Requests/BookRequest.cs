using Newtonsoft.Json;

namespace TechLibrary.Contracts.Requests
{
    public class BookRequest 
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("isbn")]
        public string ISBN { get; set; }
        public string LongDescr { get; set; }
        public string ShortDescr { get; set; }
        public string PublishedDate { get; set; }
        public string ThumbnailUrl { get; set; }

        public int[] Categories { get; set; }
    }
}
