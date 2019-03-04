using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetDevices(int storeid);
        Device GetDeviceById(int id, int storeid);
        void AddDevice(Device Device);
        void UpdateDevice(int id, Device Device, int storeid);
        void DeleteDevice(int id, int storeid);
        Task<IEnumerable<Device>> GetDevicesAsync(int storeid);
        Task<Device> GetDeviceByIdAsync(int id, int storeid);
        Task AddDeviceAsync(Device Device);
        Task<AppInfoViewModel> GetDeviceByLicenseAsync(string license);
    }
}