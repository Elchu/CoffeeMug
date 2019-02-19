using CoffeeMug.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMug.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProductsList();

        Product GetProductById(Guid id);

        Guid Add(Product product);

        void Update(Product productToUpdate, Product product);

        void Delete(Guid id);
    }
}
