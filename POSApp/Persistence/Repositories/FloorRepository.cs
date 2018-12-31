using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class FloorRepository:IFloorRepository
    {
        private PosDbContext _context;

        public FloorRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Floor> GetFloors(int storeid)
        {
            return _context.Floors.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }

        public Floor GetFloorById(int id, int storeid)
        {
            return _context.Floors.Find(id, storeid);
        }
        public Floor GetFloorByFloorNumber(string floorNumber, int storeid)
        {
            return _context.Floors.FirstOrDefault(a=>a.FloorNumber==floorNumber && a.StoreId==storeid && !a.IsDisabled);
        }
        public void AddFloor(Floor Floor)
        {
            var inDb = _context.Floors.FirstOrDefault(a =>
                a.FloorNumber == Floor.FloorNumber && a.StoreId == Floor.StoreId);
            if (inDb == null)
            {
                _context.Floors.Add(Floor);

            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Floor.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(Floor);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
           

        }

        public void UpdateFloor(int id, Floor Floor, int storeid)
        {
            if (Floor.Id != id)
            {
                Floor.Id = id;
            }
            else { }

            Floor.StoreId = storeid;
            _context.Floors.Attach(Floor);
            _context.Entry(Floor).State = EntityState.Modified;
        }

        public void DeleteFloor(int id, int storeid)
        {
            var floor = _context.Floors.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            floor.IsDisabled = true;
            _context.Floors.Attach(floor);
            _context.Entry(floor).State = EntityState.Modified;
        }
    }
}