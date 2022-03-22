using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCRUD.API.Models;

namespace WebCRUD.API.Controllers
{
    public class CustomerController : ApiController
    {

        public static List<CustomerModel> listCustomers = new List<CustomerModel>();

        [HttpGet]
        [Route("api/customer/insert")]
        public void InsertDataToList()
        {
            listCustomers.Add(new CustomerModel { Id = 1, Name = "Danko", Lastname = "Nikšić" });
            listCustomers.Add(new CustomerModel { Id = 2, Name = "Dino", Lastname = "Nikšić" });
            listCustomers.Add(new CustomerModel { Id = 3, Name = "Dragan", Lastname = "Bogdanović" });
        }
        // GET api/values
        [HttpGet]
        public HttpResponseMessage GetAllCustomers()
        {
            if (listCustomers.Count == 0)
            { return Request.CreateResponse(HttpStatusCode.BadRequest, "No customers."); }

            return Request.CreateResponse(HttpStatusCode.OK, listCustomers);

        }

        // GET api/values/5
        [HttpGet]
        public HttpResponseMessage GetCustomerById(int id)
        {
            var customer = listCustomers.Find(s => s.Id == id);

            if (customer == null)
            {return Request.CreateResponse(HttpStatusCode.BadRequest, $"Customer {id} not found"); }

            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        // POST api/values
        [HttpPost]
        public void CreateNewCustomer(CustomerModel customer)
        {
            if (customer == null)
            { return; }

            if (listCustomers.Count == 0)
            { customer.Id = 1; }
            else
            { customer.Id = listCustomers.Last().Id + 1; }

            listCustomers.Add(customer);
        }
        // POST api/values
        [HttpPost]
        [Route("api/customer/create")]
        public void CreateNewCustomers(List<CustomerModel> customers)
        {
            if (customers == null)
            { return; }

            foreach (var item in customers)
            {
                if (listCustomers.Count == 0)
                { item.Id = 1; }
                else
                { item.Id = listCustomers.Last().Id + 1; }

                listCustomers.Add(item);
            }
        }

        // PUT api/values
        [HttpPut]
        public HttpResponseMessage EditCustomerById(CustomerModel customer)
        {
            var customerFromList = listCustomers.Find(s => s.Id == customer.Id);
            
            if (customerFromList == null)
            { return Request.CreateResponse(HttpStatusCode.BadRequest, $"Customer {customer.Id} not found"); }

            customerFromList.Name = customer.Name;
            customerFromList.Lastname = customer.Lastname;

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/values/delete/3
        [HttpDelete]
        [Route("api/customer/delete/{id}")]
        public HttpResponseMessage DeleteCustomerById(int id)
        {
            var customerFromList = listCustomers.Find(s => s.Id == id);

            if (customerFromList == null)
            { Request.CreateResponse(HttpStatusCode.BadRequest, $"Customer {id} not found"); }

            listCustomers.Remove(customerFromList);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
