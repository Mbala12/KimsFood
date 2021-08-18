using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderItem.ViewModel
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderNumber { get; set; }

        public int PaymentTypeID { get; set; }

        public string PaymentName { get; set; }

        public decimal FinalTotal { get; set; }

        public int Time { get; set; }

        public decimal Cost { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string DailyDate { get; set; }

        public IEnumerable<OrderDetailViewModel> ListOfOrderDetailViewModel { get; set; }
    }
}