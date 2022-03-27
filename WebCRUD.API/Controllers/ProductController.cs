using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCRUD.Model;
using WebCRUD.Service;

namespace WebCRUD.API.Controllers
{
    public class ProductController : ApiController
    {

        //Get all products - api/product/
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            ProductService productService = new ProductService();
            List<ProductModelEntity> products;
            List<ProductRest> listproductsrest = new List<ProductRest>();
                        
            products = productService.GetAllProducts();

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
        public HttpResponseMessage GetProductById(int id)
        {
            ProductService productService = new ProductService();
            ProductModelEntity products;

            products = productService.GetProductById(id);

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
        public HttpResponseMessage GetProductByName(string name)
        {
            ProductService productService = new ProductService();
            ProductModelEntity products;

            products = productService.GetProductByName(name);

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
        public HttpResponseMessage CreateNewProduct(ProductRest product)
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
                productService.CreateNewProduct(productEntity);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        //Post List of products
        [HttpPost, Route("api/product/create")]
        public HttpResponseMessage CreateNewProducts(List<ProductRest> products)
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
            productService.CreateNewProducts(productsEntity);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }


        //Put by Id
        [HttpPut, Route("api/product/{id}")]
        public HttpResponseMessage EditProduct([FromUri] int id, ProductRest product)
        {
            ProductService productService = new ProductService();

            if (product == null || product.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Name is required");
            }

            if (!productService.CheckId(id))
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
                productService.EditProduct(productEntity);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
        //Delete by Id
        [HttpDelete, Route("api/product/delete/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            ProductService productService = new ProductService();
            if (!productService.CheckId(id))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Id = {id} is not in database");
            }
            try
            {
                productService.DeleteProduct(id);
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
