using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using Newtonsoft.Json.Converters;

namespace POSApp.Core.Dtos
{
    public class PosScreen
    {
        public List<PosProducts> PosProducts { get; set; }
        public List<PosCategory> PosCategories { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<PosHold> PosHolds { get; set; }
        public int Hold { get; set; }
        public string HoldRef { get; set; }
    }

    public class PosHold
    {
        public string Description { get; set; }
        public string Ref { get; set; }
        public int Id { get; set; }

    }
    public class PosCustomer
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public string Msg { get; set; }

    }
    public class PosProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int StoreId { get; set; }
        public string ProductImage { get; set; }
    }

    public class PosCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryGroup { get; set; }
        public int StoreId { get; set; }
        public string CategoryImage { get; set; }
        
    }
    public partial class Row
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string category_id { get; set; }
        public string price { get; set; }
        public string image { get; set; }
        public string tax { get; set; }
        public string tax_method { get; set; }
        public string quantity { get; set; }
        public string barcode_symbology { get; set; }
        public string type { get; set; }
        public string alert_quantity { get; set; }
        public string store_price { get; set; }
        public int qty { get; set; }
        public string comment { get; set; }
        public string discount { get; set; }
        public string real_unit_price { get; set; }
        public string unit_price { get; set; }
    }

    public partial class RootObject
    {
        public string id { get; set; }
        [JsonConverter(typeof(string))]
        public string item_id { get; set; }
        public string label { get; set; }
        public Row row { get; set; }
        public bool combo_items { get; set; }
    }
    public class NoRootObject
    {
        public int id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }
}
