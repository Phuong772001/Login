using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Model;

namespace Test.System.Product
{
    public class ProductService : IProductService
    {
        private readonly LoginContext _context;
        public ProductService(LoginContext context)
        {
            _context = context;
        }
        public async Task<ProductDetail> AddProduct(ProductRequest employee)
        {
            var product = new ProductDetail()
            {
                Cost = employee.Cost,
                Name = employee.Name,
            };
             _context.ProductDetails.Add(product);
            await _context.SaveChangesAsync();
            return product;

            //_context.Products.Add(employee);
            //await _context.SaveChangesAsync();
            //return employee;
        }

        public async Task<List<ProductDetail>> GetProducts()
        {
            return await _context.ProductDetails.OrderBy(a => a.Name).ToListAsync();
        }

        public void UpdateProduct(ProductRequest employee)
        {
            var product = _context.ProductDetails.Find(employee.Id);
            if (product == null)
            {
                throw new("Không tìm thấy ");
            }

            product.Cost = employee.Cost;
            product.Name = employee.Name;
            //_context.Products.Update(employee);
            //_context.SaveChanges();
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.ProductDetails.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _context.Remove(product);
                _context.SaveChanges();
            }
        }

        public ProductDetail GetProduct(int id)
        {
            return _context.ProductDetails.FirstOrDefault(x => x.Id == id);
        }
    }
}
