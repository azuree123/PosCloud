using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class SecurityRightRepository : ISecurityRightRepository
    {
        private PosDbContext _context;

        public SecurityRightRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SecurityRight> GetSecurityRights()
        {
            return _context.SecurityRights.ToList();
        }

        public SecurityRight GetSecurityRightById(string id, int storeid,int objectId)
        {
            return _context.SecurityRights.FirstOrDefault(a=>a.IdentityUserRoleId==id && a.StoreId==storeid && a.SecurityObjectId==objectId);
        }

        public void AddSecurityRight(SecurityRight SecurityRight)
        {
            _context.SecurityRights.Add(SecurityRight);
        }

        public void UpdateSecurityRight(int id, int storeid, SecurityRight SecurityRight)
        {
            SecurityRight.StoreId = storeid;
            _context.SecurityRights.Attach(SecurityRight);
            _context.Entry(SecurityRight).State = EntityState.Modified;
        }
        public void DeleteSecurityRight(string id, int storeid,int objectId)
        {
            var SecurityRight = new SecurityRight { IdentityUserRoleId = id, StoreId = storeid,SecurityObjectId = objectId};
            _context.SecurityRights.Attach(SecurityRight);
            _context.Entry(SecurityRight).State = EntityState.Deleted;

            
        }
        public void DeleteSecurityRightbyRole(string id, int storeid)
        {
            var securityRights =_context.SecurityRights.Where(a=>a.IdentityUserRoleId==id && a.StoreId==storeid);
            _context.SecurityRights.RemoveRange(securityRights);

        }
    }
}