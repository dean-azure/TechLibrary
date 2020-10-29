using System.Collections.Generic;
using TechLibrary.Domain;

namespace TechLibrary.Interfaces
{
    public interface IBook
    {
        int? Id { get; set; }

        string Title { get; set; }
        string ISBN { get; set; }

        string LongDescription { get; set; }
        string ShortDescription { get; set; }

        string PublishedDate { get; set; }
        string ThumbnailUrl { get; set; }

        ICollection<BookCategory> BookCategories { get; set; }
    }


}