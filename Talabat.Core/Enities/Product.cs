using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Enities
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        //[ForeignKey(nameof(Product.Brand)] i do not have to make this data
        //anotaion beacuse i named the Prop With the Table that the id come from "ProductBrandId"
        // or Change Naming and use fluint Api to configer that tis property is forgin key 
        public int BrandId { get; set; } // This the foign key from productBrand

        public int CategoryId { get; set; } // This the foign key from productBrand


        public ProductBrand Brand { get; set; } // Navigational Property For Brand Model  (One)

        public ProductCategory Category { get; set; } // Navigational Property For Category Model (One)


    }
}
