﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.ViewModels
{
    public class DesignationViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}