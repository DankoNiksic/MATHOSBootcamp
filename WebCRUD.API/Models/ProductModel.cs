﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCRUD.API.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}