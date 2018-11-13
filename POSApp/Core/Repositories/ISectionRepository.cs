using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}