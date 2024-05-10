using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;
using Talabat.Core.Spacifications;

namespace Talabat.Core.ProductSpec
{
    // BaseSpacification i make this class to be the container of the Common Code
    public class ProductWithBrandandCategory : BaseSpacification<Product>
            
    {

        // this constractor is used to give the Value to the
        // include and make the criteria to be null
        public ProductWithBrandandCategory(string Sort):base()
        {
            //for The Include
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

            // For Sorting
            // The Condition That if the Sort is Send With Value
            if (!string.IsNullOrEmpty(Sort))
            {
                 switch  (Sort) {
                    case "PriceAsc":
                        // Set the Value to the property
                        OrderBy = P => P.Price;
                        break;
                    case "PriceDesc":
                        OrderByDesc = P => P.Price;
                        break;
                    default:
                        OrderBy = P => P.Name;
                        break;

                }



            }
            else OrderBy = P => P.Name;

        }

        // the constractor that make Query that get the Product By Id
        // we send the lamda exprition to the Base Constractor of the parant
        public ProductWithBrandandCategory(int Id):base(P => P.Id == Id) 
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }


    }
}
