using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TimedEventProducts
    {
        public string ProductCode { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public int  TimedEventId { get; set; }
        [ForeignKey("TimedEventId")]
        public TimedEvent TimedEvent { get; set; }
        
    }
}