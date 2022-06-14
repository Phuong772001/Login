using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Model;
using Test.System.Product;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("api/Product/GetProducts")]
        public  Task<List<ProductDetail>> GetProducts()
        {
            return _productService.GetProducts();
        }
        [HttpPost]
        [Route("api/Product/AddProduct")]
        public IActionResult AddProduct([FromForm] ProductRequest product)
        {
            _productService.AddProduct(product);
            return Ok();
        }
        [HttpPut]
        [Route("api/Product/UpdateProduct")]
        public IActionResult UpdateProduct([FromForm] ProductRequest product)
        {
            _productService.UpdateProduct(product);
            return Ok();
        }
        [HttpDelete]
        [Route("api/Product/DeleteProduct")]
        public IActionResult DeleteProduct([FromForm] int id)
        {
            var existingProduct = _productService.GetProduct(id);
            if (existingProduct != null)
            {
                _productService.DeleteProduct(existingProduct.Id);
                return Ok();
            }
            return NotFound($"Product Not Found with ID : {existingProduct.Id}");
        }

        [HttpGet]
        [Route("GetProduct")]
        public ProductDetail GetProduct([FromQuery] int id)
        {
            return _productService.GetProduct(id);
        }
    }
}
