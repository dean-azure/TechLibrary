using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLibrary.Contracts.Responses;
using TechLibrary.Interfaces;

namespace TechLibrary.Contracts.Requests
{
    public class SearchRequest
    {
        int _page = 1;
        int _recordsPerPage = 10;

        public string SearchString { get; set; }
        public CategorySearchResponse[] Categories { get; set; } = new CategorySearchResponse[0];

        public int Page 
        {
            get 
            {
                if (NewSearch)
                    return 1;
                return _page; 
            }
            set 
            {
                if (value < 1)
                    value = 1;
                _page = value; 
            }
        }
        public int RecordCount { get; internal set; }
        public int RecordsPerPage 
        {
            get { return _recordsPerPage; }
            set
            {
                if (value < 10)
                    value = 10;
                _recordsPerPage = value;
            }
        }

        public int Pages
        {
            get
            {
                try
                {
                    var result = (int)Math.Ceiling((double)RecordCount / (double)RecordsPerPage);

                    if (result < 0)
                        return 0;

                    return result;
                }
                catch (DivideByZeroException)
                {
                    return 0;
                }
            }

        }

        public int Skip
        {
            get
            {
                return (Page - 1) * RecordsPerPage;
            }

        }

        public bool NewSearch { get; set; }
    }
}
