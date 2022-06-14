using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Model;

namespace Test.System.Product
{
    public interface IProductService
    {
        Task<ProductDetail> AddProduct(ProductRequest employee);
        Task<List<ProductDetail>> GetProducts();
        void UpdateProduct(ProductRequest employee);
        void DeleteProduct(int id);
        ProductDetail GetProduct(int id);
    }
}
