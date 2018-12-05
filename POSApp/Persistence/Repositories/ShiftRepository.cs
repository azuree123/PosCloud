using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ShiftRepository:IShiftRepository
    {
        
        private PosDbContext _context;

        public ShiftRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Shift> GetShifts(int storeid)
        {
            return _context.Shifts.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }

        public Shift GetShiftById(int id, int storeid)
        {
            return _context.Shifts.Find(id, storeid);
        }
       
        public void AddShift(Shift Shift)
        {
            var inDb = _context.Shifts.FirstOrDefault(a => a.Name == Shift.Name && a.StoreId == Shift.StoreId);
            if (inDb == null)
            {
                _context.Shifts.Add(Shift);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Shift.ShiftId = inDb.ShiftId;
                    _context.Entry(inDb).CurrentValues.SetValues(Shift);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }

        public void UpdateShift(int id, Shift Shift, int storeid)
        {
            if (Shift.ShiftId != id)
            {
                Shift.ShiftId = id;
            }
            else { }

            Shift.StoreId = storeid;
            _context.Shifts.Attach(Shift);
            _context.Entry(Shift).State = EntityState.Modified;
        }

        public void DeleteShift(int id, int storeid)
        {
            var shift = _context.Shifts.FirstOrDefault(a => a.ShiftId == id && a.StoreId == storeid);
            shift.IsDisabled = true;
            _context.Shifts.Attach(shift);
            _context.Entry(shift).State = EntityState.Modified;
        }
    
}
}