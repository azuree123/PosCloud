using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace POSApp.Core.Repositories
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetSections(int storeid);
        Section GetSectionById(int id, int storeid);
        void AddSection(Section Section);
        void UpdateSection(int id, Section Section, int storeid);
        void DeleteSection(int id, int storeid);
        Section GetSectionBySectionNumber(string SectionNumber, int storeid);
        Section GetSectionByCode(string code, int storeid);
        Task<IEnumerable<Section>> GetSectionsAsync(int storeid);
        Task<Section> GetSectionByIdAsync(int id, int storeid);
        Task AddSectionAsync(Section Section);
    }
}