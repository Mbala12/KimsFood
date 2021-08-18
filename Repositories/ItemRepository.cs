using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderItem.Models;

namespace OrderItem.Repositories
{
    public class ItemRepository
    {
        private OnlineFoodEntities onFood;

        public ItemRepository()
        {
            onFood = new OnlineFoodEntities();
        }

        public IEnumerable<SelectListItem> GetAllItems()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in onFood.Items where obj.InStock == true
                                  //join objCat in onFood.Categories on obj.CategoryID equals objCat.CategoryID
                                  select new SelectListItem
                                  {
                                      Text = obj.ItemName,
                                      Value = obj.ItemID.ToString(),
                                      Selected = false
                                  }).ToList();
            return objSelectListItems;

        }
    }
}