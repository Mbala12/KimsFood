using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderItem.ViewModel
{
    public class ItemDetailViewModel
    {
        public int ItemID { get; set; }

        public int CategoryID { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Image")]
        public string Image { get; set; }

        [Display(Name = "Image Price")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public bool InStock { get; set; }
    }
}