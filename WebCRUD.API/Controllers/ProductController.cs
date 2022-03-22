using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCRUD.API.Models;

namespace WebCRUD.API.Controllers
{
    public class ProductController : ApiController
    {
        public static List<ProductModel> listProducts = new List<ProductModel>();

        //Get all products - api/product/
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            if (listProducts.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Products not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, listProducts);
        }

        //Get by Id - api/product/
        [HttpGet,Route("api/product/{id}")]
        public HttpResponseMessage GetAllProducts(int id)
        {
            var products = listProducts.Find(p => p.Id == id);
            if (products == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Product with id= {id} not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        // GET api/values/Name
        [HttpGet, Route("api/product/name/{name}")]
        public HttpResponseMessage GetProductByName(string name)
        {
            var customer = listProducts.FindAll(s => (s.Name).ToLower() == name.ToLower());

            if (customer.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Product {name} not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }
        //Post api/product/
        [HttpPost]
        public HttpResponseMessage CreateNewProduct(ProductModel product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Try again");
            }

            listProducts.Add(product);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        //Post List of product
        [HttpPost, Route("api/product/create")]
        public HttpResponseMessage CreateNewProducts(List<ProductModel> products)
        {
            if (products == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Try again");
            }

            foreach (var item in products)
            {
                if (listProducts.Count == 0)
                {
                    item.Id = 1;
                }
                else
                {
                    item.Id = listProducts.Last().Id + 1;
                }
                listProducts.Add(item);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //Put by Id
        [HttpPut]
        public HttpResponseMessage EditProduct(ProductModel product)
        {
            var productFromList = listProducts.Find(x => x.Id == product.Id);
            if (productFromList == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Try again");
            }
            productFromList.Id = product.Id;
            productFromList.Name = product.Name;
            productFromList.Price = product.Price;
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        //Delete by Id
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(int id)
        {
            var productFromList = listProducts.Find(x => x.Id == id);
            if (productFromList == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Product with id= {id} not found");
            }
            listProducts.Remove(productFromList);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
