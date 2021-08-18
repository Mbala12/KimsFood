using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderItem.Models;

namespace OrderItem.Repositories
{
    public class PaymentTypeRepository
    {
        private OnlineFoodEntities onFood;
        public PaymentTypeRepository()
        {
            onFood = new OnlineFoodEntities();
        }

        public IEnumerable<SelectListItem> GetAllPaymentType()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in onFood.PaymentTypes
                                  select new SelectListItem
                                  {
                                      Text = obj.PaymentTypeName,
                                      Value = obj.PaymentTypeID.ToString(),
                                      Selected = true
                                  }).ToList();
            return objSelectListItems;

        }
    }
}