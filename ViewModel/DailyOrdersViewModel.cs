using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderItem.ViewModel
{
    public class DailyOrdersViewModel
    {
        public string DailyDate { get; set; }

        public string Image { get; set; }

        public string ItemName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal SumQuantity { get; set; }

        public decimal SumUnitPrice { get; set; }

        //public string FormattedDate(string format)
        //{
        //    return OrderDate.ToString(format);
        //}
    }
}