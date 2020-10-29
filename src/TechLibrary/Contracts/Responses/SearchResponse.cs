using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TechLibrary.Interfaces;

namespace TechLibrary.Contracts.Responses
{
    public class SearchResponse
    {
        int _page = 1;
        int _recordsPerPage = 10;


        public string SearchString { get; set; } = "";

        public int Page
        {
            get { return _page; }
            set
            {
                if (value < 1)
                    value = 1;
                _page = value;
            }
        }
        public int Pages
        {
            get
            {
                try
                {
                    return (int)Math.Ceiling((double)RecordCount / (double)RecordsPerPage);
                }
                catch (DivideByZeroException)
                {
                    return 1;
                }
            }
        }

        public int[] PageNumbers 
        {
            get
            {
                List<int> pageNumbers = new List<int>(new int[] { 1, Page, Pages });
                if (Page <= 7)
                {
                    pageNumbers.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7});
                }
                else if (Pages > Pages - 7)
                {
                    pageNumbers.AddRange(new int[] {Pages - 1, Pages - 2, Pages - 3, Pages - 4, Pages- 5, Pages - 6});
                }
                for (int i = 5; i < Pages; i = i + 5)
                {
                    pageNumbers.Add(i);
                }

                for (int i = Page - 3; i < Page + 3; i++)
                {
                    pageNumbers.Add(i);
                }

                pageNumbers.Add(Page);

                pageNumbers.Sort();
                return pageNumbers.Where(x => x <= Pages && x >= 1).Distinct().ToArray();
            }
        }

        public int RecordCount { get; set; }
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

        public int Skip
        {
            get
            {
                return (Page - 1) * RecordsPerPage;
            }

        }




        public CategorySearchResponse[] Categories { get; set; }
        public BookSearchResponse[] Books { get; set; }


    }
}

