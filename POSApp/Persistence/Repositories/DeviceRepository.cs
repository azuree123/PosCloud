using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;
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
        public async Task<IEnumerable<Device>> GetDevicesAsync(int storeid)
        {
            return await _context.Devices.Where(a => a.StoreId == storeid && !a.IsDisabled).ToListAsync();
        }
        public Device GetDeviceById(int id, int storeid)
        {
            return _context.Devices.Find(id, storeid);
        }
        public async Task<Device> GetDeviceByIdAsync(int id, int storeid)
        {
            return await _context.Devices.FindAsync(id, storeid);
        }
        public async Task<AppInfoViewModel> GetDeviceByLicenseAsync(string license)
        {
            return await _context.Devices.Include(a => a.Store).Include(a => a.Store.Client).Where(a => a.License == license && a.IsDisabled==false).Select(a => new AppInfoViewModel
            {
                BusinessStartTime = a.Store.BusinessStartTime,
                BranchName = a.Store.Name,
                CompanyName = a.Store.Client.Name,
                Currency = a.Store.Currency,
                RefundPin = a.RefundPin,
                ArabicCompanyName = a.ArabicName,
                DeviceArabicName = a.ArabicName,
                ReceiptHeader = a.ReceiptHeader,
                ReceiptFooter = a.ReceiptFooter,
                DeviceName = a.Name,
                StoreId = a.StoreId,
                DeviceId = a.Id,
                StoreAddress = a.Store.Address
            }).FirstOrDefaultAsync();
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
        public async Task AddDeviceAsync(Device Device)
        {
            var inDb = await _context.Devices.FirstOrDefaultAsync(a => a.DeviceCode == Device.DeviceCode && a.StoreId == Device.StoreId);
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