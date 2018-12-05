using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;

namespace POSApp.Persistence.Repositories
{
    public class UserRepository: IUserRepository
    
        {
        private PosDbContext _context;

        public UserRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetUsers(int storeid)
        {
            return _context.Users.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }

        public ApplicationUser GetUserById(string id, int storeid)
        {
            return _context.Users.FirstOrDefault(a=>a.Id==id && a.StoreId==storeid);
        }
    
      

        public void UpdateUser(string id, ApplicationUser ApplicationUser, int storeid)
        {
            ApplicationUser getUser = _context.Users.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            getUser.UserName = ApplicationUser.UserName;
            getUser.Email = ApplicationUser.Email;
            getUser.UpdatedById = HttpContext.Current.User.Identity.GetUserId();
            getUser.UpdatedOn=DateTime.Now;
        }

        public void DeleteUser(string id, int storeid)
        {
           
            var user = _context.Users.FirstOrDefault(a=>a.Id==id && a.StoreId==storeid);
            user.IsDisabled = true;
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
