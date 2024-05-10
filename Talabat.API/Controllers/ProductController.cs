using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.ApiResponse;
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
        private readonly IGenaricRepository<ProductBrand> _BrandRepo;
        private readonly IGenaricRepository<ProductCategory> _CategoryRepo;

        // to ust Automapper we inject The class That implement Imapper 
        public ProductController(
            IGenaricRepository<Product> ProductsRepo, 
            IGenaricRepository<ProductBrand> Brand,
            IGenaricRepository<ProductCategory> Category ,
            IMapper Mapper
            )
        {
            _productsRepo = ProductsRepo;
            _mapper = Mapper;
            _BrandRepo = Brand;
            _CategoryRepo = Category;
        }

        // Get All Products
        [HttpGet] //Get /Api/Product 
                  //Sort By Price Asc Get /Api/Product?sort=PriceAsc
                  //Sort By Price Desc Get /Api/Product?sort=PriceDesc
                  //Sort By Name Get /Api/Product?sort=Name
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(string sort)
        {
            // we make object Of the ProductWithBrandandCategory with  the parameter less constractor
            var Spec = new ProductWithBrandandCategory(sort);

            var Products = await _productsRepo.GetAllWithSpecAsync(Spec);

            if (Products is null)
                return NotFound(new ObjResponse(401, "Products Fetching Error"));

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
                return NotFound(new ObjResponse(401, "Product Not Found"));

            // to return the Mapping Product Dto
            return Ok(_mapper.Map<Product, ProductDto>(Product));
        }


        // Update Products By ID
        [HttpPut("{Id}")] //Get /Api/Product
        public async Task<ActionResult<AddProductDto>> UpdateProductById(int Id , AddProductDto UpdatedProduct)
        {
            // Find the Product with Id
            Product? existingProduct = await _productsRepo.GetByIdAsync(Id);
            if (existingProduct is null)
                return NotFound(new ObjResponse(401, "This Product May be Deleted"));

            var UpdatedFile = UpdatedProduct.Picture;
            var OrignalFileName = existingProduct.PictureUrl;

           var UpdatedUrl = DocumentSettings.UpdateFile(UpdatedFile , OrignalFileName , "Imgs" );

            // Map the Update Product to the existing Product
            Product? ProductUpdate = _mapper.Map(UpdatedProduct , existingProduct);

            ProductUpdate.PictureUrl = UpdatedUrl;

            if (ProductUpdate is null)
                return NotFound(new ObjResponse(401 , "Product Update Error"));

            await _productsRepo.UpdateAsync(ProductUpdate);

            // to return the Mapping Product Dto
            return Ok(new ObjResponse(200,"Product Updated Successfully"));
        }


        // Add Product 
        [HttpPost] //Get /Api/Product
        public async Task<ActionResult<AddProductDto>> AddProduct(AddProductDto NewProduct)
        {

            // The Property of the file Imahe
            var file = NewProduct.Picture;

            // Mapping the Dto to Product Entities
            Product? MappingProduct = _mapper.Map<AddProductDto, Product>(NewProduct);
            if (MappingProduct is null)
                return NotFound(new ObjResponse(401, "Error in Adding Product"));

            // If MappingProduct Is Not Null :
            // Add The Image Using the Static Member Method Class
            var Path = DocumentSettings.UploadFile(file, "Imgs");

            MappingProduct.PictureUrl = Path;

            await _productsRepo.AddAsync(MappingProduct);

            // to return the Mapping Product Dto
            return Ok(new ObjResponse(200, "The Product Added Successfully"));
        }


        // Delete Product By Id
        [HttpDelete("{Id}")] //Delete /Api/Product
        public async Task<ActionResult<AddProductDto>> DeletProduct(int Id)
        {
            // Find the product that we want to delete With Id 
            Product? Product = await _productsRepo.GetByIdAsync(Id);

            if (Product is null) return NotFound(new ObjResponse(401, "THis Product Not Found"));
            
            // Get the File Name
            string FileName = Product.PictureUrl;

            // Delete The Image form the WWRoot
            DocumentSettings.DeleteFile(FileName);

            // Delete Product
            await _productsRepo.DeleteAsync(Product);

            // to return the Mapping Product Dto
            return Ok(new ObjResponse(200, "The Product Deleted Successfully"));
        }


        //For Brands And Categories

        [HttpGet("Brands")] //Get /Api/Product/Brands
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _BrandRepo.GetAllAsync();
            return Ok(Brands);

        }


        [HttpGet("Categories")] //Get /Api/Product/Categories
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllCategories()
        {
            var Categories = await _CategoryRepo.GetAllAsync();
            return Ok(Categories);

        }


    }
}
