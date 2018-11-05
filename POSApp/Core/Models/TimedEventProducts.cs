﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TimedEventProducts
    {
        public int TimedEventId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public TimedEvent TimedEvent { get; set; }
        public Product Product { get; set; }
    }
}