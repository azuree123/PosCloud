using System;
using System.Collections.Generic;
using System.Linq;
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


        public UserRoleDataViewModel UserAccessData()
        {
            var cookie = HttpContext.Current.Request.Cookies.AllKeys.Where(a => a.Contains("UserRoleData"));
            string val = string.Empty;
            string test = string.Empty;
            if (cookie.Any())
            {
                foreach (var cook in cookie)
                {
                    var getVal = HttpContext.Current.Request.Cookies[cook];
                    if (getVal != null)
                    {
                        val += getVal.Value;
                    }
                }
                test = val;
            }
            return JsonConvert.DeserializeObject<UserRoleDataViewModel>(test);
        }
     



        /// <summary>
        /// Encrypt the given string using the default key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <returns>The encrypted string.</returns>
        ///
        //private static string _key = "POSCLOUD_381_FutureField";

        public static IEnumerable<string> Split(string str, int chunkSize)
        {
            int val =(str.Length + (chunkSize-1)) / chunkSize;
            var nums = Enumerable.Range(0, val);
            List<string> arr = new List<string>();
            foreach (var num in nums)
            {
                var s=str.Substring(num * chunkSize);
                if (s.Length < chunkSize)
                {
                    arr.Add(s);
                }
                else
                {
                arr.Add(str.Substring(num*chunkSize, chunkSize)); 
                }
            }

            return arr;
        }
        public static IEnumerable<String> EnumByNearestSpace(String value, int length)
        {
            if (String.IsNullOrEmpty(value))
                yield break;

            int bestDelta = int.MaxValue;
            int bestSplit = -1;

            int from = 0;

            for (int i = 0; i < value.Length; ++i)
            {
                var Ch = value[i];

                if (Ch != ' ')
                    continue;

                int size = (i - from);
                int delta = (size - length > 0) ? size - length : length - size;

                if ((bestSplit < 0) || (delta < bestDelta))
                {
                    bestSplit = i;
                    bestDelta = delta;
                }
                else
                {
                    yield return value.Substring(from, bestSplit - from);

                    i = bestSplit;

                    from = i + 1;
                    bestSplit = -1;
                    bestDelta = int.MaxValue;
                }
            }

            // String's tail
            if (from < value.Length)
            {
                if (bestSplit >= 0)
                {
                    if (bestDelta < value.Length - from)
                        yield return value.Substring(from, bestSplit - from);

                    from = bestSplit + 1;
                }

                if (from < value.Length)
                    yield return value.Substring(from);
            }
        }
        //public static string Encrypt(string strToEncrypt)
        //{
        //    try
        //    {
        //        return Encrypt(strToEncrypt, _key);
        //    }

        //    catch (Exception ex)
        //    {
        //        return "Wrong Input. " + ex.Message;
        //    }
        //}

        /// <summary>
        /// Decrypt the given string using the default key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        //public static string Decrypt(string strEncrypted)
        //{
        //    try
        //    {
        //        return Decrypt(strEncrypted, _key);
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Wrong Input. " + ex.Message;
        //    }
        //}

        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        //public static string Encrypt(string strToEncrypt, string strKey)
        //{
        //    try
        //    {
        //        TripleDESCryptoServiceProvider objDESCrypto =
        //            new TripleDESCryptoServiceProvider();
        //        MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
        //        byte[] byteHash, byteBuff;
        //        string strTempKey = strKey;
        //        byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
        //        objHashMD5 = null;
        //        objDESCrypto.Key = byteHash;
        //        objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
        //        byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
        //        return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
        //            TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Wrong Input. " + ex.Message;
        //    }
        //}

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        //public static string Decrypt(string strEncrypted, string strKey)
        //{
        //    try
        //    {
        //        TripleDESCryptoServiceProvider objDESCrypto =
        //            new TripleDESCryptoServiceProvider();
        //        MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
        //        byte[] byteHash, byteBuff;
        //        string strTempKey = strKey;
        //        byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
        //        objHashMD5 = null;
        //        objDESCrypto.Key = byteHash;
        //        objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
        //        byteBuff = Convert.FromBase64String(strEncrypted);
        //        string strDecrypted = ASCIIEncoding.ASCII.GetString
        //        (objDESCrypto.CreateDecryptor().TransformFinalBlock
        //        (byteBuff, 0, byteBuff.Length));
        //        objDESCrypto = null;
        //        return strDecrypted;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Wrong Input. " + ex.Message;
        //    }
        //}
    }
}