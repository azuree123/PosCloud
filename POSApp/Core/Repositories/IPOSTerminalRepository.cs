using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Repositories
{
    public interface IPOSTerminalRepository
    {
        IEnumerable<POSTerminal> GetPOSTerminals(int storeid);
        POSTerminal GetPOSTerminalById(int id, int storeid);
        void AddPOSTerminal(POSTerminal POSTerminal);
        void UpdatePOSTerminal(int id, POSTerminal POSTerminal, int storeid);
        void DeletePOSTerminal(int id, int storeid);
    }
}