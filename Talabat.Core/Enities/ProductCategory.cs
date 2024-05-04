using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Enities
{
    public class ProductCategory : BaseEntity
    {

        public string Name { get; set; }


        /// Navigational Propert pf (Many)
        /// But i wil do this usin fluint API
        ///public ICollection<Product> Products { get; set; } = new HashSet<Product>();


    }

}
