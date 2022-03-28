using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCRUD.Model;

namespace WebCRUD.Repository.Common
{
    public interface ICustomerRepository
    {
        Task<List<CustomerModelEntity>> GetAllCustomerAsync();
        Task<CustomerModelEntity> GetCustomerByIdAsync(int id);
        Task<CustomerModelEntity> GetCustomerByNameAsync(string name);
        Task CreateNewCustomerAsync(CustomerModelEntity customer);
        Task CreateNewCustomersAsync(List<CustomerModelEntity> customer);
        Task EditCustomerAsync(CustomerModelEntity customer);
        Task DeleteCustomerAsync(int id);
        Task<bool> CheckIdAsync(int id);
    }
}
