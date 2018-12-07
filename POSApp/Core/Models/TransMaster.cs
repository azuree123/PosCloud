using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TransMaster:AuditableEntity
    {
        
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Type { get; set; }  //INV or PRI
        public string TransCode { get; set; }

        public int BusinessPartnerId { get; set; }
        public BusinessPartner BusinessPartner { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TransDate { get; set; }
        [DataType(DataType.DateTime)]

        public string TransRef { get; set; }
        public string TransStatus { get; set; }

        [DefaultValue(0)]
        public decimal TotalPrice { get; set; }

        public bool Posted { get; set; }
        public string ACRef { get; set; }

        public string PaymentMethod { get; set; }//visa, master,accout,credit, cash etc

        public int? DineTableId { get; set; }
        public DineTable DineTable { get; set; }

        public int? OrderTime { get; set; }//in minutes

        public ICollection<TransDetail> TransDetails { get; set; }
        public ICollection<TransMasterPaymentMethod> TransMasterPaymentMethods { get; set; }
    }
}