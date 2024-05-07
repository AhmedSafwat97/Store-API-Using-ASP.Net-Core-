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
        public ProductWithBrandandCategory():base()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
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
