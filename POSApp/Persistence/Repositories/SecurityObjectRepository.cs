using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class SecurityObjectRepository : ISecurityObjectRepository
    {
        private PosDbContext _context;

        public SecurityObjectRepository(PosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SecurityObject> GetSecurityObjects()
        {
            return _context.SecurityObjects.ToList();
        }

        public SecurityObject GetSecurityObject(int id)
        {
            return _context.SecurityObjects.Find(id);
        }

        public void AddSecurityObject(SecurityObject SecurityObject)
        {
            if (!_context.SecurityObjects.Where(a => a.Name == SecurityObject.Name).Any())
            {
                _context.SecurityObjects.Add(SecurityObject);
            }
        }

        public void UpdateSecurityObject(int id, SecurityObject SecurityObject)
        {
            _context.SecurityObjects.Attach(SecurityObject);
            _context.Entry(SecurityObject).State = EntityState.Modified;
        }

        public void DeleteSecurityObject(int id)
        {
            var SecurityObject = new SecurityObject { SecurityObjectId = id };
            _context.SecurityObjects.Attach(SecurityObject);
            _context.Entry(SecurityObject).State = EntityState.Deleted;
        }
    
    }
}