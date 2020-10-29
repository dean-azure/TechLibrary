using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLibrary.Interfaces;

namespace TechLibrary.Contracts.Responses
{
    public class CategorySearchResponse
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }

        /// <summary>
        /// Stores the number of books referencing this category
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// When true, selected for filtering records
        /// </summary>
        public bool Selected { get; set; }
    }
}
