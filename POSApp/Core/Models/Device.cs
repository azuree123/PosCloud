﻿using System;
namespace POSApp.Core.Models
{
    public class Device:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Licence { get; set; }

        public string Code { get; set; }

        public string AppVersion { get; set; }

        public DateTime DownloadedDate { get; set; }


        public string Address { get; set; }

        public string Contact { get; set; }

        public string City { get; set; }

        public string State { get; set; }

    }
}