using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Spacifications.EntityParams
{
    public class ProductParams : FilterSortPainationSpecParams
    {

        // For Filtration 
        public int? brandId { get; set; }

        public int? CategoryId { get; set; }

    }
}
