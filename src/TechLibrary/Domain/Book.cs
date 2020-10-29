using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TechLibrary.Domain
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string PublishedDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescr { get; set; }
        public string LongDescr { get; set; }
        [JsonIgnore]
        public int Deleted { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }

    }
}