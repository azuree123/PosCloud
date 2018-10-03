using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models.DbModels
{
    public class ExpenseHead:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}