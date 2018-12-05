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
            return _context.SecurityObjects.Where(a=> !a.IsDisabled).ToList();
        }

        public SecurityObject GetSecurityObject(int id)
        {
            return _context.SecurityObjects.Find(id);
        }

        public void AddSecurityObject(SecurityObject SecurityObject)
        {
            var inDb = _context.SecurityObjects.FirstOrDefault(a => a.Name == SecurityObject.Name);
            if (inDb == null)
            {
                _context.SecurityObjects.Add(SecurityObject);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    SecurityObject.SecurityObjectId = inDb.SecurityObjectId;
                    _context.Entry(inDb).CurrentValues.SetValues(SecurityObject);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }

        public void UpdateSecurityObject(int id, SecurityObject SecurityObject)
        {
            _context.SecurityObjects.Attach(SecurityObject);
            _context.Entry(SecurityObject).State = EntityState.Modified;
        }

        public void DeleteSecurityObject(int id)
        {
            var securityObject = _context.SecurityObjects.FirstOrDefault(a => a.SecurityObjectId == id);
            securityObject.IsDisabled = true;
            _context.SecurityObjects.Attach(securityObject);
            _context.Entry(securityObject).State = EntityState.Modified;
        }
    
    }
}