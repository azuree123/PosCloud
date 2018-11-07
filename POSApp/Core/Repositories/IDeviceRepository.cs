using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetDevices(int storeid);
        Device GetDeviceById(int id, int storeid);
        void AddDevice(Device Device);
        void UpdateDevice(int id, Device Device, int storeid);
        void DeleteDevice(int id, int storeid);
    }
}