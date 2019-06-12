using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;

namespace POSApp.Persistence.Repositories
{
    public class BusinessPartnerRepository : IBusinessPartnerRepository
    {
        private readonly PosDbContext _context;

        public BusinessPartnerRepository(PosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BusinessPartner>> GetAllUsersAsyncIncremental(int storeId, DateTime date)
        {
            return await _context.BusinessPartners.Where(a => a.StoreId == storeId && !a.IsDisabled && (a.UpdatedOn >= date || a.CreatedOn >= date)).ToListAsync();
        }
        public BusinessPartner GetBusinessPartner(int id, int StoreId)
        {
            return _context.BusinessPartners.FirstOrDefault(x => x.Id == id && x.StoreId == StoreId );
        }
        public async Task<BusinessPartner> GetBusinessPartnerAsync(int id, int StoreId)
        {
            return await _context.BusinessPartners.FirstOrDefaultAsync(x => x.Id == id && x.StoreId == StoreId);
        }
        public IEnumerable<BusinessPartner> GetBusinessPartners(string type, int StoreId)
        {
            //return _context.Customers;
            return _context.BusinessPartners

                .Where(u => u.StoreId == StoreId && u.Type == type && !u.IsDisabled);

        }
        public async Task<IEnumerable<BusinessPartner>> GetBusinessPartnersAsync(string type, int StoreId)
        {
            //return _context.Customers;
            return await _context.BusinessPartners

                .Where(u => u.StoreId == StoreId && u.Type == type && !u.IsDisabled).ToListAsync();

        }
        public IEnumerable<BusinessPartnerViewModel> GetBusinessPartnersFiltered(string type, string query, int StoreId)
        {
            //return _context.Customers;
            query = query.ToUpper();
            return _context.BusinessPartners
                 .Where(x => x.Name.ToUpper().Contains(query) && x.StoreId == StoreId)
                .Select(p => new BusinessPartnerViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    State = p.State,
                    City = p.City,
                    Address = p.Address,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber
                    ,
                    Birthday = Convert.ToDateTime(p.Birthday),
                    Note = p.Remarks
                });
        }
        public IEnumerable<BusinessPartnerViewModel> GetBusinessPartnersFiltered(string type, int query, int StoreId)
        {
            return _context.BusinessPartners
             .Where(x => x.Id == query && x.StoreId == StoreId)
             .Select(p => new BusinessPartnerViewModel
             {
                 Id = p.Id,
                 Name = p.Name,
                 State = p.State,
                 City = p.City,
                 Address = p.Address,
                 Email = p.Email,
                 PhoneNumber = p.PhoneNumber
                    ,
                 Birthday = Convert.ToDateTime(p.Birthday),
                 Note = p.Remarks
             });
        }

        public int IsExisting(string type, string name, int StoreId)
        {
            var customer = _context.BusinessPartners.Where(z => z.Name == name && z.StoreId == StoreId);
            if (customer.Any())
            {
                return customer.FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }

        }

        public bool IsWalkIn(int id,int StoreId)
        {
            var customer = _context.BusinessPartners.Where(z => z.Id == id && z.Type == "C" && z.StoreId == StoreId)
                .ToList().FirstOrDefault();
            if (customer.Name.ToLower() == "walk-in customer")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void DeleteBusinessPartner(int id, int StoreId)
        {
            var dept = _context.BusinessPartners.FirstOrDefault(a => a.Id == id && a.StoreId == StoreId);
            dept.IsDisabled = true;
            _context.BusinessPartners.Attach(dept);
            _context.Entry(dept).State = EntityState.Modified;
            
        }
        public void AddBusinessPartner(BusinessPartner item)
        {
            var inDb = _context.BusinessPartners.FirstOrDefault(a =>
                a.Name == item.Name &&  a.Type == item.Type && a.StoreId == item.StoreId && a.Email == item.Email);
            if (inDb==null)
            {
            _context.BusinessPartners.Add(item);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                item.Id = inDb.Id;
                _context.Entry(inDb).CurrentValues.SetValues(item);
                _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public async Task AddBusinessPartnerAsync(BusinessPartner item)
        {
            var inDb = await _context.BusinessPartners.FirstOrDefaultAsync(a =>
                a.Name == item.Name && a.Type == item.Type && a.StoreId == item.StoreId && a.Email == item.Email);
            if (inDb == null)
            {
                _context.BusinessPartners.Add(item);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    item.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(item);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public void UpdateBusinessPartner(int id, int StoreId,BusinessPartner item)
        {
            item.StoreId = StoreId;
            _context.BusinessPartners.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}