using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Models;

namespace POSApp.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers(int storeid);
        ApplicationUser GetUserById(string id, int storeid);
        void UpdateUser(string id, ApplicationUser ApplicationUser, int storeid);
        void DeleteUser(string id, int storeid);
        IEnumerable<RegisterListViewModel> GetApiUsers(int storeid);
        Task<ApplicationUser> GetUserByIdAsync(string id, int storeid);
        Task<IEnumerable<RegisterListViewModel>> GetApiUsersAsync(int storeid);
        int GetShiftId(string userId, int storeId);
        UserRoleDataViewModel GetUserLoginData(string userId);
        void UpdateRole(ApplicationRole role);
    }
}
