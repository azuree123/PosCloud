using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.SecurityFilters;

namespace POSApp.Core.ViewModels
{
    public class UserStoreViewModel
    {
        public int Id { get; set; }

        public string EncryptId
        {
            get { return AuthHelper.Encrypt(Id.ToString()); }
        }

        public string StoreName { get; set; }
    }
}