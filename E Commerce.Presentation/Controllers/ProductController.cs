using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        //GET All Products
        [HttpGet]
        //Get:BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {


            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }





        //GET Product By Id
        [HttpGet("{id}")]
        //Get:BaseUrl/api/Products/1

        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }


        //GET All Types
        //Get:BaseUrl/api/Products/types
        [HttpGet("types")]

        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }



        //GET All Brands
        //Get:BaseUrl/api/Products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }


    }
}
