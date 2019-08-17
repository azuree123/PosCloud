using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IFloorRepository
    {
        IEnumerable<Floor> GetFloors(int storeid);
        Floor GetFloorById(int id, int storeid);
        void AddFloor(Floor Floor);
        void UpdateFloor(int id, Floor Floor, int storeid);
        void DeleteFloor(int id, int storeid);
        Floor GetFloorByFloorNumber(string floorNumber, int storeid);
        Task<IEnumerable<Floor>> GetFloorsAsync(int storeid);
        Task<Floor> GetFloorByIdAsync(int id, int storeid);
        Task AddFloorAsync(Floor Floor);
        Task<IEnumerable<Floor>> GetAllFloorsAsyncIncremental(int storeId, DateTime date);
    }
}