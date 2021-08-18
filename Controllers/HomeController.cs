using OrderItem.Models;
using OrderItem.Repositories;
using OrderItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderItem.Controllers
{
    [Authorize]
    [HandleError]
    public class HomeController : Controller
    {
        private OnlineFoodEntities onFood;
        public HomeController()
        {
            onFood = new OnlineFoodEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            CategoryRepository objCategoryRepository = new CategoryRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCategoryRepository.GetAllCategories(), objPaymentTypeRepository.GetAllPaymentType(), objItemRepository.GetAllItems());
            return View(objMultipleModels);
        }

        [HttpGet]
        public JsonResult GetSelectedItems(int categoryID)
        {
            //onFood.Configuration.ProxyCreationEnabled = false;
            List<Item> ListItems = onFood.Items.Where(x => x.CategoryID == categoryID).ToList();
            return Json(ListItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemUnitPrice(int itemID)
        {
            //decimal unitPrice = onFood.Items.Single(model => model.ItemID == itemID).ItemPrice;
            var result = onFood.Items.Single(model => model.ItemID == itemID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            objOrderRepository.AddOrder(objOrderViewModel);

            return Json("Your Order has been successfully Placed.", JsonRequestBehavior.AllowGet);
        }
    }
}