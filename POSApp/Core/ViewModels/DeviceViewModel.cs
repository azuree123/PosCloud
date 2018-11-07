﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class DeviceViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string License { get; set; }
        public int StoreId { get; set; }
        public string DeviceCode { get; set; }

        public string AppVersion { get; set; }
        [DataType(DataType.Date)]
        public DateTime DownloadedDate { get; set; }


        public string Address { get; set; }

        public string Contact { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}