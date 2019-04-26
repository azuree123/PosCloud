using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using POSApp.Core.Models;
using POSApp.Persistence;


namespace POSApp.Handlers
{
    public class AuthHelper
    {
        public static PosDbContext PosDbContext { get; private set; }

        public static bool IsAlreadyLoggedIn(HttpContextBase httpContext)
        {
            PosDbContext = new PosDbContext();
            var cookie = HttpContext.Current.Request.Cookies["PayRoll"];
           
            if (cookie != null)
            {
                var email = Decrypt(cookie.Values["Client"]);
                User check = PosDbContext.Users.Where(a => a.Email == email).ToList().FirstOrDefault();
                if(check!=null){
                return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static int GetUserCompany(HttpContextBase httpContext)
        {
           
            var cookie = HttpContext.Current.Request.Cookies["PayRoll"];

            if (cookie != null)
            {
                string companyid = Decrypt(cookie.Values["Company"]);
                int company = Convert.ToInt32(companyid);
               
                return company;
            }
            else
            {
                return 0;
            }
        }
        public static int GetUserDetails(HttpContextBase httpContext)
        {

            var cookie = HttpContext.Current.Request.Cookies["PayRoll"];

            if (cookie != null)
            {
                string companyid = Decrypt(cookie.Values["ClientId"]);
                int company = Convert.ToInt32(companyid);

                return company;
            }
            else
            {
                return 0;
            }
        }

        public static string Encrypt(string input)
        {
            return (MachineKey.Encode(GetBytes(input), MachineKeyProtection.All));
        }
        public static string Decrypt(string encodedData)
        {
            return GetString(MachineKey.Decode(encodedData, MachineKeyProtection.All));
        }
        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public string UserData(HttpContextBase httpContext)
        {
            AAPackagesDbEntities context = new AAPackagesDbEntities();
           int userid=GetUserDetails(httpContext);
            int cid = GetUserCompany(httpContext);
            string senddata = string.Empty;
            if (userid != 0)
            {
                string role = context.Users.Where(a => a.id == userid).Select(a => a.role).FirstOrDefault();
                senddata =
                    context.UserGroupAccesses.Where(a => a.userCompany == cid && a.groupName == role)
                        .Select(a => a.groupPermission)
                        .FirstOrDefault();



            }
            else
            {

            }
            return senddata;
        }
        public string UserFunctionalitiesData(HttpContextBase httpContext)
        {
            AAPackagesDbEntities context = new AAPackagesDbEntities();
            int userid = GetUserDetails(httpContext);
            int cid = GetUserCompany(httpContext);
            string senddata = string.Empty;
            if (userid != 0)
            {
                string role = context.Users.Where(a => a.id == userid).Select(a => a.role).FirstOrDefault();
                senddata =
                    context.UserGroupAccesses.Where(a => a.userCompany == cid && a.groupName == role)
                        .Select(a => a.functionalities)
                        .FirstOrDefault();



            }
            else
            {

            }
            return senddata;
        }

        public static string GetBackImg()
        {
            AAPackagesDbEntities context=new AAPackagesDbEntities();
            string img = context.BackGroundImages.ToList().FirstOrDefault().LoginImage;
            return img;
        }


      
    }
}