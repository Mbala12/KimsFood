using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderItem.Models;
using OrderItem.Repositories;
using OrderItem.ViewModel;
using PagedList.Mvc;
using PagedList;

namespace OrderItem.Controllers
{
    [Authorize]
    [HandleError]
    public class ItemController : Controller
    {
        private OnlineFoodEntities onFood;
        public ItemController()
        {
            onFood = new OnlineFoodEntities();
        }
        // GET: Item
        public ActionResult Index()
        {
            CategoryRepository objCategoryRepository = new CategoryRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCategoryRepository.GetAllCategories(), objPaymentTypeRepository.GetAllPaymentType(), objItemRepository.GetAllItems());
            return View(objMultipleModels);
        }

        [HttpPost]
        public ActionResult Index(ItemViewModel objItemViewModel)
        {
            string Message = "";
            string ActualImageName = string.Empty;
            string ImageuniqueName = string.Empty;
            if (objItemViewModel.ItemID == 0)
            {
                ImageuniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageuniqueName + Path.GetExtension(objItemViewModel.ImagePath.FileName);
                objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + ActualImageName));
                Item objtItem = new Item()
                {
                    ItemID = objItemViewModel.ItemID,
                    CategoryID = objItemViewModel.CategoryID,
                    ItemName = objItemViewModel.ItemName,
                    ItemPrice = objItemViewModel.ItemPrice,
                    ImagePath = ActualImageName,
                    InStock = true
                };
                onFood.Items.Add(objtItem);
                Message = "New Item has been successfully Added.";
            }
            else
            {
                Item objItem = onFood.Items.Single(model => model.ItemID == objItemViewModel.ItemID);
                if (objItemViewModel.ImagePath != null)
                {
                    ImageuniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageuniqueName + Path.GetExtension(objItemViewModel.ImagePath.FileName);
                    objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + ActualImageName));
                    objItem.ImagePath = ActualImageName;
                }
                objItem.ItemName = objItemViewModel.ItemName;
                objItem.ItemPrice = objItemViewModel.ItemPrice;
                objItem.CategoryID = objItemViewModel.CategoryID;
                //objItem.InStock = true;
                Message = "This Item has been successfully Updated.";
            }
            onFood.SaveChanges();
            return Json(new { message = Message, success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllItems(int? i)
        {
            IEnumerable<ItemDetailViewModel> listOfAllItems = (
                from objItem in onFood.Items
                join objCat in onFood.Categories on objItem.CategoryID equals objCat.CategoryID
                where objItem.InStock == true
                select new ItemDetailViewModel()
                {
                    ItemID = objItem.ItemID,
                    CategoryName = objCat.CategoryName,
                    ItemName = objItem.ItemName,
                    ItemPrice = objItem.ItemPrice,
                    Image = objItem.ImagePath
                }).ToList().ToPagedList(i ?? 1, 7);
            return PartialView("_ItemDetailPartial", listOfAllItems);
        }

        public PartialViewResult GetAllOrderDetails(int? i)
        {
                IEnumerable<OrderDetailsViewModel> listOfAllOrderDetails = (
                from objOrderDetails in onFood.OrderDetails
                join objOrder in onFood.Orders on objOrderDetails.OrderID equals objOrder.OrderID
                join objItem in onFood.Items on objOrderDetails.ItemID equals objItem.ItemID
                select new OrderDetailsViewModel()
                {
                    OrderDetailID = objOrderDetails.OrderDetailsID,
                    OrderNumber = objOrder.OrderNumber,
                    OrderDate = objOrder.OrderDate,
                    DailyDate = objOrder.DailyDate,
                    Image = objItem.ImagePath,
                    ItemName = objItem.ItemName,
                    UnitPrice = objOrderDetails.UnitPrice,
                    Quantity = objOrderDetails.Quantity,
                    Total = objOrderDetails.Total
                }).ToList().ToPagedList(i ?? 1, 7);
            return PartialView("_OrderDetailPartial", listOfAllOrderDetails);
        }

        public PartialViewResult GetAllOrders(int? i)
        {
            IQueryable<OrderViewModel> listOfAllOrders = (
            //IEnumerable<OrderViewModel> listOfAllOrders = (
                from objOrder in onFood.Orders
                join objOrderDetails in onFood.OrderDetails on objOrder.OrderID equals objOrderDetails.OrderID
                join objPayment in onFood.PaymentTypes on objOrder.PaymentTypeID equals objPayment.PaymentTypeID
                //group new { objOrderDetails, objOrder, objPayment } by new { objOrder.OrderNumber, objOrderDetails.UnitPrice, objPayment.PaymentTypeName} into Group1
                select new OrderViewModel()
                {
                    OrderID = objOrder.OrderID, //Convert.ToInt32(Group1.Select(x => x.objOrder.OrderID)),
                    OrderNumber = objOrder.OrderNumber, //Group1.Select(x => x.objOrder.OrderNumber).ToString(),
                    PaymentName = objPayment.PaymentTypeName, //Group1.Select(x => x.objPayment.PaymentTypeName).ToString(),
                    Cost = objOrder.Cost, //Convert.ToDecimal(Group1.Select(x => x.objOrder.Cost)),
                    Time = objOrder.Time, //Convert.ToInt32(Group1.Select(x => x.objOrder.Time)),
                    FinalTotal = objOrder.FinalTotal, //Convert.ToDecimal(Group1.Select(x => x.objOrder.FinalTotal)),
                    CustomerName = objOrder.CustomerName, //Group1.Select(x => x.objOrder.CustomerName).ToString(),
                    CustomerPhone = objOrder.CustomerPhone //Group1.Select(x => x.objOrder.CustomerPhone).ToString()
                }).Distinct();//.ToList().Distinct().ToPagedList(i ?? 1, 10);
             return PartialView("_OrderPartial", listOfAllOrders);
        }

        //public PartialViewResult GetAllDailyOrders()
        //{
        //    //IEnumerable<DailyOrdersViewModel>
        //    var listOfAllDailyOrders = (
        //    from objOrderDetails in onFood.OrderDetails
        //    join objOrder in onFood.Orders on objOrderDetails.OrderID equals objOrder.OrderID
        //    join objItem in onFood.Items on objOrderDetails.ItemID equals objItem.ItemID
        //    group new { objOrderDetails, objOrder, objItem } by new { objOrder.DailyDate, objOrderDetails.UnitPrice, objItem.ItemName } into Group1
        //    //let DailyDate = Group1.Select(x => x.objOrder.DailyDate)
        //    select new //DailyOrdersViewModel()
        //        {
        //            DailyDate = Group1.Select(x => x.objOrder.DailyDate), //= Group1.Key.DailyDate,
        //            Image = Group1.Select(x => x.objItem.ImagePath),
        //            ItemName = Group1.Select(x => x.objItem.ItemName),
        //            UnitPrice = Group1.Select(x => x.objOrderDetails.UnitPrice),
        //            SumQuantity = Group1.Select(x => x.objOrderDetails.Quantity).Sum(),
        //            SumUnitPrice = Group1.Select(x => x.objOrderDetails.Total).Sum()
        //    });//.ToList()
        //    return PartialView("_DailyOrdersPartial", listOfAllDailyOrders);
        //}

        [HttpGet]
        public JsonResult EditItemDetails(int itemID)
        {
            var result = onFood.Items.Single(model => model.ItemID == itemID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RemoveItemDetails(int itemID)
        {
            var objItem = onFood.Items.Single(model => model.ItemID == itemID);
            objItem.InStock = false;
            onFood.SaveChanges();
            return Json(new { message = "Item has been Successfully Deleted.", success= true}, JsonRequestBehavior.AllowGet);
        }
    }
}