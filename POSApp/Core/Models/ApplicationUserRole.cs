using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace POSApp.Core.Models
{
    public class ApplicationRole : IdentityRole
    {

     

        public int? StoreId { get; set; }
        public Store Store { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; }
         
        public string UpdatedById { get; set; }
        

        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedOn { get; set; }


    }



}