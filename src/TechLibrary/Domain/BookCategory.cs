using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechLibrary.Domain
{
    public class BookCategory
    {
        public int? CategoryId { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }

    }
}
