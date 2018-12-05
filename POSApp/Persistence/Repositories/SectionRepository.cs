using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class SectionRepository:ISectionRepository
    {
        private PosDbContext _context;

        public SectionRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Section> GetSections(int storeid)
        {
            return _context.Sections.Where(a => a.StoreId == storeid && a.IsActive).ToList();
        }

        public Section GetSectionById(int id, int storeid)
        {
            return _context.Sections.Find(id, storeid);
        }
        public Section GetSectionByCode(string code, int storeid)
        {
            return _context.Sections.FirstOrDefault(a=>a.Code==code && a.StoreId== storeid);
        }
        public Section GetSectionBySectionNumber(string SectionNumber, int storeid)
        {
            return _context.Sections.FirstOrDefault(a => a.Name == SectionNumber && a.StoreId == storeid);
        }
        public void AddSection(Section Section)
        {
            if (!_context.Sections.Where(a => a.Name == Section.Name && a.StoreId == Section.StoreId).Any())
            {
                _context.Sections.Add(Section);
            }

        }

        public void UpdateSection(int id, Section Section, int storeid)
        {
            if (Section.SectionId != id)
            {
                Section.SectionId = id;
            }
            else { }

            Section.StoreId = storeid;
            _context.Sections.Attach(Section);
            _context.Entry(Section).State = EntityState.Modified;
        }

        public void DeleteSection(int id, int storeid)
        {
            var section = _context.Sections.FirstOrDefault(a => a.SectionId == id && a.StoreId == storeid);
            section.IsActive = false;
            _context.Sections.Attach(section);
            _context.Entry(section).State = EntityState.Modified;
        }
    }
}