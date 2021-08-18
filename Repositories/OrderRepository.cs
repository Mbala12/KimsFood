using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OrderItem.Models;
using OrderItem.ViewModel;

namespace OrderItem.Repositories
{
    public class OrderRepository
    {
        private OnlineFoodEntities onFood;
        public OrderRepository()
        {
            onFood = new OnlineFoodEntities();
        }

        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            var KimsAcc = "kimsfood20@gmail.com";

            Order objOrder = new Order();
            objOrder.CustomerName = objOrderViewModel.CustomerName;
            objOrder.CustomerPhone = objOrderViewModel.CustomerPhone;
            objOrder.CustomerAddress = objOrderViewModel.CustomerEmail;
            objOrder.DailyDate = DateTime.Now.ToShortDateString();
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.Time = objOrderViewModel.Time;
            objOrder.Cost = objOrderViewModel.Cost;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = String.Format("{0:ddmmmyyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeID = objOrderViewModel.PaymentTypeID;
            onFood.Orders.Add(objOrder);
            onFood.SaveChanges();

            int orderID = objOrder.OrderID; 
            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderID = orderID;
                objOrderDetail.ItemID = item.ItemID;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Quantity = item.Quantity;
                objOrderDetail.Total = item.Total;
                onFood.OrderDetails.Add(objOrderDetail);
                onFood.SaveChanges();
            }

            #region SendEmailForConfirmation
            if(objOrder.CustomerAddress != null)
            {
                SendCustomerEmail(objOrder.CustomerName, objOrder.CustomerAddress, objOrder.OrderNumber, objOrder.Cost, objOrder.PaymentTypeID, objOrder.FinalTotal);
                SendUsEmail(objOrder.CustomerName, KimsAcc, objOrder.OrderNumber, objOrder.Cost, objOrder.PaymentTypeID, objOrder.FinalTotal, objOrder.CustomerPhone);
            }
            else
            {
                SendUsEmail(objOrder.CustomerName, KimsAcc, objOrder.OrderNumber, objOrder.Cost, objOrder.PaymentTypeID, objOrder.FinalTotal, objOrder.CustomerPhone);
            }
            #endregion

            return true;
        }

        public void SendCustomerEmail(string username, string emailID, string reference, decimal transport, int payment, decimal amount)
        {
            var account = "kimsfood20@gmail.com";
            var accountTitle = "Kim's Food Catering";
            var phone = "(+27) 738-493-596";
            var title = "Kim's Food";
            string subject = "";
            string body = "";
            var fromEmail = new MailAddress(account, accountTitle);
            var fromEmailPassword = "KimsKims2020";
            var toEmail = new MailAddress(emailID);
            

            if (emailID != null)
            {
                if (payment == 1)
                {
                    var type = "Card";
                    subject = "Your Order has been successfully Placed";
                    body = "Hello Dear " + username + "<br/><br/>We are excited to tell you that your "+ title +" Online Order has been" +
                            " successfully received on the " + DateTime.Now + " and Your " + title + " Online Order Reference Number is: <b><a>" + reference + "</b><br/><br/> You have chosen to pay via " + type.ToUpper() + ", the amount of R" + amount + 
                            " including R" + transport + " for Delivery.<br/><br/><br/>Kind Regards<br/><b>" + accountTitle + "</b><br/> Cell phone: "+ phone +"<br/> Your Happiness is our choice";
                }
                else if(payment == 2)
                {
                    var type = "Cash";
                    subject = "Your Order has been successfully Placed";
                    body = "Hello Dear " + username + "<br/><br/>We are excited to tell you that your "+ title +" Online Order has been" +
                            " successfully received on the " + DateTime.Now + " and Your " + title + " Online Order Reference Number is: <b><a>" + reference + "</b><br/><br/> You have chosen to pay via " + type.ToUpper() + ", the amount of R" + amount + 
                            " including R" + transport + " for Delivery.<br/><br/><br/> Kind Regards <br/><b>"+ accountTitle + "</b><br/> Cell phone: " + phone + "<br/> Your Happiness is our choice";
                }
                else if(payment == 3)
                {
                    var type = "EFT";
                    subject = "Your Order has been successfully Placed";
                    body = "Hello Dear " + username + "<br/><br/>We are excited to tell you that your "+ title +" Online Order has been" +
                            " successfully received on the " + DateTime.Now + " and Your " + title + " Online Order Reference Number is: <b><a>" + reference + "</b><br/><br/> You have chosen to pay via " + type + ", the amount of R" + amount + 
                            " including R" + transport + " for Delivery.<br/><br/><br/> Kind Regards <br/><b>"+ accountTitle + "</b><br/> Cell phone: " + phone + "<br/> Your Happiness is our choice";
                }
               
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }

        public void SendUsEmail(string username, string emailID, string reference, decimal transport, int payment, decimal amount, string phone)
        {
            //var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            //reference = "/OrderDetail/GetAllOrderDetails";

            var account = "kimsfood20@gmail.com";
            var accountTitle = "Kim's Food Catering";
            var fromEmail = new MailAddress(account, accountTitle);
            var fromEmailPassword = "KimsKims2020";
            var toEmail = new MailAddress(emailID);
            var subject = "";
            var body = "";

            if(payment == 1) 
            { 
                string type = "Card";
                subject = "New Online Order with Reference Number " + reference;
                body = "Hello Dear " + accountTitle + "<br/><br/>A Customer called <b>" + username.ToUpper() + "</b> with phone number: <b>" + phone +
                    "</b> has placed a new Kim's Food Online Order " + " on the " + DateTime.Now + " and His/Her " + accountTitle + " Online Order Reference Number is: <b><a>"
                     + reference + "</b>" + "</b><br/><br/>"+ username.ToUpper() + " has chosen to pay via " + type.ToUpper() + ", the amount of R" + amount +
                    " including R" + transport + " for Delivery." + "<br/><br/><br/>Kind Regards<br/><b>" + accountTitle + "</b><br/>Your Happiness is our choice";
            }
            else if(payment == 2)
            {
                string type = "Cash";
                subject = "New Online Order with Reference Number " + reference;
                body = "Hello Dear " + accountTitle + "<br/><br/>A Customer called <b>" + username.ToUpper() + "</b> with phone number: <b>" + phone +
                    "</b> has placed a new Kim's Food Online Order " + " on the " + DateTime.Now + " and His/Her " + accountTitle + " Online Order Reference Number is: <b><a>"
                     + reference + "</b>" + "</b><br/><br/>"+ username.ToUpper() + " has chosen to pay via " + type.ToUpper() + ", the amount of R" + amount +
                    " including R" + transport + " for Delivery." + "<br/><br/><br/>Kind Regards<br/><b>" + accountTitle + "</b><br/>Your Happiness is our choice";
            }
            else if (payment == 3)
            {
                string type = "EFT";
                subject = "New Online Order with Reference Number " + reference;
                body = "Hello Dear " + accountTitle + "<br/><br/>A Customer called <b>" + username.ToUpper() + "</b> with phone number: <b>" + phone +
                    "</b> has placed a new Kim's Food Online Order " + " on the " + DateTime.Now + " and His/Her " + accountTitle + " Online Order Reference Number is: <b><a>"
                    + reference + "</b>" + "</b><br/><br/>"+ username.ToUpper() + " has chosen to pay via " + type.ToUpper() + ", the amount of R" + amount +
                    " including R" + transport + " for Delivery." + "<br/><br/><br/>Kind Regards<br/><b>" + accountTitle + "</b><br/>Your Happiness is our choice";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }
    }
}
