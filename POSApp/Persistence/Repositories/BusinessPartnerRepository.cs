using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public BusinessPartner GetBusinessPartner(int id, int StoreId)
        {
            return _context.BusinessPartners.FirstOrDefault(x => x.Id == id && x.StoreId == StoreId);
        }
        public IEnumerable<BusinessPartnerViewModel> GetBusinessPartners(string type, int StoreId)
        {
            //return _context.Customers;
            return _context.BusinessPartners
                
                .Where(u => u.StoreId == StoreId && u.Type == type)
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
                    Birthday = p.Birthday,
                    Note = p.Remarks
                }).ToList();
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
                    Birthday = p.Birthday,
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
                 Birthday = p.Birthday,
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
        public void DeleteBusinessPartner(int id, int StoreId)
        {
            var dept = _context.BusinessPartners.FirstOrDefault(a => a.Id == id && a.StoreId == StoreId);
            _context.BusinessPartners.Remove(dept);
        }
        public void AddBusinessPartner(BusinessPartner item)
        {
            _context.BusinessPartners.Add(item);

        }
        public void UpdateBusinessPartner(int id, int StoreId,BusinessPartner item)
        {
            _context.BusinessPartners.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}