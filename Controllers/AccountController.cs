using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderItem.ViewModel;
using OrderItem.Models;
using System.Web.Security;

namespace OrderItem.Controllers
{
    public class AccountController : Controller
    {
        private OnlineFoodEntities onFood;
        public AccountController()
        {
            onFood = new OnlineFoodEntities();
        }
        //GET: Account
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(AccountViewModel accView)
        //{
        //    var user = onFood.Accounts.Where(x => x.EmailID == accView.EmailID).FirstOrDefault();
        //    if (user != null)
        //    {
        //        if (string.Compare(crypto.Hash(accView.Password), user.Password) == 0)
        //        {
        //            Session["role"] = user.Role;
        //            FormsAuthentication.SetAuthCookie(user.UserName, false);
        //            return RedirectToAction("Index", "Welcome");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "UserName or Password Incorrect!!!");
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "UserName or Password Incorrect!!!");
        //        return View();
        //    }
        //}

        //public ActionResult SignUp()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SignUp(SignUpViewModel signup)
        //{
        //    var account = "kimsfood20@gmail.com";
        //    var phone = "0738493596";
        //    Account acc = new Account();
        //    acc.UserName = signup.Username;
        //    acc.EmailID = signup.EmailID;
        //    acc.Phone = signup.Phone;
        //    acc.Password = crypto.Hash(signup.Password);
        //    acc.Role = "user";
        //    acc.IsActive = true;
        //    acc.CreatedOn = DateTime.Now.ToShortDateString();
        //    acc.ActivityDate = DateTime.Now;

        //    var user = onFood.Accounts.Where(x => x.UserName == signup.Username || x.EmailID == signup.EmailID || x.Phone == signup.Phone || x.Password == signup.Password).FirstOrDefault();
        //    if (user != null && user.UserName == signup.Username)
        //    {
        //        ModelState.AddModelError("", "This Username already exists");
        //        return View();
        //    }
        //    else if (user != null && user.EmailID == signup.EmailID)
        //    {
        //        ModelState.AddModelError("", "This Email Address already exists");
        //        return View();
        //    }
        //    else if (user != null && user.Phone == signup.Phone)
        //    {
        //        ModelState.AddModelError("", "This Phone No. already exists");
        //        return View();
        //    }
        //    else if (user != null && user.Password == signup.Password)
        //    {
        //        ModelState.AddModelError("", "This Password already exists");
        //        return View();
        //    }
        //    else
        //    {
        //        onFood.Accounts.Add(acc);
        //        onFood.SaveChanges();
        //        return RedirectToAction("KimLogin", "Account");
        //    }
        //}

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("MainPage", "Welcome");
        }

        //[HttpGet]
        public ActionResult KimLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KimLogin(AccountViewModel accView)
        {
            var user = onFood.Accounts.Where(x => x.EmailID == accView.EmailID).FirstOrDefault();
            if (accView.EmailID != null && accView.Password != null)
            {
                if (string.Compare(crypto.Hash(accView.Password), user.Password) == 0)
                {
                    Session["role"] = user.Role;
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Welcome");
                }
                else
                {
                    ModelState.AddModelError("", "Email address or Password Incorrect!!!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Email address or Password Incorrect!!!");
                return View();
            }
        }

        public ActionResult KimSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KimSignUp(Account acc)
        {
            acc.Password = crypto.Hash(acc.Password);
            acc.Role = "user";
            acc.CreatedOn = DateTime.Now.ToShortDateString();
            acc.IsActive = true;
            acc.ActivityDate = DateTime.Now;
            onFood.Accounts.Add(acc);
            onFood.SaveChanges();
            return RedirectToAction("KimLogin", "Account");
        }
    }
}