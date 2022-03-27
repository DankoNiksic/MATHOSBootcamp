using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCRUD.Model;

namespace WebCRUD.Service.Common
{
    public interface ICustomerService
    {
        List<CustomerModelEntity> GetAllCustomers();
        CustomerModelEntity GetCustomerById(int id);
        CustomerModelEntity GetCustomerByName(string name);
        void CreateNewCustomer(CustomerModelEntity customer);
        void CreateNewCustomers(List<CustomerModelEntity> customer);
        void EditCustomer(CustomerModelEntity customer);
        void DeleteCustomer(int id);
        bool CheckId(int id);
    }
}
