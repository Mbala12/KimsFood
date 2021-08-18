using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderItem.Models;

namespace OrderItem.Repositories
{
    public class CategoryRepository
    {
        private OnlineFoodEntities onFood;
        public CategoryRepository()
        {
            onFood = new OnlineFoodEntities();
        }

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in onFood.Categories
                                  select new SelectListItem
                                  {
                                      Text = obj.CategoryName,
                                      Value = obj.CategoryID.ToString(),
                                      Selected = true
                                  }).ToList();
            return objSelectListItems;

        }
    }
}