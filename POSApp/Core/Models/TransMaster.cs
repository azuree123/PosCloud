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
        public int? DeviceId { get; set; }
        public Device Device { get; set; }
        public int StoreId { get; set; }
        public string  TransferTo { get; set; }
        public Store Store { get; set; }
        public int SessionCode { get; set; }
        public string Type { get; set; }  //INV or PRI
        public string TransCode { get; set; }
        public string DeliveryType { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Name { get; set; }
        public int? BusinessPartnerId { get; set; }
        public BusinessPartner BusinessPartner { get; set; }
        public int? WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TransDate { get; set; }
        [DefaultValue(0)]
        public decimal Tax { get; set; }
        public string TransRef { get; set; }
        public string TransStatus { get; set; }

        [DefaultValue(0)]
        public decimal TotalPrice { get; set; }

        
        public bool Posted { get; set; }
        public string ACRef { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }//visa, master,accout,credit, cash etc
        public bool IsPurchased{ get; set; }
        public bool Issued { get; set; }
        public int? DineTableId { get; set; }
        public DineTable DineTable { get; set; }
        public int? OrderTime { get; set; }//in minutes
        public int? DiscountId { get; set; }
        public virtual TimedEvent TimedEvent { get; set; } 
        [DefaultValue(0)]
        public decimal Discount { get; set; }
        public ICollection<TransDetail> TransDetails { get; set; }=new List<TransDetail>();
        public ICollection<TransMasterPaymentMethod> TransMasterPaymentMethods { get; set; }=new List<TransMasterPaymentMethod>();
    }
}