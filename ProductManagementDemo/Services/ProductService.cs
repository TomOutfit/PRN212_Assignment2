using BusinessObjects;
using System.Collections.Generic;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public void SaveProduct(Product p)
        {
            _productRepository.SaveProduct(p);
        }

        public void UpdateProduct(Product p)
        {
            _productRepository.UpdateProduct(p);
        }

        public void DeleteProduct(Product p)
        {
            _productRepository.DeleteProduct(p);
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }
    }
}
