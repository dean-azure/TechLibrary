using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechLibrary.Domain
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string CategoryName {get;set;}
        [JsonIgnore]
        public int Deleted { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }

    }
}
