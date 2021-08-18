using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderItem.Repositories;
using OrderItem.Models;
using OrderItem.ViewModel;

namespace OrderItem.Controllers
{
    public class OrderDetailController : Controller
    {
        private OnlineFoodEntities onFood;
        public OrderDetailController()
        {
            onFood = new OnlineFoodEntities();
        }
        // GET: OrderDetail
        public ActionResult Index()
        {
            CategoryRepository objCategoryRepository = new CategoryRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCategoryRepository.GetAllCategories(), objPaymentTypeRepository.GetAllPaymentType(), objItemRepository.GetAllItems());
            return View(objMultipleModels);
        }

        public PartialViewResult GetAllOrderDetails(string reference)
        {
            IEnumerable<OrderDetailsViewModel> listOfAllOrderDetails = (
                from objOrderDetails in onFood.OrderDetails
                join objOrder in onFood.Orders on objOrderDetails.OrderID equals objOrder.OrderID
                join objItem in onFood.Items on objOrderDetails.ItemID equals objItem.ItemID
                //where objOrder.OrderNumber == reference
                select new OrderDetailsViewModel()
                {
                    ItemID = objOrderDetails.ItemID,
                    OrderNumber = objOrder.OrderNumber,
                    ItemName = objItem.ItemName,
                    UnitPrice = objOrderDetails.UnitPrice,
                    Quantity = objOrderDetails.Quantity,
                    Total = objOrderDetails.Total
                }).ToList();
            return PartialView("_OrderDetailPartial", listOfAllOrderDetails);
        }

        //[HttpGet]
        //public ActionResult Index(string reference)
        //{
        //           IEnumerable<OrderDetailsViewModel> listOfAllOrderDetails = (
        //           from objOrderDetails in onFood.OrderDetails
        //           join objOrder in onFood.Orders on objOrderDetails.OrderID equals objOrder.OrderID
        //           join objItem in onFood.Items on objOrderDetails.ItemID equals objItem.ItemID
        //           where objOrder.OrderNumber == reference
        //           select new OrderDetailsViewModel()
        //           {
        //               ItemID = objOrderDetails.ItemID,
        //               OrderNumber = objOrder.OrderNumber,
        //               ItemName = objItem.ItemName,
        //               UnitPrice = objOrderDetails.UnitPrice,
        //               Quantity = objOrderDetails.Quantity,
        //               Total = objOrderDetails.Total
        //           }).ToList();
        //    return View(listOfAllOrderDetails);
        //}
    }
}