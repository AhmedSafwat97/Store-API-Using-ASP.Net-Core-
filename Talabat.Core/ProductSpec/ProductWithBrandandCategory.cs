using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.EntityParams;

namespace Talabat.Core.ProductSpec
{
    // BaseSpacification i make this class to be the container of the Common Code
    public class ProductWithBrandandCategory : BaseSpacification<Product>
            
    {

        // this constractor is used to give the Value to the
        // include and make the criteria to be null
        public ProductWithBrandandCategory(ProductParams ProductParam) :base(

                // For Filter By Brand Or Category 
                // or Filter By Brand and Category 
                // if brand Comes with value => !brand.HasValue = False
                // if brand Comes without value => !brand.HasValue = True
                // if False => P.BrandId == brand 
                // if True => Not Apply the Conodition 
                P => 
                     (string.IsNullOrEmpty(ProductParam.Search) || P.Name.ToLower().Contains(ProductParam.Search) ) &&
                     (!ProductParam.brandId.HasValue || P.BrandId == ProductParam.brandId) && 
                     (!ProductParam.CategoryId.HasValue || P.CategoryId == ProductParam.CategoryId)
            )
        {

            //for The Include
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

            // For Sorting
            // The Condition That if the Sort is Send With Value
            if (!string.IsNullOrEmpty(ProductParam.sort))
            {

                 switch  (ProductParam.sort) {
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



            //Example 
            // We Have 100 Product in the database
            // The PageSizeLimit  Is 20 Product Per Page
            // The Page Count is !00 / 20 = 5 pages

            // i want to get the page Number 3 
            // then Skip = PageSize(20) * PageNumber(3) - 1 = 40  => Skip = 40

            ApplyPagination(ProductParam.PageSize * (ProductParam.PageIndex - 1), ProductParam.PageSize);


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
