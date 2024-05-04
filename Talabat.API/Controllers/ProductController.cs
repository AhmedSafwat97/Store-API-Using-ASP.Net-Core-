using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Enities;
using Talabat.Core.IReposities;
using Talabat.Repository;

namespace Talabat.API.Controllers
{

    public class ProductController : BController
    {
        private readonly IGenaricRepository<Product> _productsRepo;

        ProductController(IGenaricRepository<Product> ProductsRepo)
        {
            _productsRepo = ProductsRepo;
        }

        [HttpGet] //Get  /Api/Product
         public ActionResult<IEnumerable<Product>>

    }
}
