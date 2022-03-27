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
        List<ProductModelEntity> GetAllProduct();
        ProductModelEntity GetProductById(int id);
        ProductModelEntity GetProductByName(string name);
        void CreateNewProduct(ProductModelEntity product);
        void CreateNewProducts(List<ProductModelEntity> product);
        void EditProduct(ProductModelEntity product);
        void DeleteProduct(int id);
        bool CheckId(int id);
    }
}
