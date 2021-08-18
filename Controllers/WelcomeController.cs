using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderItem.Controllers
{
    [HandleError]
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Location()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }
    }
}