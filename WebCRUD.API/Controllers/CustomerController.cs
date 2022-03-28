using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebCRUD.API.Models;
using WebCRUD.Model;
using WebCRUD.Service;

namespace WebCRUD.API.Controllers
{
    public class CustomerController : ApiController
    {
        //Get all customers - api/customer/
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllCustomersAsync()
        {
            CustomerService customerService = new CustomerService();
            List<CustomerModelEntity> customers;
            List<CustomerRest> listcustomersrest = new List<CustomerRest>();

            customers = await customerService.GetAllCustomersAsync();

            foreach (var item in customers)
            {
                CustomerRest customerrest = new CustomerRest
                {
                    
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    Phone = item.Phone
                };
                listcustomersrest.Add(customerrest);
            }
            if (customers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, listcustomersrest);
        }

        //Get by Id - api/customer/
        [HttpGet, Route("api/customer/{id}")]
        public async Task<HttpResponseMessage> GetCustomerByIdAsync(int id)
        {

            CustomerService customerService = new CustomerService();
            CustomerModelEntity customers;
            if (!await (customerService.CheckIdAsync(id)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Id = {id} is not in database");
            }

            customers =await customerService.GetCustomerByIdAsync(id);

            CustomerRest customerrest = new CustomerRest
            {
                Name = customers.Name,
                Email=customers.Email,
                Address=customers.Address,
                Phone=customers.Phone
            };

            if (customers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, customerrest);
        }

        // GET api/values/Name
        [HttpGet, Route("api/customer/name/{name}")]
        public async Task<HttpResponseMessage> GetCustomerByNameAsync(string name)
        {
            CustomerService customerService = new CustomerService();
            CustomerModelEntity customers;

            customers =await customerService.GetCustomerByNameAsync(name);

            CustomerRest customerrest = new CustomerRest
            {
                Name = customers.Name,
                Email = customers.Email,
                Address = customers.Address,
                Phone = customers.Phone
            };

            if (customers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, customerrest);
        }

        //Post api/customer/
        [HttpPost]
        public async Task<HttpResponseMessage> CreateNewCustomerAsync(CustomerRest customer)
        {

            if (customer == null || customer.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Name is required");
            }
            CustomerModelEntity customerEntity = new CustomerModelEntity
            {
                Name = customer.Name,
                Email= customer.Email,
                Address= customer.Address,
                Phone= customer.Phone
            };

            CustomerService customerService = new CustomerService();
            try
            {
               await customerService.CreateNewCustomerAsync(customerEntity);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        //Post List of customers
        [HttpPost, Route("api/customer/create")]
        public async Task<HttpResponseMessage> CreateNewCustomersAsync(List<CustomerRest> customers)
        {
            List<CustomerModelEntity> customersEntity = new List<CustomerModelEntity>();
            foreach (var item in customers)
            {
                CustomerModelEntity model = new CustomerModelEntity
                {                    
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    Phone = item.Phone

                };
                customersEntity.Add(model);
            }
            CustomerService customerService = new CustomerService();
           await customerService.CreateNewCustomersAsync(customersEntity);
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }


        //Put by Id
        [HttpPut, Route("api/customer/{id}")]
        public async Task<HttpResponseMessage> EditCustomerAsync([FromUri] int id, CustomerRest customer)
        {
            CustomerService customerService = new CustomerService();

            if (customer == null || customer.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Name is required");
            }

           if (! await(customerService.CheckIdAsync(id)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Id = {id} is not in database");
            }

            CustomerModelEntity customerEntity = new CustomerModelEntity
            {
                Id = id,
                Name = customer.Name,
                Email=customer.Email,
                Address=customer.Address,
                Phone=customer.Phone
            };

            try
            {
               await customerService.EditCustomerAsync(customerEntity);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }
        //Delete by Id
        [HttpDelete, Route("api/customer/delete/{id}")]
        public async Task<HttpResponseMessage> DeleteCustomerAsync(int id)
        {
            CustomerService customerService = new CustomerService();
            if (! await( customerService.CheckIdAsync(id)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Id = {id} is not in database");
            }
            try
            {
              await customerService.DeleteCustomerAsync(id);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Customer is used by other table");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
    public class CustomerRest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
    }
}
