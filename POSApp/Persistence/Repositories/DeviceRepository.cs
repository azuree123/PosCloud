﻿using System;
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
            return _context.Devices.Where(a => a.StoreId == storeid).ToList();
        }

        public Device GetDeviceById(int id, int storeid)
        {
            return _context.Devices.Find(id, storeid);
        }

        public void AddDevice(Device Device)
        {
            if (!_context.Devices.Where(a => a.Name == Device.Name && a.StoreId == Device.StoreId).Any())
            {
                _context.Devices.Add(Device);
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
            var Device = new Device { Id = id, StoreId = storeid };
            _context.Devices.Attach(Device);
            _context.Entry(Device).State = EntityState.Deleted;
        }
    }
}