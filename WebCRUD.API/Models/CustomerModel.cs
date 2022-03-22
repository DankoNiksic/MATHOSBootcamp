using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCRUD.API.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
    }
}