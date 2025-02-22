﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagement.Models
{
    public class Order
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ItemID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public int OrderStatusID { get; set; }
        public string Reason { get; set; }
    }
}