using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;
using POSApp.Models;
using POSApp.SecurityFilters;
using POSApp.Services;

namespace POSApp.Persistence.Repositories
{
    public class UserRepository : IUserRepository

    {
        private PosDbContext _context;

        public UserRepository(PosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsyncIncremental(int storeId, DateTime date)
        {
            return await _context.Users.Where(a => a.StoreId == storeId && !a.IsDisabled && (a.UpdatedOn >= date || a.CreatedOn >= date)).ToListAsync();
        }

        public IEnumerable<ApplicationUser> GetUsers(int storeid)
        {
            return _context.Users.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }

        public ApplicationUser GetUserById(string id, int storeid)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
        }
        public async Task<ApplicationUser> GetUserByIdAsync(string id, int storeid)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.Id == id && a.StoreId == storeid);
        }


        public void UpdateUser(string id, ApplicationUser ApplicationUser, int storeid)
        {
            ApplicationUser getUser = _context.Users.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            getUser.UserName = ApplicationUser.UserName;
            getUser.Email = ApplicationUser.Email;
            getUser.UpdatedById = HttpContext.Current.User.Identity.GetUserId();
            getUser.UpdatedOn = DateTime.Now;
        }

        public void DeleteUser(string id, int storeid)
        {

            var user = _context.Users.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            user.IsDisabled = true;
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
        }
        public IEnumerable<RegisterListViewModel> GetApiUsers(int storeid)
        {
            var list = _context.Users.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
            List<RegisterListViewModel> send = new List<RegisterListViewModel>();
            foreach (var registerListViewModel in list)
            {
                RegisterListViewModel model = new RegisterListViewModel();
                model.UserName = registerListViewModel.UserName;
                model.Email = registerListViewModel.Email;
                model.UserId = registerListViewModel.Id;
                model.UserPassword = Security.DecryptString(registerListViewModel.PasswordEncrypt,
                    "E546C8DF278CD5931069B522E695D4F2");
                model.EmployeeId = registerListViewModel.EmployeeId;
                send.Add(model);
            }
            return send;
        }
        public async Task<IEnumerable<RegisterListViewModel>> GetApiUsersAsync(int storeid)
        {
            var list = await _context.Users.Where(a => a.StoreId == storeid && !a.IsDisabled).ToListAsync();
            List<RegisterListViewModel> send = new List<RegisterListViewModel>();
            foreach (var registerListViewModel in list)
            {
                RegisterListViewModel model = new RegisterListViewModel();
                model.UserName = registerListViewModel.UserName;
                model.Email = registerListViewModel.Email;
                model.UserId = registerListViewModel.Id;
                model.UserPassword = Security.DecryptString(registerListViewModel.PasswordEncrypt,
                    "E546C8DF278CD5931069B522E695D4F2");
                model.EmployeeId = registerListViewModel.EmployeeId;
                send.Add(model);
            }
            return send;
        }
        public int GetShiftId(string userId, int storeId)
        {
            return _context.Users.Include(a => a.Employee).FirstOrDefault(a => a.Id == userId && a.StoreId == storeId).Employee.ShiftId;
        }
        public void UpdateRole(ApplicationRole role)
        {
          
            _context.Roles.Attach(role);
            _context.Entry(role).State = EntityState.Modified;
          
        }

        public UserRoleDataViewModel GetUserLoginData(string userId)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@p1", userId) };
            var sql = @"SELECT  a.Id,ISNULL(c.Name,'') as Name,a.StoreId
         ,ISNULL(STUFF((SELECT ', ' + CAST(e.Name AS VARCHAR(100)) [text()]
         FROM PosCloud.SecurityRights as d  inner join
        PosCloud.SecurityObjects as e on d.SecurityObjectId=e.SecurityObjectId
        where  d.Manage=1 and d.IdentityUserRoleId=c.Id
         FOR XML PATH(''), TYPE)
        .value('.','NVARCHAR(MAX)'),1,2,' '),'') as ManageData,
		ISNULL(STUFF((SELECT ', ' + CAST(e.Name AS VARCHAR(100)) [text()]
         FROM PosCloud.SecurityRights as d  inner join
        PosCloud.SecurityObjects as e on d.SecurityObjectId=e.SecurityObjectId
           where  d.[View]=1 and d.IdentityUserRoleId=c.Id
         FOR XML PATH(''), TYPE)
        .value('.','NVARCHAR(MAX)'),1,2,' '),'') as ViewData  	
        from AspNetUsers as a left outer join
        (AspNetUserRoles as b  inner join 
        AspNetRoles as c on b.RoleId=c.Id)on a.Id=b.UserId
        where a.Email= @p1";
            var data = _context.Database.SqlQuery<UserRoleDataViewModel>(sql, parameters.ToArray()).FirstOrDefault();

            return data;
        }


    }
}
