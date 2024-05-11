using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Spacifications
{
    public class FilterSortPainationSpecParams
    {
        public string? sort { get; set; }

        // Set the Limit Of the Sequance Per Page
        public const int MaxPageSize = 20;

        //For Search 
        private string? search ;

        public string? Search
        {
            get { return search; }
            // Set The Search Value to be lower Case
            set { search = value?.ToLower(); }
        }


        // MAke Full Property to Edit the Set Function
        private int pagesize = 10 ;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > MaxPageSize ? MaxPageSize : value; }
        }

        // The Number Of Page 
        public int PageIndex { get; set; } = 1;


    }
}
