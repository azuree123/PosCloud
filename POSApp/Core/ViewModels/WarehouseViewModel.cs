﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class WarehouseViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ArabicName { get; set; }
        [DisplayName("Branch")]
        public int StoreId { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> StoreDdl { get; set; }
    }
    public class WarehouseListModelView
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
    }
}