using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderItem.ViewModel
{
    public class ItemViewModel
    {
        public int ItemID { get; set; }

        public int CategoryID { get; set; }

        [Display(Name ="Item Name")]
        public string ItemName { get; set; }

        [Display(Name ="Item Path")]
        public HttpPostedFileBase ImagePath { get; set; }

        [Display(Name ="Image Price")]
        public decimal ItemPrice { get; set; }
    }
}