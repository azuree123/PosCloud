using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PoSCloud.Core.Models;

namespace PoSCloud.Persistence
{
    public class PosDbContext : IdentityDbContext<ApplicationUser>
    {
        public PosDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.SetCommandTimeOut(300);
        }
       
        public void SetCommandTimeOut(int Timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Timeout;
        }
        public static PosDbContext Create()
        {

            return new PosDbContext();
        }


        
    }
}
