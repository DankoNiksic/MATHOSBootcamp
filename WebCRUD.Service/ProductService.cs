using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCRUD.Model;
using WebCRUD.Repository;
using WebCRUD.Service.Common;

namespace WebCRUD.Service
{
    public class ProductService:IProductService
    {
        public async Task<List<ProductModelEntity>> GetAllProductsAsync()
        {
            ProductRepository productRepository = new ProductRepository();
            var product = await productRepository.GetAllProductAsync();
            return product;
        }
        public async Task<ProductModelEntity> GetProductByIdAsync(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            return await productRepository.GetProductByIdAsync(id);
        }
        public async Task<ProductModelEntity> GetProductByNameAsync(string name)
        {
            ProductRepository productRepository = new ProductRepository();
            return await productRepository.GetProductByNameAsync(name);
        }
        public async Task CreateNewProductAsync(ProductModelEntity product)
        {
            ProductRepository productRepository = new ProductRepository();
            await productRepository.CreateNewProductAsync(product);

        }
        public async Task CreateNewProductsAsync(List<ProductModelEntity> product)
        {
            ProductRepository productRepository = new ProductRepository();
            await productRepository.CreateNewProductsAsync(product);

        }
        public async Task EditProductAsync(ProductModelEntity product)
        {
            ProductRepository productRepository = new ProductRepository();
            await productRepository.EditProductAsync(product);

        }
        public async Task DeleteProductAsync(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            await productRepository.DeleteProductAsync(id);

        }
        public async Task<bool> CheckIdAsync(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            return await productRepository.CheckIdAsync(id);
        }

    }
}
