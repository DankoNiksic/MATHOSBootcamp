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
    public class CustomerService: ICustomerService
    {
        public List<CustomerModelEntity> GetAllCustomers()
        {
            CustomerRepository customerRepository = new CustomerRepository();
            var customer = customerRepository.GetAllCustomer();
            return customer;
        }
        public CustomerModelEntity GetCustomerById(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            return customerRepository.GetCustomerById(id);
        }
        public CustomerModelEntity GetCustomerByName(string name)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            return customerRepository.GetCustomerByName(name);
        }
        public void CreateNewCustomer(CustomerModelEntity customer)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.CreateNewCustomer(customer);

        }
        public void CreateNewCustomers(List<CustomerModelEntity> customer)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.CreateNewCustomers(customer);

        }
        public void EditCustomer(CustomerModelEntity customer)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.EditCustomer(customer);

        }
        public void DeleteCustomer(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.DeleteCustomer(id);

        }
        public bool CheckId(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            return customerRepository.CheckId(id);
        }

    }
}
