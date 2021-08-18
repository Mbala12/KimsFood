using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderItem.ViewModel
{
    public class OrderDetailViewModel
    {
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public decimal Total { get; set; }
    }
}