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
        public async Task<List<CustomerModelEntity>> GetAllCustomersAsync()
        {
            CustomerRepository customerRepository = new CustomerRepository();
            var customer =await customerRepository.GetAllCustomerAsync();
           return customer;
        }
        public async Task<CustomerModelEntity> GetCustomerByIdAsync(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            return await customerRepository.GetCustomerByIdAsync(id);
        }
        public async Task<CustomerModelEntity> GetCustomerByNameAsync(string name)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            return await customerRepository.GetCustomerByNameAsync(name);
        }
        public async Task CreateNewCustomerAsync(CustomerModelEntity customer)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            await customerRepository.CreateNewCustomerAsync(customer);

        }
        public async Task CreateNewCustomersAsync(List<CustomerModelEntity> customer)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            await customerRepository.CreateNewCustomersAsync(customer);

        }
        public async Task EditCustomerAsync(CustomerModelEntity customer)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            await customerRepository.EditCustomerAsync(customer);

        }
        public async Task DeleteCustomerAsync(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            await customerRepository.DeleteCustomerAsync(id);

        }
        public async Task<bool> CheckIdAsync(int id)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            return await customerRepository.CheckIdAsync(id);
        }

    }
}
