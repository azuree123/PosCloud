using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
    public interface IBusinessPartnerRepository
    {
        BusinessPartner GetBusinessPartner(int id, int StoreId);
        IEnumerable<BusinessPartner> GetBusinessPartners(string type, int StoreId);
        IEnumerable<BusinessPartnerViewModel> GetBusinessPartnersFiltered(string type, string query, int StoreId);
        IEnumerable<BusinessPartnerViewModel> GetBusinessPartnersFiltered(string type, int query, int StoreId);
        int IsExisting(string type, string name, int StoreId);
        void DeleteBusinessPartner(int id, int StoreId);
        void AddBusinessPartner(BusinessPartner businesspartner);
        void UpdateBusinessPartner(int id, int StoreId,BusinessPartner businesspartner);
        Task<BusinessPartner> GetBusinessPartnerAsync(int id, int StoreId);
        Task<IEnumerable<BusinessPartner>> GetBusinessPartnersAsync(string type, int StoreId);
        Task AddBusinessPartnerAsync(BusinessPartner item);
        bool IsWalkIn(int id, int StoreId);
    }
}
