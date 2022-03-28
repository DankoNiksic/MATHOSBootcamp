using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCRUD.Model;

namespace WebCRUD.Repository.Common
{
    public interface IProductRepository
    {
        Task<List<ProductModelEntity>> GetAllProductAsync();
        Task<ProductModelEntity> GetProductByIdAsync(int id);
        Task<ProductModelEntity> GetProductByNameAsync(string name);
        Task CreateNewProductAsync(ProductModelEntity product);
        Task CreateNewProductsAsync(List<ProductModelEntity> product);
        Task EditProductAsync(ProductModelEntity product);
        Task DeleteProductAsync(int id);
        Task<bool> CheckIdAsync(int id);
    }
}
