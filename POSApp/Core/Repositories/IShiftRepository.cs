using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSApp.Core.Repositories
{
    public interface IShiftRepository
    {
        IEnumerable<Shift> GetShifts(int storeid);
        Shift GetShiftById(int id, int storeid);
        void AddShift(Shift Shift);
        void UpdateShift(int id, Shift Shift, int storeid);
        void DeleteShift(int id, int storeid);
    }
}
