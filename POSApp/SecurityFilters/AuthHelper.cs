using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using POSApp.Models;
using Newtonsoft.Json;


namespace POSApp.SecurityFilters
{
    public class AuthHelper
    {

        //public static bool IsAlreadyLoggedIn(HttpContextBase httpContext)
        //{
        //    PosDbContext context = new PosDbContext();
        //    var cookie = HttpContext.Current.Request.Cookies["PayRoll"];

        //    if (cookie != null)
        //    {
        //        var email = Decrypt(cookie.Values["Client"]);
        //        ApplicationUser check = context.Users.Where(a => a.Email == email).ToList().FirstOrDefault();
        //        if (check != null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //public static int GetUserCompany(HttpContextBase httpContext)
        //{

        //    var cookie = HttpContext.Current.Request.Cookies["PayRoll"];

        //    if (cookie != null)
        //    {
        //        string companyid = Decrypt(cookie.Values["Company"]);
        //        int company = Convert.ToInt32(companyid);

        //        return company;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //public static int GetUserDetails(HttpContextBase httpContext)
        //{

        //    var cookie = HttpContext.Current.Request.Cookies["PayRoll"];

        //    if (cookie != null)
        //    {
        //        string companyid = Decrypt(cookie.Values["ClientId"]);
        //        int company = Convert.ToInt32(companyid);

        //        return company;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //public static string Encrypt(string input)
        //{
        //    return (MachineKey.Encode(GetBytes(input), MachineKeyProtection.All));
        //}
        //public static string Decrypt(string encodedData)
        //{
        //    return GetString(MachineKey.Decode(encodedData, MachineKeyProtection.All));
        //}
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


        public UserRoleDataViewModel UserAccessData()
        {
            var cookie = HttpContext.Current.Request.Cookies["UserRoleData"];
            string val = string.Empty;
            if (cookie != null)
            {
                val = AuthHelper.Decrypt(cookie.Value);
            }
            return JsonConvert.DeserializeObject<UserRoleDataViewModel>(val);
        }
        //public string UserFunctionalitiesData(HttpContextBase httpContext)
        //{
        //    PosDbContext context = new PosDbContext();
        //    int userid = GetUserDetails(httpContext);
        //    string senddata = string.Empty;
        //    if (userid != 0)
        //    {
        //        string role = context.Users.Where(a => a.StoreId == userid).Select(a => a.Roles).FirstOrDefault().ToString();
        //        senddata =
        //            context.SecurityRights.Where(a => a.StoreId == cid && a.IdentityUserRoleId == role)
        //                .Select(a => a.Role.Name)
        //                .FirstOrDefault();



        //    }
        //    else
        //    {

        //    }
        //    return senddata;
        //}



        /// <summary>
        /// Encrypt the given string using the default key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <returns>The encrypted string.</returns>
        ///
        private static string _key = "POSCLOUD_381_FutureField";
        public static string Encrypt(string strToEncrypt)
        {
            try
            {
                return Encrypt(strToEncrypt, _key);
            }

            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Decrypt the given string using the default key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted)
        {
            try
            {
                return Decrypt(strEncrypted, _key);
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }
    }
}