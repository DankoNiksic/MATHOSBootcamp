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
        public List<ProductModelEntity> GetAllProducts()
        {
            ProductRepository productRepository = new ProductRepository();
            var product =productRepository.GetAllProduct();
            return product;
        }
        public ProductModelEntity GetProductById(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            return productRepository.GetProductById(id);            
        }
        public ProductModelEntity GetProductByName(string name)
        {
            ProductRepository productRepository = new ProductRepository();
            return productRepository.GetProductByName(name);
        }
        public void CreateNewProduct(ProductModelEntity product)
        {
            ProductRepository productRepository = new ProductRepository();
             productRepository.CreateNewProduct(product);
            
        }
        public void CreateNewProducts(List<ProductModelEntity> product)
        {
            ProductRepository productRepository = new ProductRepository();
            productRepository.CreateNewProducts(product);

        }
        public void EditProduct(ProductModelEntity product)
        {
            ProductRepository productRepository = new ProductRepository();
            productRepository.EditProduct(product);

        }
        public void DeleteProduct(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            productRepository.DeleteProduct(id);

        }
        public bool CheckId(int id)
        {
            ProductRepository productRepository = new ProductRepository();
          return productRepository.CheckId(id);
        }
        

    }
}
