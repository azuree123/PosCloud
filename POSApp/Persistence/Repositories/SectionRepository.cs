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
            return _context.Sections.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
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
            var inDb = _context.Sections.FirstOrDefault(a => a.Name == Section.Name && a.StoreId == Section.StoreId);
            if (inDb == null)
            {
                _context.Sections.Add(Section);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Section.SectionId = inDb.SectionId;
                    _context.Entry(inDb).CurrentValues.SetValues(Section);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
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
            section.IsDisabled = true;
            _context.Sections.Attach(section);
            _context.Entry(section).State = EntityState.Modified;
        }
    }
}