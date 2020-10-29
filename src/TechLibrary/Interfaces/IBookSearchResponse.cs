using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechLibrary.Interfaces
{

    public interface IBookSearchResponse
    {
        int? Id { get; set; }

        string Title { get; set; }
        string ISBN { get; set; }

        string ShortDescription { get; set; }

        string PublishedDate { get; set; }
        string ThumbnailUrl { get; set; }

        string AmazonLink { get; }

    }

}
