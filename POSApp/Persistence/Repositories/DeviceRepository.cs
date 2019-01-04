using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Persistence;

namespace POSApp.Persistence.Repositories
{
    public class DeviceRepository: IDeviceRepository
     
    {
        private PosDbContext _context;

        public DeviceRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Device> GetDevices(int storeid)
        {
            return _context.Devices.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }

        public Device GetDeviceById(int id, int storeid)
        {
            return _context.Devices.Find(id, storeid);
        }

        public void AddDevice(Device Device)
        {
            var inDb = _context.Devices.FirstOrDefault(a => a.DeviceCode == Device.DeviceCode && a.StoreId == Device.StoreId);
            if (inDb == null)
            {
                _context.Devices.Add(Device);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Device.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(Device);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }

        public void UpdateDevice(int id, Device Device, int storeid)
        {
            if (Device.Id != id)
            {
                Device.Id = id;
            }
            else { }

            Device.StoreId = storeid;
            _context.Devices.Attach(Device);
            _context.Entry(Device).State = EntityState.Modified;
        }

        public void DeleteDevice(int id, int storeid)
        {
            var device = _context.Devices.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            device.IsDisabled = true;
            _context.Devices.Attach(device);
            _context.Entry(device).State = EntityState.Modified;
        }
    }
}