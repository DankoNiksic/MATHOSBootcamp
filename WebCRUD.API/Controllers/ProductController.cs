using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebCRUD.Model;
using WebCRUD.Service;

namespace WebCRUD.API.Controllers
{
    public class ProductController : ApiController
    {

        //Get all products - api/product/
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllProductsAsync()
        {
            ProductService productService = new ProductService();
            List<ProductModelEntity> products;
            List<ProductRest> listproductsrest = new List<ProductRest>();
                        
            products = await productService.GetAllProductsAsync();

            foreach (var item in products)
            {
                ProductRest productrest = new ProductRest
                {
                    Name = item.Name,
                    Price = item.Price
                };
                listproductsrest.Add(productrest);
            }
            if (products == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Products not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, listproductsrest);
        }

        //Get by Id - api/product/
        [HttpGet, Route("api/product/{id}")]
        public async Task<HttpResponseMessage> GetProductByIdAsync(int id)
        {
            ProductService productService = new ProductService();
            ProductModelEntity products;

            products =await productService.GetProductByIdAsync(id);

            ProductRest productrest = new ProductRest
            {
                Name = products.Name,
                Price = products.Price
            };

            if (products == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Products not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, productrest);
        }

        // GET api/values/Name
        [HttpGet, Route("api/product/name/{name}")]
        public async Task<HttpResponseMessage> GetProductByNameAsync(string name)
        {
            ProductService productService = new ProductService();
            ProductModelEntity products;

            products =await productService.GetProductByNameAsync(name);

            ProductRest productrest = new ProductRest
            {
                Name = products.Name,
                Price = products.Price
            };

            if (products == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Products not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, productrest);
        }

        //Post api/product/
        [HttpPost]
        public async Task<HttpResponseMessage> CreateNewProductAsync(ProductRest product)
        {

            if (product == null || product.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Name is required");
            }
            ProductService productService = new ProductService();

            ProductModelEntity productEntity = new ProductModelEntity
            {
                Name = product.Name,
                Price = product.Price
            };
            try
            {
               await productService.CreateNewProductAsync(productEntity);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        //Post List of products
        [HttpPost, Route("api/product/create")]
        public async Task<HttpResponseMessage> CreateNewProductsAsync(List<ProductRest> products)
        {
            List<ProductModelEntity> productsEntity = new List<ProductModelEntity>();
            foreach (var item in products)
            {
                ProductModelEntity model = new ProductModelEntity
                {
                    Name = item.Name,
                    Price = item.Price

                };
                productsEntity.Add(model);
            }
            ProductService productService = new ProductService();
           await productService.CreateNewProductsAsync(productsEntity);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }


        //Put by Id
        [HttpPut, Route("api/product/{id}")]
        public async Task<HttpResponseMessage> EditProduct([FromUri] int id, ProductRest product)
        {
            ProductService productService = new ProductService();

            if (product == null || product.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Name is required");
            }

            if (! await( productService.CheckIdAsync(id)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Id = {id} is not in database");
            }

            ProductModelEntity productEntity = new ProductModelEntity
            {
                Id = id,
                Name = product.Name,
                Price = product.Price
            };

            try
            {
               await productService.EditProductAsync(productEntity);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
        //Delete by Id
        [HttpDelete, Route("api/product/delete/{id}")]
        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            ProductService productService = new ProductService();
            if (! await( productService.CheckIdAsync(id)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Id = {id} is not in database");
            }
            try
            {
               await productService.DeleteProductAsync(id);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Product is used by other table");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
    public class ProductRest
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

}
