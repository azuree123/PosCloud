using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace POSApp.Core.Models
{
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
      
    // public int StoreId { get; set; }
    public string PasswordEncrypt { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int? POSTerminalId { get; set; }
        public POSTerminal POSTerminal { get; set; }
       
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedOn { get; set; }
        public bool IsDisabled { get; set; }
        public string CreatedById { get; set; }
       
        public string UpdatedById { get; set; }
       

        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedOn { get; set; }
        public virtual ICollection<TillOperation> TillOperations { get; set; }
        public virtual ICollection<UserStore> UserStores { get; set; }=new List<UserStore>();
    }

   

}