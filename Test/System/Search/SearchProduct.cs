using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Model;

namespace Test.System.Search
{
    public class SearchProduct : ISearchProduct
    {
        private readonly LoginContext _context;
        public static int Page_size { get; set; } = 5;

        public SearchProduct(LoginContext context)
        {
            _context = context;
        }
        public  List<ProductRequest> GetAll(string search, int page = 1)
        {
            var product = _context.ProductDetails
                .AsQueryable();
            product = product.Skip((page - 1) * Page_size).Take(Page_size);
            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.Cost.ToString().Contains(search));

            }
            var result = product.Select(hh => new ProductRequest
            {
               Id = hh.Id,
               Cost = hh.Cost,
               Name = hh.Name

            });
            return result.ToList();
        }
    }
}
