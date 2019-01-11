using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Ninject.Extensions.Conventions.BindingGenerators;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class SizeRepository:ISizeRepository
    {
        private PosDbContext _context;

        public SizeRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Size> GetSizes(int storeId)
        {
            return _context.Sizes.Where(a => a.StoreId == storeId && !a.IsDisabled).ToList();
        }

        public Size GetSizeById(int id, int storeId)
        {
            return _context.Sizes.Find(id, storeId);
        }

        public Size GetSizeByName(string name, int storeId)
        {
            return _context.Sizes.FirstOrDefault(a => a.Name == name && a.StoreId == storeId && !a.IsDisabled);
        }

        public void AddSize(Size Size)
        {
            var inDb = _context.Sizes.FirstOrDefault(a => a.Name == Size.Name && a.StoreId == Size.StoreId);
            if (inDb == null)
            {
                _context.Sizes.Add(Size);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Size.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(Size);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exist!");
                }
            }
        }
        public void UpdateSize(int id, Size Size, int storeid)
        {
            if (Size.Id != id)
            {
                Size.Id = id;
            }
            else { }

            Size.StoreId = storeid;
            _context.Sizes.Attach(Size);
            _context.Entry(Size).State = EntityState.Modified;
        }

        public void DeleteSize(int id, int storeid)
        {
            var size = _context.Sizes.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            size.IsDisabled = true;
            _context.Sizes.Attach(size);
            _context.Entry(size).State = EntityState.Modified;
        }
    }
}