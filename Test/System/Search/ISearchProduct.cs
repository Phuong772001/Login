using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;

namespace Test.System.Search
{
    public interface ISearchProduct
    {
        List<ProductRequest> GetAll(string search, int page = 1);
    }
}
