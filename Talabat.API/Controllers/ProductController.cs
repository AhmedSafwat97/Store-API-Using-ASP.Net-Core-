using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.Core.Enities;
using Talabat.Core.IReposities;
using Talabat.Core.ProductSpec;
using Talabat.Repository;

namespace Talabat.API.Controllers
{

    public class ProductController : BController
    {
        private readonly IGenaricRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        // to ust Automapper we inject The class That implement Imapper 
        public ProductController(IGenaricRepository<Product> ProductsRepo , IMapper Mapper)
        {
            _productsRepo = ProductsRepo;
            _mapper = Mapper;
        }

        // Get All Products
        [HttpGet] //Get /Api/Product
         public  async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            // we make object Of the ProductWithBrandandCategory with  the parameter less constractor
            var Spec = new ProductWithBrandandCategory();

            var Products = await _productsRepo.GetAllWithSpecAsync(Spec);

            if (Products is null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<Product> , IEnumerable<ProductDto>>(Products));
        }


        // Get Products By ID
        [HttpGet("{Id}")] //Get /Api/Product
        public async Task<ActionResult<ProductDto>> GetProductById(int Id)
        {
            // we make object Of the ProductWithBrandandCategory with  the parameter less constractor
            var Spec = new ProductWithBrandandCategory(Id);

            var Product = await _productsRepo.GetByIDWithSpecAsync(Spec);

            if (Product is null)
                return NotFound();

            // to return the Mapping Product Dto
            return Ok(_mapper.Map<Product , ProductDto>(Product));
        }

    }
}
