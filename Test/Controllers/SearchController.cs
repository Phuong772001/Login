using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.System.Search;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchProduct _searchProduct;

        public SearchController(ISearchProduct searchProduct)
        {
            _searchProduct = searchProduct;
        }
        [HttpGet]
        public IActionResult GetAllProduct(string search, int page = 1)
        {
            try
            {
                var result = _searchProduct.GetAll(search, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest("We can't get the product");
            }
        }
    }
}
