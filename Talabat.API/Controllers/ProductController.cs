using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Helper;
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
        public ProductController(IGenaricRepository<Product> ProductsRepo, IMapper Mapper)
        {
            _productsRepo = ProductsRepo;
            _mapper = Mapper;
        }

        // Get All Products
        [HttpGet] //Get /Api/Product
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            // we make object Of the ProductWithBrandandCategory with  the parameter less constractor
            var Spec = new ProductWithBrandandCategory();

            var Products = await _productsRepo.GetAllWithSpecAsync(Spec);

            if (Products is null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products));
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
            return Ok(_mapper.Map<Product, ProductDto>(Product));
        }


        // Add Product 
        [HttpPost] //Get /Api/Product
        public async Task<ActionResult<AddProductDto>> AddProduct(AddProductDto NewProduct)
        {

            // The Property of the file Imahe
            var file = NewProduct.Picture;

            // Use Static Mehod from Static class

            var MappingProduct = _mapper.Map<AddProductDto, Product>(NewProduct);
            if (MappingProduct is null)
                return NotFound();

            var Path = DocumentSettings.UploadFile(file, "Images");

            MappingProduct.PictureUrl = Path;

            await _productsRepo.AddAsync(MappingProduct);

            // to return the Mapping Product Dto
            return Ok("The Product Added Successfully");
        }


        // Delete Product 
        [HttpDelete("{Id}")] //Get /Api/Product
        public async Task<ActionResult<AddProductDto>> DeletProduct(int Id)
        {
            // Find the product that we want to delete With Id 
            Product? Product = await _productsRepo.GetByIdAsync(Id);

            if (Product is null) return NotFound();
            
            // Get the File Name
            string FileName = Product.PictureUrl;

            // Delete Product
            await _productsRepo.DeleteAsync(Product);

            // Delete The Image form the WWRoot
            DocumentSettings.DeleteFile(FileName, "Images");

            // to return the Mapping Product Dto
            return Ok("The Product Deleted Successfully");
        }


    }
}
