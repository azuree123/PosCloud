using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Services;
using RazorEngine;
using Encoding = System.Text.Encoding;


namespace POSApp.Controllers
{
    public class ReportMailController : LanguageController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        
   
        public ReportMailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: ReportMail


        public async Task<JsonResult> SendUserMail()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DateTime currentDate = DateTime.Now.Date;
            DateTime dateWeekBefore = currentDate.AddDays(-7);
            EmailFormViewModel vm = new EmailFormViewModel();
            vm.CurrentDate = currentDate;
            vm.DateWeekBefore = dateWeekBefore;
            vm.ClientName = _unitOfWork.ClientRepository.GetClients().Select(a => a.Name).FirstOrDefault();
            vm.Currency = _unitOfWork.StoreRepository.GetStores().Select(a => a.City).FirstOrDefault();
            vm.TotalOrders = _unitOfWork.TransMasterRepository
                .GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Count(a => a.Type == "INV" && (a.TransStatus == "Paid" || a.TransStatus == "Complete") && a.TransDate >= dateWeekBefore);
           
            vm.Sales =  _unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current))
                .Where(a => a.Type == "INV" && (a.TransStatus == "Paid" || a.TransStatus == "Complete") && a.TransDate >= dateWeekBefore).Select(a => a.TotalPrice).Sum();
            vm.TimeSales = _unitOfWork.TransMasterRepository.GetTimeSale().Select(a=>a.Amount).Sum();
            vm.TimeOrders = _unitOfWork.TransMasterRepository.GetTimeSale().Count();
            vm.Time = _unitOfWork.TransMasterRepository.GetTimeSale().Select(a => a.Time).FirstOrDefault();
            vm.EmployeeName = _unitOfWork.TransMasterRepository.GetTopEmployeeSale().Select(a => a.EmployeeName).FirstOrDefault();
            vm.EmployeeSales = _unitOfWork.TransMasterRepository.GetTopEmployeeSale().Select(a => a.Amount).Sum();
            vm.EmployeeOrders = _unitOfWork.TransMasterRepository.GetTopEmployeeSale().Count();
            vm.WeeklySales = _unitOfWork.TransMasterRepository.GetWeeklyIncome();
            vm.WeekBeforeSalesCompare = _unitOfWork.TransMasterRepository.GetBeforeWeeklyIncome() - vm.WeeklySales;

            var client = _unitOfWork.ClientRepository.GetClient((int) UserStores.GetStoreCookie(System.Web.HttpContext.Current));





            vm.EmailTo = client.Email;
            vm.Subject = "Weekly Summary Report";
            vm.Body = RenderRazorViewToString("mail", vm);
            
            //await MessageServices.SendEmailAsync("info@dealinwheel.com", "Order Confirmation at DeailinWheel.com", body, user.Email);

            var task = Task.Run(() => SendEmail(vm));

            bool result = false;

            result = SendEmail(vm);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SendEmail(EmailFormViewModel Vm)
        {
            try
            {
               
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["senderEmail"].ToString();
                string senderPassword =
                    System.Configuration.ConfigurationManager.AppSettings["senderPassword"].ToString();
                SmtpClient client = new SmtpClient("mail.poscosmic.com", 25);
                client.EnableSsl = false;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail,senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail,Vm.EmailTo,Vm.Subject,Vm.Body);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                    viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                    ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


    }
}