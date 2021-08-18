using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderItem.ViewModel
{
    public class OrderDetailsViewModel
    {
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public string OrderNumber { get; set; }
        
        public DateTime OrderDate { get; set; }

        public string DailyDate { get; set; }

        public string Image { get; set; }

        public string ItemName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public decimal Total { get; set; }
    }
}