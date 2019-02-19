using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMug.Models;

namespace CoffeeMug.Services
{
    public class ProductRepository : IProductRepository
    {
        readonly DatabaseContext _databaseContext;

        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Guid Add(Product product)
        {
            _databaseContext.Add(product);
            _databaseContext.SaveChanges();

            return product.ProductId;
        }

        public void Delete(Guid id)
        {
            var product = GetProductById(id);

            _databaseContext.Remove(product);
            _databaseContext.SaveChanges();
        }

        public void Update(Product productToUpdate, Product product)
        {
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;

            _databaseContext.Update(productToUpdate);
            _databaseContext.SaveChanges();
        }

        public Product GetProductById(Guid id)
            => _databaseContext.Products.FirstOrDefault(p => p.ProductId.Equals(id));

        public IEnumerable<Product> GetProductsList()
            => _databaseContext.Products.ToList();

    }
}
