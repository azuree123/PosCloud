using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using POSApp.Controllers;
using POSApp.Controllers.WebApi;

namespace AAPackages.Handlers
{
    public class Config
    {
        public enum UserRole : byte
        {
            
            Admin = 1,
            
            Manager = 2,
            
            User = 3
        }
        public enum Permission
        {
            Inventory = 1,
            Purchases = 2,
            Sales = 3,
            CMS = 4,
            Payroll=5,
            Accounts=6,
            Reports=7,
            Setup=8,
            Security = 9
        }
 

        public enum Inventory
        {
            ItemDefinition = 1,
            OpeningStock = 2,
            CreatePromotion = 3,
            PromotionDetails = 4,
            IssueItems=5

           

        }
        public enum Purchases
        {
            SupplierInfo = 1,
            PurchaseOrder = 2,
            GoodRecieptNote = 3,
            GoodReturnNote = 4,
            PaymentToSupplier = 5
           

        }
         public enum Sales
        {
             CustomerInfo = 1,
            Quotation = 2,
            QuotationEstimation = 3,
            BookingOrder = 4,
            DeliveryChallan = 5,
            SaleInvoice = 6,
            SaleInvoiceWithOutTax = 7,
            SalesReturn = 8,
            PaymentToCustomer = 9
           

        }
         public enum CMS
        {
            RegisterComplain = 1,
            AssignComplaints = 2,
            ComplaintsResolved = 3,
            ProductsInstalled=4

        }
         public enum Payroll
        {
            EmployeeInformation = 1,
            DailyAttendance = 2,
            AttendanceAnnualRegister = 3,
            EmployeeMonthlyAttendance = 4,
            LeaveApplication = 5,
            CompensateryLeave = 6,
            OfficialHoliday = 7,
            TaDa = 8,
            LeaveTypes = 9
           

        }
         public enum Accounts
        {
            Expenses = 1,
            GroupCompany = 2,
            BankInfo = 3,
            GeneralVoucher = 4,
            VoucherApproval = 5,
            GeneralLedger = 6,
            TrailBalance = 7,
            BalanceSheet = 8,
            ProfitLoss = 9
           

        }
         public enum Reports
        {
            DayBook = 1,
            Stock = 2,
            Purchases = 3,
            Ledgers = 4,
            Quotation = 5,
             Sales=6,
             Accounts=7
           

        }
         public enum Setup
        {
            Inventory = 1,
            Sales = 2,
            Payroll = 3,
            Accounts = 4
           

        }
         public enum Security
        {
            ChangePassword = 1,
            CreateSoftwareGroup = 2,
            CreateSoftwareUser = 3,
            AssignAccess = 4,
           ChangeCompanyInfo=5,
           ChangeImg = 6

        }

        // public static void Email(string email,bool saletax,int id,ControllerContext ccContext)
        //{
        //    string hostadd = ConfigurationManager.AppSettings["Host"].ToString();
        //    string fromEmail = ConfigurationManager.AppSettings["FromMail"].ToString();
        //    string password = ConfigurationManager.AppSettings["Password"].ToString();
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress(fromEmail);
        //    mailMessage.Subject = "Sale Invoice Alert Email";
        //    Controllers.SaleOrdersController data = new SaleOrdersController();
        //     if (saletax)
        //     {

        //         mailMessage.Body = data.PreviewSaleInvoiceWithTaxEmail(ccContext, id);
        //     }
        //     else
        //     {
        //         mailMessage.Body = data.PreviewSaleInvoiceWithOutTaxEmail(id);
        //     }
            
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.To.Add(new MailAddress(email));
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = hostadd;
        //    smtp.EnableSsl = true;
        //    NetworkCredential networdCard = new NetworkCredential();
        //    networdCard.UserName = mailMessage.From.Address;
        //    networdCard.Password = password;
        //    smtp.UseDefaultCredentials = true;
        //    smtp.Credentials = networdCard;
        //    smtp.Port = 587;
        //    smtp.Send(mailMessage);
        //}

         

        





    }
}