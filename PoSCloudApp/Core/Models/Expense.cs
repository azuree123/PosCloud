using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models.DbModels;

namespace PoSCloudApp.Core.Models
{
    public class Expense:AuditableEntity
    {
        public int Id { get; set; }
        public int ExpenseHead { get; set; }
    }
}